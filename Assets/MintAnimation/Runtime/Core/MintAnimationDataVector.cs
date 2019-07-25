using UnityEngine;

namespace MintAnimation.Core
{
    [System.Serializable]
    public class MintAnimationDataVector : MintAnimationData<Vector3>
    {
        public MintAnimationDataVector()
        {
            Handler = new MintHandleVector3();
        }
    }
}