using Firebase.Auth;
using ToolsLib1.FirebaseClient1;

namespace LogicLibrary1.AppAuth1;

public class AppAuthentication1 (IToolAuthEmailProvider _authClient) : IAppAuthentication
{
    public string GetCurrentUser()
    => _authClient.GetCurrentUser();
    
    public async Task<UserCredential> SignInAsync(string email, string pass)
    => await _authClient.SignInAsync(email, pass);
    
    public void SignOut()
    => _authClient.SignOut();

    public async Task<UserCredential> SignUpAsync(string email, string pass)
    => await _authClient.SignUpAsync(email, pass);
}
