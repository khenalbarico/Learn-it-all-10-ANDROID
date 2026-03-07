namespace LogicLibrary1.SearchHandler1;

public  interface ISearchManager
{
    string SearchTerm { get; set; }
    Task<T>PerformSearchAsync<T>(); 
}
