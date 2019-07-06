using System;
using Game;
using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
	public abstract class MintAnimation_Base<T> : MonoBehaviour
	{
        public Action                   OnComplete;
        public MintAnimationOptions     MintAnimationOptions = new MintAnimationOptions();
        public bool                     IsAutoPlay = true;
        public T                        StartValue;
        public T                        EndValue;

        public PlayEndAction            CompleteAction = PlayEndAction.None;
        
        private bool                    _isFristInit = true;
        protected MintAnimationClip<T>  mMintAnimationClip;
        private void OnEnable()
        {
            if (_isFristInit) init();
            if (IsAutoPlay)
            {
                Play();
            }
        }
        private void OnDisable()
        {
            Stop();
        }

        protected virtual void init()
        {
            MintAnimationDataBase<T> animationInfo = SetAnimationInfo();
            animationInfo.StartValue = StartValue;
            animationInfo.EndValue = EndValue;
            animationInfo.Options = MintAnimationOptions;
            if (MintAnimationOptions.AutoStartValue) animationInfo.SetStartValue(getter());
            mMintAnimationClip = new MintAnimationClip<T>(getter, setter, animationInfo);
            mMintAnimationClip.OnComplete += OnComplete;
            mMintAnimationClip.OnComplete += this.OnCompleteAction;
            _isFristInit = false;
        }

        public void OnCompleteAction()
        {
            switch (CompleteAction)
            {
                case PlayEndAction.Destory:
                    Destroy(this.gameObject);
                    break;
                case PlayEndAction.Disable:
                    this.gameObject.SetActive(false);
                    break;
                case PlayEndAction.DestoryAnimation:
                    Destroy(this);
                    break;
            }
        }

        protected virtual T getter(){return default;}
        protected virtual void setter(T value){ }

        public void Play()
        {
            this.mMintAnimationClip.AnimationInfo.Options = this.MintAnimationOptions;
            this.mMintAnimationClip.Play();
        }

        public void Pause()
        {
            mMintAnimationClip.Pause(!mMintAnimationClip.IsPause);
        }

        public void Stop()
        {
            mMintAnimationClip.Stop();
        }

        protected virtual MintAnimationDataBase<T> SetAnimationInfo()
        {
            return null;
        }
    }
}
