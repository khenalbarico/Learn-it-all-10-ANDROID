namespace LogicLibrary1.LoadingHandler1;

public interface ILoadingScreenManager
{
    Type? ActiveClassType { get; }
    void ShowLoadingScreen<T>();
    void CloseLoadingScreen();
    bool IsActive<T>();
}
