namespace LogicLibrary1.LoadingHandler1;

public interface ILoadingScreenManager
{
    Type? ActiveClassType { get; }

    void ShowLoadingScreen(Type pageType);
    bool IsActive(Type pageType);
    void CloseLoadingScreen();
}