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

        public MintTweener(MintGetter<T> mintGetter, MintSetter<T> mintSetter , MintTweenDataBase<T> mintTweenInfo) {
            _getter = mintGetter;
            _setter = mintSetter;
            TweenInfo = mintTweenInfo;
            this.IsPause = true;
            register();
        }

        public Action                                           OnComplete;

        public MintTweenDataBase<T>                         TweenInfo;

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
            _nowTime = TweenInfo.Duration;
            setAnimationValue();
            this.IsPause = true;
        }
        
        
        private void reset()
        {
            _nowTime = 0;
            this.IsPause = true;
            _backTime = TweenInfo.Duration / 2;
            setAnimationValue();
        }

        private bool updateAnimation(float deltaTime) {
            if (this.IsPause) return false;
            setAnimationValue();
            if (_nowTime >= TweenInfo.Duration) {
                _nowLoopCount++;
                if (TweenInfo.IsLoop)
                {
                    if (TweenInfo.LoopCount == -1 || _nowLoopCount < TweenInfo.LoopCount)
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
            if (TweenInfo.IsReversal) {
                return TweenInfo.Duration - _progressValue;
            }
            return _progressValue;
        }

        private void setAnimationValue()
        {
            if (TweenInfo.IsBack)
            {
                if (_nowTime <= _backTime)
                    _progressValue = _nowTime * 2;
                else
                    _progressValue = TweenInfo.Duration - ((_nowTime - _backTime) * 2);
            }
            else
            {
                _progressValue = _nowTime;
            }
            _setter.Invoke(TweenInfo.Handler.GetProgress(getNowTime() , TweenInfo));
        }

        private void register() {
            switch (TweenInfo.DriveType)
            {
                case DriveEnum.Custom:
                    if (TweenInfo.CustomDrive != null) {
                        TweenInfo.CustomDrive.AddDriveAction(updateAnimation, TweenInfo.UpdaterTypeEnum);
                    }
                    break;
                case DriveEnum.Globa:
                    MintDriveComponentSinge.Instance.AddDriveAction(updateAnimation, TweenInfo.UpdaterTypeEnum);
                    break;
            }
        }
        private void unregister() {
            switch (TweenInfo.DriveType)
            {
                case DriveEnum.Custom:
                    if (TweenInfo.CustomDrive != null)
                    {
                        TweenInfo.CustomDrive.RemoveDriveAction(updateAnimation , TweenInfo.UpdaterTypeEnum);
                    }
                    break;
                case DriveEnum.Globa:
                    MintDriveComponentSinge.Instance.RemoveDriveAction(updateAnimation, TweenInfo.UpdaterTypeEnum);
                    break;
            }
        }

        /// <summary>
        /// 获取当前播放进度
        /// </summary>
        /// <returns></returns>
        public float GetPlayerProgress()
        {
            return _nowTime / this.TweenInfo.Duration;
        }

        /// <summary>
        /// 获取当前从startValue 到 endValue 之间的float进度
        /// </summary>
        /// <returns></returns>
        public float GetProgress()
        {
            if (this.TweenInfo.IsCustomEase)
            {
                return this.TweenInfo.TimeCurve.Evaluate(this.getNowTime());
            }
            else
            {
                return MintEaseAction.GetEaseAction(this.TweenInfo.EaseType, this.getNowTime());
            }
        }

        public static MintTweener<float> Create(MintGetter<float> mintGetter, MintSetter<float> mintSetter, float endvalue, float duration)
        {
            var mintTweenerInfo = new MintTweenDataBase<float>()
            {
                EaseType = MintEaseMethod.Linear,
                Duration = duration,
                StartValue = mintGetter.Invoke(),
                EndValue = endvalue
            };
            var a = new MintTweener<float>(mintGetter, mintSetter, mintTweenerInfo);
            return a;
        }
        public static MintTweener<Vector3> Create(MintGetter<Vector3> mintGetter, MintSetter<Vector3> mintSetter, Vector3 endvalue, float duration)
        {
            var mintTweenerInfo = new MintTweenDataBase<Vector3>()
            {
                EaseType = MintEaseMethod.Linear,
                Duration = duration,
                StartValue = mintGetter.Invoke(),
                EndValue = endvalue
            };
            var a = new MintTweener<Vector3>(mintGetter, mintSetter, mintTweenerInfo);
            return a;
        }
        public static MintTweener<Color> Create(MintGetter<Color> mintGetter, MintSetter<Color> mintSetter, Color endvalue, float duration)
        {
            var mintTweenerInfo = new MintTweenDataBase<Color>()
            {
                EaseType = MintEaseMethod.Linear,
                Duration = duration,
                StartValue = mintGetter.Invoke(),
                EndValue = endvalue
            };
            var a = new MintTweener<Color>(mintGetter, mintSetter, mintTweenerInfo);
            return a;
        }

        public void Dispose()
        {
            this.unregister();
        }
    }
}