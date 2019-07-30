using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
    [AddComponentMenu("MintAnimation/RotationAnimation", 1)]
    public class MintAnimation_Rotation : MintAnimation_Base<Vector3>
    {
        [SerializeField]
        private MintAnimationDataVector MintAnimationData = new MintAnimationDataVector();
        
        public bool IsLocal;
        
        protected override void setter(Vector3 value)
        {
            if (this.IsLocal)
                transform.localRotation = Quaternion.Euler(value);
            else
                transform.rotation = Quaternion.Euler(value);
        }

        protected override MintTweenDataBase<Vector3> getAnimationData()
        {
            return MintAnimationData;
        }

        protected override Vector3 getter()
        {
            if (this.IsLocal)
                return transform.localRotation.eulerAngles;
            else
                return transform.rotation.eulerAngles;
        }

    }


}

