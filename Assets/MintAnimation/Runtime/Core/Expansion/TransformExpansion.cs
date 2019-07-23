using UnityEngine;
namespace MintAnimation.Core
{
	public static class TransformExpansion
	{
		public static MintTweener<Vector3> RotationTo(this Transform transform, Vector3 endRotation , float duration , MintEaseMethod easeTypoe = MintEaseMethod.Linear)
		{
			MintAnimationDataBase<Vector3> animationInfo = new MintAnimationDataVector3();
			animationInfo.StartValue = transform.localRotation.eulerAngles;
			animationInfo.EndValue = endRotation;
			animationInfo.Options = new MintAnimationOptions
			{
				EaseType = easeTypoe,
				Duration =  duration,
				AutoStartValue = false
			};
			return new MintTweener<Vector3>(() => transform.localEulerAngles,
				rValue => transform.localEulerAngles = rValue, animationInfo);
		}

        public static MintTweener<Vector3> MoveTo(this Transform transform, Vector3 endPosition, float duration, MintEaseMethod easeTypoe = MintEaseMethod.Linear)
        {
            MintAnimationDataBase<Vector3> animationInfo = new MintAnimationDataVector3();
            animationInfo.StartValue = transform.localPosition;
            animationInfo.EndValue = endPosition;
            animationInfo.Options = new MintAnimationOptions
            {
                EaseType = easeTypoe,
                Duration = duration,
                AutoStartValue = false
            };
            return new MintTweener<Vector3>(() => transform.localPosition,
                rValue => transform.localPosition = rValue, animationInfo);
        }
    }
}
