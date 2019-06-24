using System;
using UnityEngine;

namespace MintAnimation.Core
{

    public delegate T MintGetter<out T>();

    public delegate void MintSetter<in T>(T rNewValue);

    public class MintAnimationClip<T>
    {
        /// <summary>
        /// 请使用Create方法构建MintAnimation
        /// </summary>
        protected MintAnimationClip() {}

        public MintAnimationClip(MintGetter<T> mintGetter, MintSetter<T> mintSetter , MintAnimationDataBase<T> mintAnimationInfo) {
            _getter = mintGetter;
            _setter = mintSetter;
            AnimationInfo = mintAnimationInfo;
        }

        public Action                                           OnComplete;

        public MintAnimationDataBase<T>                         AnimationInfo;

        private MintGetter<T>                                   _getter;
        private MintSetter<T>                                   _setter;

        private float                                           _nowTime;
        private bool                                            _isPause;

        private int                                             _nowLoopCount;
        private float                                           _backTime;

        public void Reset()
        {
            _nowTime = 0;
            _isPause = true;
            _backTime = AnimationInfo.Options.Duration / 2;
            setAnimationValue();
            register();
        }

        public void RePlay()
        {
            this.Reset();
            this.Play();
        }
        public void Play() {
            _isPause = false;
        }
        public void Pause() {
            _isPause = true;
        }
        public void Stop() {
            _nowTime = AnimationInfo.Options.Duration;
            setAnimationValue();
            _isPause = true;
            unregister();
        }

        private bool updateAnimation(float deltaTime) {
            if (_isPause) return false;
            setAnimationValue();
            if (_nowTime >= AnimationInfo.Options.Duration) {
                _nowLoopCount++;
                if (AnimationInfo.Options.IsLoop)
                {
                    if (AnimationInfo.Options.LoopCount == -1 || _nowLoopCount < AnimationInfo.Options.LoopCount)
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
            if (AnimationInfo.Options.IsBack)
            {
                if (_nowTime <= _backTime)
                    _setter.Invoke(AnimationInfo.GetProgress(_nowTime * 2));
                else
                    _setter.Invoke(AnimationInfo.GetProgress(AnimationInfo.Options.Duration - ((_nowTime - _backTime) * 2)));
            }
            else
            {
                _setter.Invoke(AnimationInfo.GetProgress(_nowTime));
            }
        }

        private void register() {
            switch (AnimationInfo.Options.DriveType)
            {
                case DriveEnum.Custom:
                    if (AnimationInfo.Options.CustomDrive != null) {
                        AnimationInfo.Options.CustomDrive.AddDriveAction(updateAnimation, AnimationInfo.Options.UpdaterTypeEnum);
                    }
                    break;
                case DriveEnum.Globa:
                    MintDriveComponentSinge.Instance.AddDriveAction(updateAnimation, AnimationInfo.Options.UpdaterTypeEnum);
                    break;
            }
        }
        private void unregister() {
            switch (AnimationInfo.Options.DriveType)
            {
                case DriveEnum.Custom:
                    if (AnimationInfo.Options.CustomDrive != null)
                    {
                        AnimationInfo.Options.CustomDrive.RemoveDriveAction(updateAnimation , AnimationInfo.Options.UpdaterTypeEnum);
                    }
                    break;
                case DriveEnum.Globa:
                    MintDriveComponentSinge.Instance.RemoveDriveAction(updateAnimation, AnimationInfo.Options.UpdaterTypeEnum);
                    break;
            }
        }

        /// <summary>
        /// 获取当前播放进度
        /// </summary>
        /// <returns></returns>
        public float GetPlayerProgress()
        {
            float mNowTime;
            if (AnimationInfo.Options.IsBack)
            {
                if (_nowTime <= _backTime)
                    mNowTime = _nowTime * 2;
                else
                    mNowTime = AnimationInfo.Options.Duration - (_nowTime - _backTime) * 2;
            }
            else
            {
                mNowTime = _nowTime;
            }
            return mNowTime / this.AnimationInfo.Options.Duration;
        }

        /// <summary>
        /// 获取当前从startValue 到 endValue 之间的float进度
        /// </summary>
        /// <returns></returns>
        public float GetProgress()
        {
            if (this.AnimationInfo.Options.IsCustomEase)
            {
                return this.AnimationInfo.Options.TimeCurve.Evaluate(this.GetPlayerProgress());
            }
            else
            {
                return MintEaseAction.GetEaseAction(this.AnimationInfo.Options.EaseType, this.GetPlayerProgress());
            }
        }

        public static MintAnimationClip<float> Create(MintGetter<float> mintGetter, MintSetter<float> mintSetter, float endvalue, float duration)
        {
            MintAnimationDataBase<float> mintAnimationInfo = new MintAnimtaionDataFloat();
            mintAnimationInfo.Options = new MintAnimationOptions(){ EaseType = MintEaseMethod.Linear , AutoStartValue = true , Duration = duration};
            mintAnimationInfo.StartValue = mintGetter.Invoke();
            mintAnimationInfo.EndValue = endvalue;
            var a = new MintAnimationClip<float>(mintGetter, mintSetter, mintAnimationInfo);
            return a;
        }
        public static MintAnimationClip<Vector3> Create(MintGetter<Vector3> mintGetter, MintSetter<Vector3> mintSetter, Vector3 endvalue, float duration)
        {
            MintAnimationDataBase<Vector3> mintAnimationInfo = new MintAnimationDataVector3();
            mintAnimationInfo.Options = new MintAnimationOptions(){ EaseType = MintEaseMethod.Linear , AutoStartValue = true , Duration = duration};
            mintAnimationInfo.StartValue = mintGetter.Invoke();
            mintAnimationInfo.EndValue = endvalue;
            var a = new MintAnimationClip<Vector3>(mintGetter, mintSetter, mintAnimationInfo);
            return a;
        }
        public static MintAnimationClip<Color> Create(MintGetter<Color> mintGetter, MintSetter<Color> mintSetter, Color endvalue, float duration)
        {
            MintAnimationDataBase<Color> mintAnimationInfo = new MintAnimationDataColor();
            mintAnimationInfo.Options = new MintAnimationOptions(){ EaseType = MintEaseMethod.Linear , AutoStartValue = true , Duration = duration};
            mintAnimationInfo.StartValue = mintGetter.Invoke();
            mintAnimationInfo.EndValue = endvalue;
            var a = new MintAnimationClip<Color>(mintGetter, mintSetter, mintAnimationInfo);
            return a;
        }
        
    }
}