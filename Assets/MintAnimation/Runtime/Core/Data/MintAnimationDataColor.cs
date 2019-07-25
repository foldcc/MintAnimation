using UnityEngine;

namespace MintAnimation.Core
{
    [System.Serializable]
    public class MintAnimationDataColor : MintAnimationData<Color>
    {
        public MintAnimationDataColor()
        {
            Handler = new MintHandleColor();
        }
    }
}