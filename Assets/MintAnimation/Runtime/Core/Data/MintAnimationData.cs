using System;

namespace MintAnimation.Core
{
    public class MintAnimationData<T> : MintTweenDataBase<T>
    {
        public Action                   OnComplete;
        
        public bool                     IsAutoPlay = true;
        public bool                     AutoStartValue = true;
        public PlayEndAction            CompleteAction = PlayEndAction.None;
    }
}