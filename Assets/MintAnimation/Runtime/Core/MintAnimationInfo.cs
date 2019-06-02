using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation.Core
{

    [System.Serializable]
    public class MintAnimationInfo
    {
        public float                Duration = 0.35f;

        public bool                 IsBack;
        public bool                 IsLoop;
        public int                  LoopCount;
       
        public bool                 IsCustomEase = false;
        public MintEaseMethod       EaseType = MintEaseMethod.OutBack;
        public AnimationCurve       TimeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public bool                 AutoStartValue = true;

        public Color                StartCor    = Color.white;
        public Color                EndCor      = Color.white;

        public float                StartF;
        public float                EndF;

        public Vector3              StartV3;
        public Vector3              EndV3;

        public Quaternion          StartQ;
        public Quaternion          EndQ;

        public DriveEnum            DriveType = DriveEnum.Globa;
        public UpdaterTypeEnum      UpdaterTypeEnum = UpdaterTypeEnum.Update;
        public MintDriveComponent   CustomDrive;

        /// <summary>
        /// 获取当前时间下的float
        /// </summary>
        /// <param name="nowTime">当前时间 [0,Duration]</param>
        /// <returns></returns>
        float GetProgressWitchF(float nowTime) {
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
        Vector3 GetProgressWitchV3(float nowTime) {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > Duration)
                nowTime = Duration;

            if (!IsCustomEase)
            {
                return MintEaseAction.GetEaseAction(EaseType, nowTime / Duration) * (EndV3 - StartV3) + StartV3;
            }
            else
            {
                return TimeCurve.Evaluate(nowTime / Duration) * (EndV3 - StartV3) + StartV3;
            }
        }

        /// <summary>
        /// 获取当前时间下的Color
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        Color GetProgressWitchCor(float nowTime) {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > Duration)
                nowTime = Duration;

            if (!IsCustomEase)
            {
                return MintEaseAction.GetEaseAction(EaseType, nowTime / Duration) * (EndCor - StartCor) + StartCor;
            }
            else
            {
                return TimeCurve.Evaluate(nowTime / Duration) * (EndCor - StartCor) + StartCor;
            }
        }

        /// <summary>
        /// 获取当前时间下的Quaternion
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        Quaternion GetProgressWitchQ(float nowTime) {
//            if (nowTime < 0)
//                nowTime = 0;
//            else if (nowTime > Duration)
//                nowTime = Duration;
//            if (!IsCustomEase)
//            {
//                return Quaternion.Slerp(StartQ, EndQ, MintEaseAction.GetEaseAction(EaseType, nowTime / Duration));
//            }
//            else {
//                return Quaternion.Slerp(StartQ, EndQ, TimeCurve.Evaluate(nowTime / Duration));
//            }
            return Quaternion.Euler(GetProgressWitchV3(nowTime));
        }

        public T GetProgress<T>(float nowTime)
        {
            if (typeof(T) == typeof(float))
            {
                object obj = GetProgressWitchF(nowTime);
                return (T)obj;
            }
            if (typeof(T) == typeof(Vector3))
            {
                object obj = GetProgressWitchV3(nowTime);
                return (T)obj;
            }
            if (typeof(T) == typeof(Color))
            {
                object obj = GetProgressWitchCor(nowTime);
                return (T)obj;
            }
            if (typeof(T) == typeof(Quaternion))
            {
                object obj = GetProgressWitchQ(nowTime);
                return (T)obj;
            }
            return default;
        }

        public void SetStartValue<T>(T value)
        {
            if (typeof(T) == typeof(float))
            {
                StartF = (float)(object)value;
            }
            else if (typeof(T) == typeof(Vector3))
            {
                StartV3 = (Vector3)(object)value;
            }
            else if (typeof(T) == typeof(Color))
            {
                StartCor = (Color)(object)value;
            }
            else if (typeof(T) == typeof(Quaternion))
            {
                StartQ = (Quaternion)(object)value;
            }
        }
    }
}
