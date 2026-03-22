using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;

namespace ToolsLib1.FirebaseClient1;

internal static class FirebaseClientFactory1
{
    static readonly object _sync = new();
    static FirebaseAuthClient? _authClient;

    public static FirebaseAuthClient CreateAuthClient(this IFirebaseCfg cfg)
    {
        if (_authClient != null)
            return _authClient;

        lock (_sync)
        {
            _authClient ??= new FirebaseAuthClient(new FirebaseAuthConfig
            {
                ApiKey = cfg.FirebaseApiKey,
                AuthDomain = cfg.FirebaseAuthDomain,
                Providers =
                [
                    new EmailProvider()
                ]
            });

            return _authClient;
        }
    }

    public static FirebaseClient CreateDbClient(this IFirebaseCfg cfg)
    {
        var authClient = cfg.CreateAuthClient();

        return new FirebaseClient(cfg.FirebaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = async () =>
            {
                var user = authClient.User;

                if (user == null)
                {
                    throw new InvalidOperationException(
                        "Unable to acquire Firebase ID token. Make sure the user is signed in.");
                }

                return await user.GetIdTokenAsync();
            }
        });
    }
}