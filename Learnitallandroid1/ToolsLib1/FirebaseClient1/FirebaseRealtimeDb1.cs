using Firebase.Database;
using Firebase.Database.Query;

namespace ToolsLib1.FirebaseClient1;

public class FirebaseRealtimeDb1(IFirebaseCfg _cfg) : IToolFirebaseDbOperations
{
    readonly FirebaseClient _dbClient = _cfg.CreateDbClient();

    private ChildQuery BuildQuery(params string[] childPaths)
    {
        ChildQuery query = _dbClient.Child(childPaths[0]);

        for (int i = 1; i < childPaths.Length; i++)
        {
            query = query.Child(childPaths[i]);
        }

        return query;
    }

    public async Task<T> GetAsync<T>(params string[] childPaths)
    {
        var query = BuildQuery(childPaths);
        return await query.OnceSingleAsync<T>();
    }

    public async Task<FirebaseObject<T>> PostAsync<T>(T item, params string[] childPaths)
    {
        var query = BuildQuery(childPaths);
        return await query.PostAsync(item);
    }

    public async Task PatchAsync<T>(T item, params string[] childPaths)
    {
        var query = BuildQuery(childPaths);
        await query.PutAsync(item);
    }

    public async Task RemoveUidAsync(string uid)
    {
        await _dbClient
            .Child("Books1")
            .Child(uid)
            .DeleteAsync();
    }
}