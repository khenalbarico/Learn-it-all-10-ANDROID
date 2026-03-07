using Learnitallandroid1.Views.Authentication;

namespace Learnitallandroid1;

public static class RouteRegister1
{
    public static void RegisterAll()
    {
        Routing.RegisterRoute(nameof(LoginRegister), typeof(LoginRegister));
    }
}