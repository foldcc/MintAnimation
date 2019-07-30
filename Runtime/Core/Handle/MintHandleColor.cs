using UnityEngine;

namespace MintAnimation.Core
{
    public class MintHandleColor : IMintTweenBehaviour<Color>
    {
        public Color GetProgress(float nowTime , MintTweenDataBase<Color> dataBase)
        {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > dataBase.Duration)
                nowTime = dataBase.Duration;

            if (!dataBase.IsCustomEase)
            {
                return MintEaseAction.GetEaseAction(dataBase.EaseType, nowTime / dataBase.Duration) * (dataBase.EndValue - dataBase.StartValue) + dataBase.StartValue;
            }
            else
            {
                return dataBase.TimeCurve.Evaluate(nowTime / dataBase.Duration) * (dataBase.EndValue - dataBase.StartValue) + dataBase.StartValue;
            }
        }
    }
}