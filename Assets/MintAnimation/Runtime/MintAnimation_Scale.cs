using UnityEngine;

namespace MintAnimation
{
    [AddComponentMenu("MintAnimation/ScaleAnimation", 1)]
    public class MintAnimation_Scale : MintAnimation_Base<Vector3>
    {
        protected override void setter(Vector3 value)
        {
                transform.localScale = value;
        }

        protected override Vector3 getter()
        {
                return transform.localScale;
        }
    }
}
