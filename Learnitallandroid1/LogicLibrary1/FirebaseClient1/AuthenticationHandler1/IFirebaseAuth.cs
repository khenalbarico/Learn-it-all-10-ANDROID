using Firebase.Auth;
using Models1.UserAuth;

namespace LogicLibrary1.FirebaseClient1.AuthenticationHandler1;

public interface IFirebaseAuth
{
    Task<CurrentUser> SignInWithEmailAndPasswordAsync(UserLogin payload);
    Task<CurrentUser> CreateUserWithEmailAndPasswordAsync(UserRegister payload);
}
