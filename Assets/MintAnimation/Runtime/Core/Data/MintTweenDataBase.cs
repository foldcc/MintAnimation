﻿using UnityEngine;

namespace MintAnimation.Core
{
    [System.Serializable]
    public class MintTweenDataBase<T> : MintTweenOptions
    {
        public T StartValue;
        public T EndValue;

        private IMintTweenBehaviour<T> _handler;

        public IMintTweenBehaviour<T> Handler
        {
            get
            {
                if (_handler == null)
                {
                    if (typeof(T) == typeof(float))
                    {
                        _handler = (IMintTweenBehaviour<T>) new MintHandleFloat();
                    } 
                    else if (typeof(T) == typeof(Vector3))
                    {
                        _handler = (IMintTweenBehaviour<T>) new MintHandleVector3();
                    }
                    else if (typeof(T) == typeof(Color))
                    {
                        _handler = (IMintTweenBehaviour<T>) new MintHandleColor();
                    }
                }
                return _handler;
            }
            set => _handler = value;
        }

        public void SetOptions(MintTweenOptions options)
        {
            Duration = options.Duration;
            IsBack = options.IsBack;
            IsLoop = options.IsLoop;
            IsReversal = options.IsReversal;
            LoopCount = options.LoopCount;
            IsCustomEase = options.IsCustomEase;
            EaseType = options.EaseType;
            TimeCurve = options.TimeCurve;
            DriveType = options.DriveType;
            UpdaterTypeEnum = options.UpdaterTypeEnum;
            CustomDrive = options.CustomDrive;
        }
    }
}