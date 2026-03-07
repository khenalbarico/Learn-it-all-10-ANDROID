namespace Learnitallandroid1.Views.Authentication;

public partial class LoginRegister : AuthPage1
{
    private bool _isLoginSelected = true;
    private bool _isSwitching;

    public LoginRegister()
    {
        InitializeComponent();
        ApplyTabState();
    }

    private async void OnLoginTabClicked(object sender, EventArgs e)
    {
        if (_isLoginSelected || _isSwitching)
            return;

        _isLoginSelected = true;
        await SwitchSectionAsync();
    }

    private async void OnRegisterTabClicked(object sender, EventArgs e)
    {
        if (!_isLoginSelected || _isSwitching)
            return;

        _isLoginSelected = false;
        await SwitchSectionAsync();
    }

    private async Task SwitchSectionAsync()
    {
        try
        {
            _isSwitching = true;

            if (_isLoginSelected)
            {
                await RegisterSection.FadeToAsync(0, 120);
                RegisterSection.IsVisible = false;

                ApplyTabState();

                LoginSection.Opacity = 0;
                LoginSection.IsVisible = true;
                await LoginSection.FadeToAsync(1, 180);
            }
            else
            {
                await LoginSection.FadeToAsync(0, 120);
                LoginSection.IsVisible = false;

                ApplyTabState();

                RegisterSection.Opacity = 0;
                RegisterSection.IsVisible = true;
                await RegisterSection.FadeToAsync(1, 180);
            }
        }
        finally
        {
            _isSwitching = false;
        }
    }

    private void ApplyTabState()
    {
        if (_isLoginSelected)
        {
            LoginTabButton.BackgroundColor = Color.FromArgb("#FF9A4D");
            LoginTabButton.TextColor = Colors.White;

            RegisterTabButton.BackgroundColor = Colors.Transparent;
            RegisterTabButton.TextColor = Color.FromArgb("#0A58B6");
        }
        else
        {
            RegisterTabButton.BackgroundColor = Color.FromArgb("#0A58B6");
            RegisterTabButton.TextColor = Colors.White;

            LoginTabButton.BackgroundColor = Colors.Transparent;
            LoginTabButton.TextColor = Color.FromArgb("#FF9A4D");
        }
    }
}