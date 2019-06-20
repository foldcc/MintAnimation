using System;
using Game;
using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
	public abstract class MintAnimation_Base<T> : MonoBehaviour
	{
        public Action                   OnComplete;
        public MintAnimationInfo        AnimationInfo;
        public bool                     IsAutoPlay = true;
        public PlayEndAction            CompleteAction = PlayEndAction.None;
        private bool                    _isFristInit = true;

        protected MintAnimationClip<T>  mMintAnimationClip;

        private void OnEnable()
        {
            init();
            if (IsAutoPlay)
            {
                Play();
            }
        }
        private void OnDisable()
        {
            Stop();
        }

        protected virtual void init() {
            if (AnimationInfo.AutoStartValue && _isFristInit) AnimationInfo.SetStartValue(getter());
            this.mMintAnimationClip = new MintAnimationClip<T>(getter, setter, AnimationInfo);
            this.mMintAnimationClip.OnComplete += OnComplete;
            this.SetEndAciton();
            _isFristInit = false;
        }

        private void SetEndAciton()
        {
            switch (this.CompleteAction)
            {
                case PlayEndAction.Destory:
                    this.mMintAnimationClip.OnComplete += () =>
                    {
                        Destroy(this.gameObject);
                    };
                    break;
                case PlayEndAction.Disable:
                    this.mMintAnimationClip.OnComplete += () =>
                    {
                        this.gameObject.SetActive(false);
                    };
                    break;
            }
        }

        protected virtual T getter(){return default;}
        protected virtual void setter(T value){ }

        public void Play()
        {
            mMintAnimationClip.Play();
        }

        public void Pause()
        {
            mMintAnimationClip.Pause();
        }

        public void Stop()
        {
            mMintAnimationClip.Stop();
        }
    }
}
