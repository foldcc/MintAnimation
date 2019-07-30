using UnityEngine;
namespace MintAnimation.Core
{
	public static class TransformExpansion
	{
		public static MintTweener<Vector3> RotationTo(this Transform transform, Vector3 endRotation , float duration , MintEaseMethod easeTypoe = MintEaseMethod.Linear)
		{
			MintTweenDataBase<Vector3> tweenInfo = new MintTweenDataBase<Vector3>(){
				EaseType = easeTypoe,
				Duration =  duration
			};
			tweenInfo.StartValue = transform.localRotation.eulerAngles;
			tweenInfo.EndValue = endRotation;
			return new MintTweener<Vector3>(() => transform.localEulerAngles,
				rValue => transform.localEulerAngles = rValue, tweenInfo);
		}

        public static MintTweener<Vector3> MoveTo(this Transform transform, Vector3 endPosition, float duration, MintEaseMethod easeTypoe = MintEaseMethod.Linear)
        {
            MintTweenDataBase<Vector3> tweenInfo = new MintTweenDataBase<Vector3>(){
	            EaseType = easeTypoe,
	            Duration = duration
            };
            tweenInfo.StartValue = transform.localPosition;
            tweenInfo.EndValue = endPosition;
            
            return new MintTweener<Vector3>(() => transform.localPosition,
                rValue => transform.localPosition = rValue, tweenInfo);
        }
    }
}
