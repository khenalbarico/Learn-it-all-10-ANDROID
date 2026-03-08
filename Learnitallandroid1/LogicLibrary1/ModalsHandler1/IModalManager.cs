namespace LogicLibrary1.ModalsHandler1;

public interface IModalManager
{
    Type? Caller { get; set; }
    string Message { get; set; }
    void ShowModal(string message, Type? caller = null);
    void CloseModal();
}
