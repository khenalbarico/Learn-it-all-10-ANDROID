using Microsoft.Extensions.Configuration;
using Models1.Config;

namespace LogicLibrary1.Initializers1;

public static class ConfigLoader1
{
    public static AuthenticationConfig LoadAuthenticationConfig(Stream stream)
    {
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        return config
            .GetSection("FirebaseAuthentication")
            .Get<AuthenticationConfig>() ?? new AuthenticationConfig();
    }
}