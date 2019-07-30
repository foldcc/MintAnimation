namespace MintAnimation.Core
{
    [System.Serializable]
    public class MintAnimationDataFloat : MintTweenDataBase<float>
    {
        public MintAnimationDataFloat()
        {
            Handler = new MintHandleFloat();
        }
    }
}