using UnityEngine;

namespace MintAnimation.Core
{
    [System.Serializable]
    public class MintAnimationDataVector : MintTweenDataBase<Vector3>
    {
        public MintAnimationDataVector()
        {
            Handler = new MintHandleVector3();
        }
    }
}