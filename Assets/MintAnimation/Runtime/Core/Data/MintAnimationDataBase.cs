﻿using UnityEngine;

namespace MintAnimation.Core
{
    public abstract class MintAnimationDataBase<T>
    {
        public MintAnimationOptions Options;
        
        public T StartValue;
        public T EndValue;
        
        public virtual T GetProgress(float nowTime)
        {
            return default;
        }
    }
}

