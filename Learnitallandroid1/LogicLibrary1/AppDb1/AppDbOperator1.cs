

using LogicLibrary1.AppAuth1;
using ToolsLib1.FirebaseClient1;

namespace LogicLibrary1.AppDb1
{
    public class AppDbOperator1 (
        IToolFirebaseDbOperations _dbOperations,
        IAppAuthentication        _authClient) : IAppDbOperator
    {
        public async Task AddBookAsync(
            string driveUrl,
            string title,
            string classfication,
            string desc,
            string course = "",
            string topic  = "")
        {
            var payload = new
            {
                DriveUrl       = driveUrl,
                Title          = title,
                Classification = classfication,
                Description    = desc,
                Course         = course,
                Topic          = topic
            };

            await _dbOperations.PostAsync(payload, "Books1");
        }

        public async Task AddUserBookAsync(string bookUid)
        {
            var user = _authClient.GetCurrentUser();

            var payload = new
            {
                History = new
                {
                    Books = new Dictionary<string, object>
                    {
                        [bookUid] = new
                        {
                            Date = DateTime.UtcNow
                        }
                    }
                }
            };

            await _dbOperations.PatchAsync(payload, $"Users1/{user}");
        }

        public async Task<List<T>> LoadAllBooksAsync<T>() where T : class, new()
        => await _dbOperations.GetListAsync<T>("Books1");
        
        public async Task<T> LoadThisBookAsync<T>(string bookUid)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveThisBookAsync(string bookUid)
        => await _dbOperations.RemoveUidAsync($"Books1/{bookUid}");
    }
}
