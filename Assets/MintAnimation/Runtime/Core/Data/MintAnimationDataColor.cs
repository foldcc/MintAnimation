using UnityEngine;

namespace MintAnimation.Core
{
    public class MintAnimationDataColor : MintAnimationDataBase<Color>
    {
        public override Color GetProgress(float nowTime)
        {
            if (nowTime < 0)
                nowTime = 0;
            else if (nowTime > Options.Duration)
                nowTime = Options.Duration;

            if (!Options.IsCustomEase)
            {
                return MintEaseAction.GetEaseAction(Options.EaseType, nowTime / Options.Duration) * (EndValue - StartValue) + StartValue;
            }
            else
            {
                return Options.TimeCurve.Evaluate(nowTime / Options.Duration) * (EndValue - StartValue) + StartValue;
            }
        }
    }
}