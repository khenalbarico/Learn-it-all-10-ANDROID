using LogicLibrary1.AppAuth1;
using Microsoft.Extensions.Configuration;
using TestProject1.TestTools1;
using Xunit.Abstractions;

namespace TestProject1.FirebaseAuthFacts1;

public class AuthenticationFacts1 (ITestOutputHelper _ctx)
{
    [Fact] public async Task Firebase_SignIn()
    {
        var payload = new
        {
            Email    = "khenalbarico05@gmail.com",
            Password = "test123456"
        };

        var _sut = _ctx.Get<IAppAuthentication>();

        await _sut.SignInAsync(payload.Email, payload.Password);

        var user = _sut.GetCurrentUser();
    }

    [Fact] public async Task Firebase_Register()
    {
        var payload = new
        {
            Email = "khenalbarico05@gmail.com",
            Password = "test123456"
        }; 

        var _sut = _ctx.Get<IAppAuthentication>();

        await _sut.SignUpAsync(payload.Email, payload.Password);

        var user = _sut.GetCurrentUser();
    }

    [Fact] public async Task Firebase_SignOut()
    {
        var _sut = _ctx.Get<IAppAuthentication>();

        _sut.SignOut();
    }
}
