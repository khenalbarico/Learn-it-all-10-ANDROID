namespace LogicLibrary1.AppDb1;

public interface IAppDbOperator
{
    Task AddUserBookAsync(string bookUid);
    Task AddBookAsync(
        string driveUrl,
        string title,
        string classfication,
        string desc,
        string course = "",
        string topic  = "");

    Task RemoveThisBookAsync(string bookUid);
}
