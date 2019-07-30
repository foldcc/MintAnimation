using UnityEngine;

namespace MintAnimation.Core
{
    [System.Serializable]
    public class MintAnimationDataColor : MintTweenDataBase<Color>
    {
        public MintAnimationDataColor()
        {
            Handler = new MintHandleColor();
        }
    }
}