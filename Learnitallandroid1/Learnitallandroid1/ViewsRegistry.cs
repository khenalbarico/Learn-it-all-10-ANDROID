using Learnitallandroid1.Views.Auth;
using Learnitallandroid1.Views.Components;
using Learnitallandroid1.Views.Menu;

namespace Learnitallandroid1;

internal static class ViewsRegistry
{
    public static void AddViewsRegistry(this IServiceCollection svc)
    {
        svc.AddSingleton<AppShell>();

        svc.AddTransient<AuthView>();
        svc.AddTransient<LoginComp>();
        svc.AddTransient<RegisterComp>();

        svc.AddTransient<IndexView>();
        svc.AddTransient<HomeView>();
        svc.AddTransient<SidebarComp>();
    }
}