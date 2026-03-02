using System.Net.Http.Json;
using Firebase.Auth;
using Models1.Authentication;
using Models1.UserAuth;

namespace LogicLibrary1.FirebaseClient1.AuthenticationHandler1;

public sealed class Authentication1(FirebaseAuthClient _client) : IFirebaseAuth
{
    public async Task<CurrentUser> CreateUserWithEmailAndPasswordAsync(UserRegister payload)
    {
        var result = await _client.CreateUserWithEmailAndPasswordAsync(payload.Email, payload.Password);

        var idToken = result.User.Credential.IdToken;

        await SendEmailVerificationAsync(idToken);

        return new CurrentUser
        {
            Uid = result.User.Uid,
            Token = idToken,
        };
    }

    public async Task<CurrentUser> SignInWithEmailAndPasswordAsync(UserLogin payload)
    {
        var result = await _client.SignInWithEmailAndPasswordAsync(payload.Email, payload.Password);

        var idToken = result.User.Credential.IdToken;

        var isVerified = await GetEmailVerifiedAsync(idToken);

        if (!isVerified)
            throw new InvalidOperationException("Email not verified. Please verify your email first.");

        return new CurrentUser
        {
            Uid = result.User.Uid,
            Token = idToken,
        };
    }

    private async Task SendEmailVerificationAsync(string idToken)
    {
        using var http = new HttpClient();

        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={_apiKey}";

        var res = await http.PostAsJsonAsync(url, new SendOobCodeRequest
        {
            IdToken = idToken,
            RequestType = "VERIFY_EMAIL"
        });

        res.EnsureSuccessStatusCode();
    }

    private async Task<bool> GetEmailVerifiedAsync(string idToken)
    {
        using var http = new HttpClient();

        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={_apiKey}";

        var res = await http.PostAsJsonAsync(url, new { idToken });

        res.EnsureSuccessStatusCode();

        var json = await res.Content.ReadFromJsonAsync<AccountLookupResponse>();

        return json?.Users?.FirstOrDefault()?.EmailVerified == true;
    }

    private sealed class AccountLookupResponse
    {
        public List<AccountLookupUser>? Users { get; set; }
    }

    private sealed class AccountLookupUser
    {
        public bool EmailVerified { get; set; }
    }
}