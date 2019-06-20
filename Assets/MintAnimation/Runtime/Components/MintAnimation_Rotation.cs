using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
    [AddComponentMenu("MintAnimation/RotationAnimation", 1)]
    public class MintAnimation_Rotation : MintAnimation_Base<Vector3>
    {
        public bool IsLocal;

        protected override void setter(Vector3 value)
        {
            if (this.IsLocal)
                transform.localRotation = Quaternion.Euler(value);
            else
                transform.rotation = Quaternion.Euler(value);
        }

        protected override Vector3 getter()
        {
            if (this.IsLocal)
                return transform.localRotation.eulerAngles;
            else
                return transform.rotation.eulerAngles;
        }

        protected override MintAnimationDataBase<Vector3> SetAnimationInfo()
        {
            return new MintAnimationDataVector3();
        }
    }


}

