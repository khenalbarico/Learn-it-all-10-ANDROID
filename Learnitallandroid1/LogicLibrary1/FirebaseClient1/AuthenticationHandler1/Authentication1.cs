using Firebase.Auth;
using Models1.UserAuth;

namespace LogicLibrary1.FirebaseClient1.AuthenticationHandler1;

public sealed class Authentication1(FirebaseAuthClient _client) : IFirebaseAuth
{
    public async Task<CurrentUser> SignInWithEmailAndPasswordAsync(UserLogin payload)
    {
        var user = await _client.SignInWithEmailAndPasswordAsync(payload.Email, payload.Password);

        var currentUser = new CurrentUser
        {
            Uid = user.User.Uid,
            Token = user.User.Credential.IdToken,
        };

        return currentUser;
    }

    public async Task<CurrentUser> CreateUserWithEmailAndPasswordAsync(UserRegister payload)
    {
        var user = await _client.CreateUserWithEmailAndPasswordAsync(payload.Email, payload.Password);

        var currentUser = new CurrentUser
        {
            Uid = user.User.Uid,
            Token = user.User.Credential.IdToken,
        };

        return currentUser;
    }
}