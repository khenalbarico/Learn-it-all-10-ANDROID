using LogicLibrary1.Initializers1;
using Microsoft.Extensions.Logging;


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

        using var stream = FileSystem.OpenAppPackageFileAsync("appsettings.json")
            .GetAwaiter()
            .GetResult();

        var authConfig = ConfigLoader1.LoadAuthenticationConfig(stream);

        builder.Services.AddSingleton(authConfig);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}