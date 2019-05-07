using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation {

    [System.Serializable]
    public class MintAnimationInfo
    {
        public float                Duration = 0.35f;

        public bool                 IsBack;
        public bool                 IsLoop;
        public int                  LoopCount;
       
        public bool                 IsCustomEase = true;
        public MintEaseMethod       EaseType = MintEaseMethod.OutBack;
        public AnimationCurve       TimeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public bool                 AutoStartValue = true;

        public Color                StartCor    = Color.white;
        public Color                EndCor      = Color.white;

        public float                StartF;
        public float                EndF;

        public Vector3              StartV3;
        public Vector3              EndV3;

        private Quaternion          _startQ;
        private Quaternion          _endQ;

        public DriveEnum DriveType = DriveEnum.Globa;
        public MintDriveComponent CustomDrive;

        /// <summary>
        /// 获取当前时间下的float
        /// </summary>
        /// <param name="nowTime">当前时间 [0,Duration]</param>
        /// <returns></returns>
        public float GetProgressWitchF(float nowTime) {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > Duration)
                nowTime = Duration;
            float value = StartF;
            if (!IsCustomEase)
            {
                value = MintEaseAction.GetEaseAction(EaseType, nowTime / Duration) * (EndF - StartF) + StartF;
            }
            else {
                value = TimeCurve.Evaluate(nowTime) * (EndF - StartF) + StartF;
            }
            return value;
        }

        /// <summary>
        /// 获取当前时间下的Vector3
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public Vector3 GetProgressWitchV3(float nowTime) {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > Duration)
                nowTime = Duration;

            if (!IsCustomEase)
            {
                return Vector3.Lerp(StartV3, EndV3, MintEaseAction.GetEaseAction(EaseType, nowTime / Duration));
            }
            else
            {

                return Vector3.Lerp(StartV3, EndV3, TimeCurve.Evaluate(nowTime));
            }
        }

        /// <summary>
        /// 获取当前时间下的Color
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public Color GetProgressWitchCor(float nowTime) {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > Duration)
                nowTime = Duration;

            if (!IsCustomEase)
            {
                return Color.Lerp(StartCor , EndCor , MintEaseAction.GetEaseAction(EaseType, nowTime / Duration));
            }
            else
            {
                return Color.Lerp(StartCor, EndCor, TimeCurve.Evaluate(nowTime / Duration));
            }
        }

        /// <summary>
        /// 获取当前时间下的Quaternion
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public Quaternion GetProgressWitchQ(float nowTime) {
            if (_startQ == null) {
                _startQ = Quaternion.Euler(StartV3);
                _endQ = Quaternion.Euler(EndV3);
            }

            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > Duration)
                nowTime = Duration;
            if (!IsCustomEase)
            {
                return Quaternion.Lerp(_startQ, _endQ, MintEaseAction.GetEaseAction(EaseType, nowTime / Duration));
            }
            else {
                return Quaternion.Lerp(_startQ, _endQ, TimeCurve.Evaluate(nowTime / Duration));
            }
        }

        public T GetProgress<T>(float nowTime)
        {
            if (typeof(T) == typeof(float))
            {
                object obj = GetProgressWitchF(nowTime);
                return (T)obj;
            }
            else if (typeof(T) == typeof(Vector3))
            {
                object obj = GetProgressWitchV3(nowTime);
                return (T)obj;
            }
            else if (typeof(T) == typeof(Color))
            {
                object obj = GetProgressWitchCor(nowTime);
                return (T)obj;
            }
            else if (typeof(T) == typeof(Quaternion))
            {
                object obj = GetProgressWitchQ(nowTime);
                return (T)obj;
            }
            return default(T);
        }

        public void SetStartValue<T>(T value)
        {
            if (typeof(T) == typeof(float))
            {
                StartF = (float)((object)value);
            }
            else if (typeof(T) == typeof(Vector3))
            {
                StartV3 = (Vector3)((object)value);
            }
            else if (typeof(T) == typeof(Color))
            {
                StartCor = (Color)((object)value);
            }
            else if (typeof(T) == typeof(Quaternion))
            {
                StartV3 = ((Quaternion)((object)value)).eulerAngles;
            }
            AutoStartValue = false;
        }
    }
}
