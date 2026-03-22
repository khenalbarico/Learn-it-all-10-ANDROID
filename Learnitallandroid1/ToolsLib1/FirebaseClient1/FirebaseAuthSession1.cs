namespace ToolsLib1.FirebaseClient1;

public class FirebaseAuthSession1 (FirebaseAuth1 _authClient) : IToolFirebaseAuthSession
{
    public async Task<bool> HasValidSessionAsync()
    {
        try
        {
            return await _authClient.HasValidSessionAsync();
        }
        catch
        {
            return false;
        }
    }

    public void SignOut() => _authClient.SignOut();
}
