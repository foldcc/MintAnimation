using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation
{
    [AddComponentMenu("MintAnimation/PositionAnimation", 1)]
    public class MintAnimation_Position : MintAnimation_Base<Vector3>
    {

        public bool IsLocal;

        protected override void setter(Vector3 value)
        {
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
    }
}

    
