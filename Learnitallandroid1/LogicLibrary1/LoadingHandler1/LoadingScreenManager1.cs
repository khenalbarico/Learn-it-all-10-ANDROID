namespace LogicLibrary1.LoadingHandler1;

public class LoadingScreenManager1 : ILoadingScreenManager
{
    public Type? ActiveClassType { get; private set; }

    public void ShowLoadingScreen(Type pageType)
        => ActiveClassType = pageType;

    public bool IsActive(Type pageType)
        => ActiveClassType == pageType;

    public void CloseLoadingScreen()
        => ActiveClassType = null;
}