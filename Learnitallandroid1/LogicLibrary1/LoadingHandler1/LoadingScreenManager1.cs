namespace LogicLibrary1.LoadingHandler1;

public class LoadingScreenManager1 : ILoadingScreenManager
{
    public Type? ActiveClassType { get; private set; }

    public void ShowLoadingScreen<T>()
        => ActiveClassType = typeof(T);

    public bool IsActive<T>()
        => ActiveClassType == typeof(T);

    public void CloseLoadingScreen()
        => ActiveClassType = null;
}
