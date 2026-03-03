using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models1.Config;

namespace Learnitallandroid1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var appConfig = LoadJsonFromAppPackage("appsettings.json");
        builder.Configuration.AddConfiguration(appConfig);

        var mapped = builder.Configuration
            .GetSection("FirebaseAuthentication")
            .Get<AuthenticationConfig>()
            ?? new AuthenticationConfig();

        builder.Services.AddSingleton(mapped);

        builder.Services.AddSingleton(sp =>
        {
            var mappedConfig = sp.GetRequiredService<AuthenticationConfig>();

            var firebaseClientConfig = new FirebaseAuthConfig
            {
                ApiKey = mappedConfig.ApiKey,
                AuthDomain = mappedConfig.AuthDomain,
                Providers =
                [
                    new EmailProvider()
                ],
                UserRepository = new FileUserRepository("firebase_auth_cache")
            };

            return new global::Firebase.Auth.FirebaseAuthClient(firebaseClientConfig);
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static IConfiguration LoadJsonFromAppPackage(string fileName)
    {
        using var stream = FileSystem.OpenAppPackageFileAsync(fileName)
            .GetAwaiter()
            .GetResult();

        return new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
    }
}