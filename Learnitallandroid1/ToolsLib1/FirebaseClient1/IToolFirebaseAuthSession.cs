namespace ToolsLib1.FirebaseClient1;

public interface IToolFirebaseAuthSession
{
    Task<bool> HasValidSessionAsync();
    void SignOut();
}
