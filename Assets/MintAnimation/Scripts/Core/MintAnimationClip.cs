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

        public void Init()
        {
            _nowTime = 0;
            _isPause = true;
            if (AnimationInfo.AutoStartValue) AnimationInfo.SetStartValue<T>(_getter.Invoke());
            register();
        }

        public void Play() {
            _isPause = false;
        }
        public void Pause() {
            _isPause = true;
        }
        public void Stop() {
            _isPause = true;
            unregister();
        }

        private bool updateAnimation(float deltaTime) {
            if (_isPause) return false;
            _setter.Invoke(AnimationInfo.GetProgress<T>(_nowTime));
            if (_nowTime >= AnimationInfo.Duration) {
                OnComplete?.Invoke();
                Stop();
            }
            else _nowTime += deltaTime;
            return true;
        }

        private void register() {
            switch (AnimationInfo.DriveType)
            {
                case DriveEnum.Custom:
                    if (AnimationInfo.CustomDrive != null) {
                        AnimationInfo.CustomDrive.AddDriveAction(updateAnimation);
                    }
                    break;
                case DriveEnum.Globa:
                    MintDriveComponentSinge.Instance.AddDriveAction(updateAnimation);
                    break;
            }
        }
        private void unregister() {
            switch (AnimationInfo.DriveType)
            {
                case DriveEnum.Custom:
                    if (AnimationInfo.CustomDrive != null)
                    {
                        AnimationInfo.CustomDrive.RemoveDriveAction(updateAnimation);
                    }
                    break;
                case DriveEnum.Globa:
                    MintDriveComponentSinge.Instance.RemoveDriveAction(updateAnimation);
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
            return new MintAnimationClip<float>(mintGetter, mintSetter, mintAnimationInfo);
        }
    }
}