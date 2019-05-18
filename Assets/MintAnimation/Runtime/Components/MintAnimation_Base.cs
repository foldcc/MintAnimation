using System;
using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
	public abstract class MintAnimation_Base<T> : MonoBehaviour
	{
        public Action                   OnComplete;
        public MintAnimationInfo        AnimationInfo;
        public bool                     IsAutoPlay = true;

        private bool                    _isFristInit = true;

        protected MintAnimationClip<T>  mMintAnimationClip;

        private void OnEnable()
        {
            init();
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
            if (AnimationInfo.AutoStartValue && _isFristInit) AnimationInfo.SetStartValue<T>(getter());
            mMintAnimationClip = new MintAnimationClip<T>(getter, setter, AnimationInfo);
            mMintAnimationClip.OnComplete += OnComplete;
            _isFristInit = false;
        }

        protected virtual T getter(){return default;}
        protected virtual void setter(T value){ }

    }
}
