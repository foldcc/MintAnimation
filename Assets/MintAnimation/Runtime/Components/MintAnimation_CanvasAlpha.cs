using System.Collections;
using System.Collections.Generic;
using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation {

    [AddComponentMenu("MintAnimation/CanvasAlphaAnimation", 1) , RequireComponent(typeof(CanvasGroup))]
    public class MintAnimation_CanvasAlpha : MintAnimation_Base<float>
    {
        private CanvasGroup mCanvasGroup;

        [SerializeField]
        private MintAnimationDataFloat MintAnimationData = new MintAnimationDataFloat();
        
        protected override void init()
        {
            mCanvasGroup = GetComponent<CanvasGroup>();
            base.init();
        }

        protected override float getter()
        {
            return mCanvasGroup.alpha;
        }

        protected override void setter(float value)
        {
            mCanvasGroup.alpha = value;
        }

        protected override MintTweenDataBase<float> getAnimationData()
        {
            return MintAnimationData;
        }

    }

}
