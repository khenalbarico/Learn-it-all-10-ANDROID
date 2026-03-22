using LogicLibrary1.AppAuth1;
using LogicLibrary1.AppDb1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToolsLib1.FirebaseClient1;

namespace LogicLibrary1;

public static class SvcRegistry1
{
    public static void AddSvcRegistry(this IServiceCollection svc, IConfiguration cfg)
    {
        svc.AddHttpClient();

        svc.AddAppCfgRegistry(cfg);

        svc.AddSingleton<FirebaseAuth1>();
        svc.AddSingleton<FirebaseRealtimeDb1>();
        svc.AddSingleton<FirebaseAuthSession1>();

        svc.AddSingleton<IToolAuthEmailProvider>   (sp => sp.GetRequiredService<FirebaseAuth1>());
        svc.AddSingleton<IToolFirebaseDbOperations>(sp => sp.GetRequiredService<FirebaseRealtimeDb1>());
        svc.AddSingleton<IToolFirebaseAuthSession> (sp => sp.GetRequiredService<FirebaseAuthSession1>());

        svc.AddSingleton<IAppAuthentication, AppAuthentication1>();
        svc.AddSingleton<IAppDbOperator,     AppDbOperator1>();
    }
}