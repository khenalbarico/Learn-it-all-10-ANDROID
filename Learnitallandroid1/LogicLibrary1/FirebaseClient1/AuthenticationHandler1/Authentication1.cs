using Firebase.Auth;
using Models1.Config;
using Models1.UserAuth;
using System.Text;
using System.Text.Json;

namespace LogicLibrary1.FirebaseClient1.AuthenticationHandler1;

public sealed class Authentication1(FirebaseAuthClient _client, HttpClient _httpClient, AuthenticationConfig _authConfig) : IFirebaseAuth
{
    public async Task<CurrentUser> SignInWithEmailAndPasswordAsync(UserLogin payload)
    {
        var user = await _client.SignInWithEmailAndPasswordAsync(
                         payload.Email,
                         payload.Password);

        var currentUser = new CurrentUser
        {
            Uid   = user.User.Uid,
            Token = user.User.Credential.IdToken,
        };

        return currentUser;
    }

    public async Task<CurrentUser> CreateUserWithEmailAndPasswordAsync(UserRegister payload)
    {
        var user    = await _client.CreateUserWithEmailAndPasswordAsync(
                            payload.Email,
                            payload.Password);

        var idToken = await user.User.GetIdTokenAsync();

        await SendEmailVerificationAsync(idToken);

        _client.SignOut();

        return new CurrentUser
        {
            Uid   = user  .User.Uid,
            Token = string.Empty
        };
    }

    async Task SendEmailVerificationAsync(string idToken)
    {
        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={_authConfig.ApiKey}";

        var requestBody = new
        {
            requestType = "VERIFY_EMAIL",
            idToken
        };

        var       json     =       JsonSerializer.Serialize(requestBody);
        using var content  = new   StringContent           (json, Encoding.UTF8, "application/json");
        using var response = await _httpClient   .PostAsync(url, content);

        var responseText = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Failed to send verification email: {responseText}");
    }

    public async Task ResendEmailVerificationAsync(string email, string password)
    {
        var user    = await _client.SignInWithEmailAndPasswordAsync(email, password);

        var idToken = await user.User.GetIdTokenAsync();

        await SendEmailVerificationAsync(idToken);

        _client.SignOut();
    }
}