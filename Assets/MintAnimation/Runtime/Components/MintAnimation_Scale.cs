using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
    [AddComponentMenu("MintAnimation/ScaleAnimation", 1)]
    public class MintAnimation_Scale : MintAnimation_Base<Vector3>
    {
        [SerializeField]
        private MintAnimationDataVector MintAnimationData = new MintAnimationDataVector();

        public bool IsSizeDelta;
        
        protected override void setter(Vector3 value)
        {
            if (this.IsSizeDelta)
            {
                ((RectTransform) transform).sizeDelta = value;
            }
            else
            {
                transform.localScale = value;
            }
        }

        protected override MintTweenDataBase<Vector3> getAnimationData()
        {
            return MintAnimationData;
        }

        protected override Vector3 getter()
        {
                return this.IsSizeDelta ? (Vector3)((RectTransform) transform).sizeDelta : transform.localScale;
        }
        
    }
}
