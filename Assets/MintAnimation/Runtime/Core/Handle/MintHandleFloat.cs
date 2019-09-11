namespace MintAnimation.Core
{
    public class MintHandleFloat : IMintTweenBehaviour<float>
    {
        public float GetProgress(float nowTime , MintTweenDataBase<float> dataBase)
        {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > dataBase.Duration)
                nowTime = dataBase.Duration;
            float value = dataBase.StartValue;
            if (!dataBase.IsCustomEase)
            {
                value = MintEaseAction.GetEaseAction(dataBase.EaseType, nowTime / dataBase.Duration) * (dataBase.EndValue - dataBase.StartValue) + dataBase.StartValue;
            }
            else {
                value = dataBase.TimeCurve.Evaluate(nowTime / dataBase.Duration) * (dataBase.EndValue - dataBase.StartValue) + dataBase.StartValue;
            }
            return value;
        }
    }
}