using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
    [AddComponentMenu("MintAnimation/PositionAnimation", 1)]
    public class MintAnimation_Position : MintAnimation_Base<Vector3>
    {

        public bool IsLocal;

        public bool IsBezier;

        public Vector3 BezierP1;
        public Vector3 BezierP2;
        
        protected override void setter(Vector3 value)
        {
            if (this.IsBezier)
            {
                Bezier_3ref(ref value, this.mMintTweener.AnimationInfo.StartValue, this.mMintTweener.AnimationInfo.StartValue + this.BezierP1, this.mMintTweener.AnimationInfo.StartValue + this.BezierP2, this.mMintTweener.AnimationInfo.EndValue,
                    this.mMintTweener.GetProgress());
            }
            
            if (IsLocal)
                transform.localPosition = value;
            else
                transform.position = value;
        }

        protected override Vector3 getter()
        {
            if (IsLocal)
                return transform.localPosition;
            else
                return transform.position;
        }

        protected override MintAnimationDataBase<Vector3> SetAnimationInfo()
        {
            return new MintAnimationDataVector3();
        }
        
        public static void Bezier_3ref(ref Vector3 outValue , Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            outValue = (1 - t) * ((1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2)) + t * ((1 - t) * ((1 - t) * p1 + t * p2) + t * ((1 - t) * p2 + t * p3));
        }
    }
}

    
