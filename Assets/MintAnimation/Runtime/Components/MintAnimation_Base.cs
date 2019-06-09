using System;
using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
	public abstract class MintAnimation_Base<T> : MonoBehaviour
	{
        public Action                   OnComplete;
        public MintAnimationOptions     MintAnimationOptions;
        public bool                     IsAutoPlay = true;
        public T                        StartValue;
        public T                        EndValue;
        
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

        protected virtual void init()
        {
            if (!_isFristInit) return;
            MintAnimationDataBase<T> animationInfo = SetAnimationInfo();
            animationInfo.StartValue = StartValue;
            animationInfo.EndValue = EndValue;
            animationInfo.Options = MintAnimationOptions;
            if (MintAnimationOptions.AutoStartValue) animationInfo.SetStartValue(getter());
            mMintAnimationClip = new MintAnimationClip<T>(getter, setter, animationInfo);
            mMintAnimationClip.OnComplete += OnComplete;
            _isFristInit = false;
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

        protected virtual MintAnimationDataBase<T> SetAnimationInfo()
        {
            return null;
        }
    }
}
