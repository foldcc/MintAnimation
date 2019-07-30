using System;
using MintAnimation.Core;
using UnityEngine;

namespace MintAnimation
{
    public abstract class MintAnimationComponent : MonoBehaviour
    {
        public Action OnComplete;
        
        public bool          IsAutoPlay     = true;
        public bool          AutoStartValue = true;
        public PlayEndAction CompleteAction = PlayEndAction.None;

        protected abstract void OnCompleteAction();

        public abstract void Play();

        public abstract void Pause();

        public abstract void Stop();

        public abstract MintTweenOptions GetOptions();

        public abstract void SetOptions(MintTweenOptions options);
    }
}