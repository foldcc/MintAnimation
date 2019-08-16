using MintAnimation.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace MintAnimation
{
	public abstract class MintAnimation_Base<T> : MintAnimationComponent
    {
        protected MintTweener<T>         mMintTweener;
        private bool                     _isFristInit = true;
        
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
            if (AutoStartValue)
            {
                this.getAnimationData().StartValue = this.getAutoStartValue();
            }
            mMintTweener = new MintTweener<T>(getter, setter, getAnimationData());
            mMintTweener.OnComplete += this.OnCompleteAction;
            _isFristInit = false;
        }

        protected override void OnCompleteAction()
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
        protected virtual T getAutoStartValue(){return this.getter();}

        public override void Play()
        {
            this.mMintTweener.Play();
        }

        public override void Pause()
        {
            mMintTweener.Pause(!mMintTweener.IsPause);
        }

        public override void Stop()
        {
            mMintTweener.Stop();
        }

        public override MintTweenOptions GetOptions()
        {
            return getAnimationData();
        }

        public override void SetOptions(MintTweenOptions options)
        {
            this.getAnimationData().SetOptions(options);
        }

        protected abstract MintTweenDataBase<T> getAnimationData();
    }
}
