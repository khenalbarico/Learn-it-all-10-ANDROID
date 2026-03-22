using LogicLibrary1.AppAuth1;
using LogicLibrary1.AppDb1;
using LogicLibrary1.AppInit1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToolsLib1.FirebaseClient1;
using Xunit.Abstractions;

namespace TestProject1.TestTools1;

internal static class TestOutputHelperExtensions
{
    private static IHost? Host;

    public static T Get<T>(this ITestOutputHelper ctx)
        where T : class
    {
        Host ??= new HostBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("test.settings.json");
            })
            .ConfigureServices((context, svc) =>
            {
                var cfg = context.Configuration;

                var appCfg = new AppCfg1();
                cfg.GetSection("FirebaseAuthentication").Bind(appCfg);

                svc.AddSingleton<IFirebaseCfg>(appCfg);

                svc.AddSingleton<IAppAuthentication, AppAuthentication1>();
                svc.AddSingleton<IAppDbOperator, AppDbOperator1>();

                svc.AddHttpClient();

                svc.AddSingleton<IToolAuthEmailProvider,    FirebaseAuth1>();
                svc.AddSingleton<IToolFirebaseDbOperations, FirebaseRealtimeDb1>();
            })
            .Build();

        return Host.Services.GetRequiredService<T>();
    }
}