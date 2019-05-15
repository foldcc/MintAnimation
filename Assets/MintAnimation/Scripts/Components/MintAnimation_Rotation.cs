using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation
{
    [AddComponentMenu("MintAnimation/RotationAnimation", 1)]
    public class MintAnimation_Rotation : MintAnimation_Base<Quaternion>
    {
        public bool IsLocal;

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

