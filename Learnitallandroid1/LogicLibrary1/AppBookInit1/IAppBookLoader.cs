namespace LogicLibrary1.AppBookInit1;

public interface IAppBookLoader
{
    Task<T> LoadThisBookAsync<T>(string bookUid);
    Task<T> LoadAllBooksAsync<T>();
}
