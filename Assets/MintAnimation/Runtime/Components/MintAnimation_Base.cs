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
        protected MintTweener<T>        mMintTweener;
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
            if (MintAnimationOptions.AutoStartValue)
            {
                this.StartValue = this.getter();
            }
            animationInfo.StartValue = StartValue;
            animationInfo.EndValue = EndValue;
            animationInfo.Options = MintAnimationOptions;
            mMintTweener = new MintTweener<T>(getter, setter, animationInfo);
            mMintTweener.OnComplete += this.OnCompleteAction;
            _isFristInit = false;
        }

        public void OnCompleteAction()
        {
            this.OnComplete?.Invoke();
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
            this.mMintTweener.AnimationInfo.StartValue = this.StartValue;
            this.mMintTweener.AnimationInfo.EndValue = this.EndValue;
            this.mMintTweener.AnimationInfo.Options = this.MintAnimationOptions;
            this.mMintTweener.Play();
        }

        public void Pause()
        {
            mMintTweener.Pause(!mMintTweener.IsPause);
        }

        public void Stop()
        {
            mMintTweener.Stop();
        }

        protected virtual MintAnimationDataBase<T> SetAnimationInfo()
        {
            return null;
        }
    }
}
