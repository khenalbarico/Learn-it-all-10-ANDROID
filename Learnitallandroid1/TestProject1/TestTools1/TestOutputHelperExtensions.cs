using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;
using LogicLibrary1.FirebaseClient1.AuthenticationHandler1;

namespace TestProject1.TestTools1;

internal static class TestOutputHelperExtensions
{
    private static IHost? Host;

    public static T Get<T>(this ITestOutputHelper ctx, Action<IServiceCollection>? svcModifier = null)
        where T : class
    {
        Host ??= new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton(_ =>
                {
                    var config = new FirebaseAuthConfig
                    {
                        ApiKey = "AIzaSyCIE5a5xvZrBur0kBTFBHAi0fN5EGWXwgk",
                        AuthDomain = "learn-it-all-mobile-app.firebaseapp.com", 
                        Providers =
                        [
                            new EmailProvider()
                        ],
                        UserRepository = new FileUserRepository("firebase_auth_cache")
                    };

                    return new FirebaseAuthClient(config);
                });

                services.AddSingleton<IFirebaseAuth, Authentication1>();

                svcModifier?.Invoke(services);
            })
            .Build();

        return Host.Services.GetRequiredService<T>();
    }
}