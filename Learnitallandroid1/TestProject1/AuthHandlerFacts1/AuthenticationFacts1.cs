using AwesomeAssertions;
using LogicLibrary1.FirebaseClient1.AuthenticationHandler1;
using Models1.UserAuth;
using TestProject1.TestTools1;
using Xunit.Abstractions;

namespace TestProject1.AuthHandlerFacts1;

public class AuthenticationFacts1 (ITestOutputHelper _ctx)
{
    [Fact] public async Task Firebase_SignIn()
    {
        //Arrange
        var loginPayload = new UserLogin
        {
            Email = "test@test.com",
            Password = "test123456"
        };

        var _sut = _ctx.Get<IFirebaseAuth>();

        //Act
        var result = await _sut.SignInWithEmailAndPasswordAsync(loginPayload);


        //Assert
        result.Uid.Should().NotBeNullOrEmpty();
    }

    [Fact] public async Task Firebase_SignUp()
    {
        //Arrange
        var registerPayload = new UserRegister
        {
            Email = "test@test.com",
            Password = "test123456"
        };

        var _sut = _ctx.Get<IFirebaseAuth>();

        //Act
        var result = await _sut.CreateUserWithEmailAndPasswordAsync(registerPayload);

        //Assert
        result.Uid.Should().NotBeNullOrEmpty();
    }
}
