using ToolsLib1.FirebaseClient1;

namespace Learnitallandroid1;

public partial class AppShell : Shell
{
    private readonly IToolFirebaseAuthSession _authSession;
    private bool _initialized;

    public AppShell(IToolFirebaseAuthSession authSession)
    {
        InitializeComponent();
        _authSession = authSession;

        RouteRegistry.RegisterAll();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_initialized)
            return;

        _initialized = true;

        var hasValidSession = await _authSession.HasValidSessionAsync();

        if (!hasValidSession)
        {
            await GoToAsync("//AuthView");
        }
        else
        {
            await GoToAsync("//IndexView");
        }
    }
}