using UnityEngine;

namespace MintAnimation
{
    [AddComponentMenu("MintAnimation/RotationAnimation", 1)]
    public class MintAnimation_Rotation : MintAnimation_Base<Quaternion>
    {
        public bool IsLocal;

        protected override void init()
        {
            this.AnimationInfo.StartQ = Quaternion.Euler(AnimationInfo.StartV3);
            this.AnimationInfo.EndQ = Quaternion.Euler(AnimationInfo.EndV3);
            base.init();
        }

        protected override void setter(Quaternion value)
        {
            if (this.IsLocal)
                transform.localRotation = value;
            else
                transform.rotation = value;
        }

        protected override Quaternion getter()
        {
            if (this.IsLocal)
                return transform.localRotation;
            else
                return transform.rotation;
        }
    }


}

