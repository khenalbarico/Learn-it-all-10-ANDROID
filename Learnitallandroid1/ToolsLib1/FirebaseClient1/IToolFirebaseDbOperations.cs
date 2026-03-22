using Firebase.Database;

namespace ToolsLib1.FirebaseClient1;

public interface IToolFirebaseDbOperations
{
    Task<T> GetAsync<T>(
        params string[] childPaths);
    Task<FirebaseObject<T>> PostAsync<T>(
        T item,
        params string[] childPaths);

    Task PatchAsync<T>(
        T item,
        params string[] childPaths);

    Task RemoveUidAsync(string uid);
}
