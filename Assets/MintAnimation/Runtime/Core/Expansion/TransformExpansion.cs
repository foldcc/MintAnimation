using UnityEngine;
namespace MintAnimation.Core
{
	public static class TransformExpansion
	{
		public static MintAnimationClip<Vector3> RotationTo(this Transform transform, Vector3 endRotation , float duration , MintEaseMethod easeTypoe = MintEaseMethod.Linear)
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
			return new MintAnimationClip<Vector3>(() => transform.localEulerAngles,
				rValue => transform.localEulerAngles = rValue, animationInfo);
		}
		
	}
}
