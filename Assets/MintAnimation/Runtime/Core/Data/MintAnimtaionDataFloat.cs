namespace MintAnimation.Core
{
    public class MintAnimtaionDataFloat : MintAnimationDataBase<float>
    {
        public override float GetProgress(float nowTime)
        {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > Options.Duration)
                nowTime = Options.Duration;
            float value = StartValue;
            if (!Options.IsCustomEase)
            {
                value = MintEaseAction.GetEaseAction(Options.EaseType, nowTime / Options.Duration) * (EndValue - StartValue) + StartValue;
            }
            else {
                value = Options.TimeCurve.Evaluate(nowTime) * (EndValue - StartValue) + EndValue;
            }
            return value;
        }
    }
}