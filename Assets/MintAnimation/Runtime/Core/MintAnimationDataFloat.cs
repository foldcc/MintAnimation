namespace MintAnimation.Core
{
    [System.Serializable]
    public class MintAnimationDataFloat : MintAnimationData<float>
    {
        public MintAnimationDataFloat()
        {
            Handler = new MintHandleFloat();
        }
    }
}