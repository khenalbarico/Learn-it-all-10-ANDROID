using LogicLibrary1.AppInit1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToolsLib1.FirebaseClient1;

namespace LogicLibrary1;

public static class AppCfgRegistry1
{
    public static void AddAppCfgRegistry(this IServiceCollection svc, IConfiguration cfg)
    {
        var appCfg = new AppCfg1();
        cfg.GetSection("FirebaseAuthentication").Bind(appCfg);

        svc.AddSingleton<IFirebaseCfg>(appCfg);
    }
}