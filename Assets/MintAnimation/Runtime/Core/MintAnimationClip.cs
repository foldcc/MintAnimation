using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation {

    public delegate T MintGetter<out T>();

    public delegate void MintSetter<in T>(T rNewValue);

    public class MintAnimationClip<T>
    {
        /// <summary>
        /// 请使用Create方法构建MintAnimation
        /// </summary>
        protected MintAnimationClip() {}

        public MintAnimationClip(MintGetter<T> mintGetter, MintSetter<T> mintSetter , MintAnimationInfo mintAnimationInfo) {
            _getter = mintGetter;
            _setter = mintSetter;
            AnimationInfo = mintAnimationInfo;
            Init();
        }

        public Action                                           OnComplete;

        public MintAnimationInfo                                AnimationInfo;

        private MintGetter<T>                                   _getter;
        private MintSetter<T>                                   _setter;

        private float                                           _nowTime;
        private bool                                            _isPause;

        private int                                             _nowLoopCount;
        private float                                           _backTime;

        public void Init()
        {
            _nowTime = 0;
            _isPause = true;
            _backTime = AnimationInfo.Duration / 2;
            register();
        }

        public void Play() {
            _isPause = false;
        }
        public void Pause() {
            _isPause = true;
        }
        public void Stop() {
            _nowTime = AnimationInfo.Duration;
            setAnimationValue();
            _isPause = true;
            unregister();
        }

        private bool updateAnimation(float deltaTime) {
            if (_isPause) return false;
            setAnimationValue();
            if (_nowTime >= AnimationInfo.Duration) {
                _nowLoopCount++;
                if (AnimationInfo.IsLoop)
                {
                    if (AnimationInfo.LoopCount == -1 || _nowLoopCount < AnimationInfo.LoopCount)
                    {
                        _nowTime = 0;
                        return true;
                    }
                }
                OnComplete?.Invoke();
                Stop();
            }
            else _nowTime += deltaTime;
            return true;
        }
        private void setAnimationValue()
        {
            if (AnimationInfo.IsBack)
            {
                if (_nowTime <= _backTime)
                    _setter.Invoke(AnimationInfo.GetProgress<T>(_nowTime * 2));
                else
                    _setter.Invoke(AnimationInfo.GetProgress<T>(AnimationInfo.Duration - ((_nowTime - _backTime) * 2)));
            }
            else
            {
                _setter.Invoke(AnimationInfo.GetProgress<T>(_nowTime));
            }
        }

        private void register() {
            switch (AnimationInfo.DriveType)
            {
                case DriveEnum.Custom:
                    if (AnimationInfo.CustomDrive != null) {
                        AnimationInfo.CustomDrive.AddDriveAction(updateAnimation, AnimationInfo.UpdaterTypeEnum);
                    }
                    break;
                case DriveEnum.Globa:
                    MintDriveComponentSinge.Instance.AddDriveAction(updateAnimation, AnimationInfo.UpdaterTypeEnum);
                    break;
            }
        }
        private void unregister() {
            switch (AnimationInfo.DriveType)
            {
                case DriveEnum.Custom:
                    if (AnimationInfo.CustomDrive != null)
                    {
                        AnimationInfo.CustomDrive?.RemoveDriveAction(updateAnimation , AnimationInfo.UpdaterTypeEnum);
                    }
                    break;
                case DriveEnum.Globa:
                    MintDriveComponentSinge.Instance?.RemoveDriveAction(updateAnimation, AnimationInfo.UpdaterTypeEnum);
                    break;
            }
        }

        public static MintAnimationClip<float> Create(MintGetter<float> mintGetter, MintSetter<float> mintSetter, float endvalue, float duration)
        {
            MintAnimationInfo mintAnimationInfo = new MintAnimationInfo();
            mintAnimationInfo.EaseType = MintEaseMethod.Linear;
            mintAnimationInfo.StartF = mintGetter.Invoke();
            mintAnimationInfo.EndF = endvalue;
            mintAnimationInfo.Duration = duration;
            var a = new MintAnimationClip<float>(mintGetter, mintSetter, mintAnimationInfo);
            return a;
        }
    }
}