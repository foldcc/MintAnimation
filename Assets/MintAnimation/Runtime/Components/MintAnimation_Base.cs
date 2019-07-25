using MintAnimation.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace MintAnimation
{
	public abstract class MintAnimation_Base<T> : MonoBehaviour
    {
        protected MintTweener<T>         mMintTweener;
        private bool                     _isFristInit = true;
        
        private void OnEnable()
        {
            if (_isFristInit) init();
            if (getAnimationData().IsAutoPlay)
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
            if (getAnimationData().AutoStartValue)
            {
                this.getAnimationData().StartValue = this.getter();
            }
            mMintTweener = new MintTweener<T>(getter, setter, getAnimationData());
            mMintTweener.OnComplete += this.OnCompleteAction;
            _isFristInit = false;
        }

        public void OnCompleteAction()
        {
            this.getAnimationData().OnComplete?.Invoke();
            switch (getAnimationData().CompleteAction)
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
//            this.mMintTweener.TweenInfo.StartValue = this.StartValue;
//            this.mMintTweener.TweenInfo.EndValue = this.EndValue;
//            this.mMintTweener.TweenInfo.SetOptions(this.MintAnimationOptions);
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

        protected abstract MintAnimationData<T> getAnimationData();
    }
}
