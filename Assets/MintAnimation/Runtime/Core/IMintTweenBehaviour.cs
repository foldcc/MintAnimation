namespace MintAnimation.Core
{
    public interface IMintTweenBehaviour<T>
    {
        T GetProgress(float nowTime , MintTweenDataBase<T> dataBase);
    }
}