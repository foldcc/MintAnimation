using System;
using UnityEngine;

namespace MintAnimation.Core
{

    public delegate T MintGetter<out T>();

    public delegate void MintSetter<in T>(T rNewValue);

    public class MintTweener<T> : IDisposable
    {
        /// <summary>
        /// 请使用Create方法构建MintAnimation
        /// </summary>
        protected MintTweener() {}

        public MintTweener(MintGetter<T> mintGetter, MintSetter<T> mintSetter , MintAnimationDataBase<T> mintAnimationInfo) {
            _getter = mintGetter;
            _setter = mintSetter;
            AnimationInfo = mintAnimationInfo;
            this.IsPause = true;
            register();
        }

        public Action                                           OnComplete;

        public MintAnimationDataBase<T>                         AnimationInfo;

        public bool                                             IsPause { get; private set; }

        private MintGetter<T>                                   _getter;
        private MintSetter<T>                                   _setter;

        private float                                           _nowTime;
        private float                                           _progressValue;
        
        private int                                             _nowLoopCount;
        private float                                           _backTime;

        public void Play() {
            this.reset();
            this.IsPause = false;
        }
        public void Pause(bool isPause) {
            this.IsPause = isPause;
        }
        public void Stop() {
            _nowTime = AnimationInfo.Options.Duration;
            setAnimationValue();
            this.IsPause = true;
        }
        
        
        private void reset()
        {
            _nowTime = 0;
            this.IsPause = true;
            _backTime = AnimationInfo.Options.Duration / 2;
            setAnimationValue();
        }

        private bool updateAnimation(float deltaTime) {
            if (this.IsPause) return false;
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

        /// <summary>
        /// 获取处理后的Value进度值
        /// </summary>
        /// <returns></returns>
        private float getNowTime() {
            if (AnimationInfo.Options.IsReversal) {
                return AnimationInfo.Options.Duration - _progressValue;
            }
            return _progressValue;
        }

        private void setAnimationValue()
        {
            if (AnimationInfo.Options.IsBack)
            {
                if (_nowTime <= _backTime)
                    _progressValue = _nowTime * 2;
                else
                    _progressValue = AnimationInfo.Options.Duration - ((_nowTime - _backTime) * 2);
            }
            else
            {
                _progressValue = _nowTime;
            }
            _setter.Invoke(AnimationInfo.GetProgress(getNowTime()));
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
            return _nowTime / this.AnimationInfo.Options.Duration;
        }

        /// <summary>
        /// 获取当前从startValue 到 endValue 之间的float进度
        /// </summary>
        /// <returns></returns>
        public float GetProgress()
        {
            if (this.AnimationInfo.Options.IsCustomEase)
            {
                return this.AnimationInfo.Options.TimeCurve.Evaluate(this.getNowTime());
            }
            else
            {
                return MintEaseAction.GetEaseAction(this.AnimationInfo.Options.EaseType, this.getNowTime());
            }
        }

        public static MintTweener<float> Create(MintGetter<float> mintGetter, MintSetter<float> mintSetter, float endvalue, float duration)
        {
            MintAnimationDataBase<float> mintAnimationInfo = new MintAnimtaionDataFloat();
            mintAnimationInfo.Options = new MintAnimationOptions(){ EaseType = MintEaseMethod.Linear , AutoStartValue = true , Duration = duration};
            mintAnimationInfo.StartValue = mintGetter.Invoke();
            mintAnimationInfo.EndValue = endvalue;
            var a = new MintTweener<float>(mintGetter, mintSetter, mintAnimationInfo);
            return a;
        }
        public static MintTweener<Vector3> Create(MintGetter<Vector3> mintGetter, MintSetter<Vector3> mintSetter, Vector3 endvalue, float duration)
        {
            MintAnimationDataBase<Vector3> mintAnimationInfo = new MintAnimationDataVector3();
            mintAnimationInfo.Options = new MintAnimationOptions(){ EaseType = MintEaseMethod.Linear , AutoStartValue = true , Duration = duration};
            mintAnimationInfo.StartValue = mintGetter.Invoke();
            mintAnimationInfo.EndValue = endvalue;
            var a = new MintTweener<Vector3>(mintGetter, mintSetter, mintAnimationInfo);
            return a;
        }
        public static MintTweener<Color> Create(MintGetter<Color> mintGetter, MintSetter<Color> mintSetter, Color endvalue, float duration)
        {
            MintAnimationDataBase<Color> mintAnimationInfo = new MintAnimationDataColor();
            mintAnimationInfo.Options = new MintAnimationOptions(){ EaseType = MintEaseMethod.Linear , AutoStartValue = true , Duration = duration};
            mintAnimationInfo.StartValue = mintGetter.Invoke();
            mintAnimationInfo.EndValue = endvalue;
            var a = new MintTweener<Color>(mintGetter, mintSetter, mintAnimationInfo);
            return a;
        }

        public void Dispose()
        {
            this.unregister();
        }
    }
}