using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation
{
	public abstract class MintAnimation_Base<T> : MonoBehaviour
	{
        public MintAnimationInfo        AnimationInfo;
        public bool                     IsAutoPlay = true;

        private bool                    _isFristInit = true;

        protected MintAnimationClip<T>  mMintAnimationClip;

        private void OnEnable()
        {
            init();
            if (AnimationInfo.AutoStartValue && _isFristInit) AnimationInfo.SetStartValue<T>(getter());
            _isFristInit = false;
            if (IsAutoPlay)
            {
                mMintAnimationClip.Play();
            }
        }
        private void OnDisable()
        {
            mMintAnimationClip?.Stop();
        }

        protected virtual void init() {
            mMintAnimationClip = new MintAnimationClip<T>(getter, setter, AnimationInfo);
        }

        protected virtual T getter(){return default;}
        protected virtual void setter(T value){ }
    }
}
