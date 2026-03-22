namespace Learnitallandroid1.Views.Auth;

public partial class AuthView : ContentPage
{
    public AuthView()
    {
        InitializeComponent();
        SetActiveTab(isLogin: true);
    }

    private void OnLoginTabClicked(object sender, EventArgs e)
        => SetActiveTab(true);

    private void OnRegisterTabClicked(object sender, EventArgs e)
        => SetActiveTab(false);

    private void SetActiveTab(bool isLogin)
    {
        LoginCompView.IsVisible    = isLogin;
        RegisterCompView.IsVisible = !isLogin;

        LoginTabButton.BackgroundColor = isLogin ? Colors.White : Colors.Transparent;
        LoginTabButton.TextColor       = isLogin ? Color.FromArgb("#512BD4") : Colors.White;

        RegisterTabButton.BackgroundColor = !isLogin ? Colors.White : Colors.Transparent;
        RegisterTabButton.TextColor       = !isLogin ? Color.FromArgb("#512BD4") : Colors.White;
    }
}