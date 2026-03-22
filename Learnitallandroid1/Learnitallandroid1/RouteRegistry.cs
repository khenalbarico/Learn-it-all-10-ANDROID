using Learnitallandroid1.Views.Auth;
using Learnitallandroid1.Views.Menu;

namespace Learnitallandroid1;

public static class RouteRegistry
{
    private static bool _registered;

    public static void RegisterAll()
    {
        if (_registered)
            return;

        Register<AuthView>(nameof(AuthView));
        Register<IndexView>(nameof(IndexView));

        _registered = true;
    }

    private static void Register<TView>(string route) where TView : Page
    {
        Routing.RegisterRoute(route, typeof(TView));
    }
}