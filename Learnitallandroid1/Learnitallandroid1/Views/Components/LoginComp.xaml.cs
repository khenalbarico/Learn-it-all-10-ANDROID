using ToolsLib1.FirebaseClient1;

namespace Learnitallandroid1.Views.Components;

public partial class LoginComp : ContentView
{
    public LoginComp()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email    = EmailEntry.Text?.Trim() ?? string.Empty;
        var password = PasswordEntry.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            MessageLabel.Text = "Please enter your email and password.";
            return;
        }

        try
        {
            SetBusy(true);
            MessageLabel.Text = string.Empty;

            var auth = ResolveService<FirebaseAuth1>();
            await auth.SignInAsync(email, password);

            MessageLabel.TextColor = Color.FromArgb("#16A34A");
            MessageLabel.Text = "Login successful.";

            await Shell.Current.GoToAsync("//IndexView");
        }
        catch (Exception ex)
        {
            MessageLabel.TextColor = Color.FromArgb("#DC2626");
            MessageLabel.Text = ex.Message;
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void SetBusy(bool isBusy)
    {
        BusyIndicator.IsVisible = isBusy;
        BusyIndicator.IsRunning = isBusy;
    }

    private T ResolveService<T>()
    {
        var services = Handler?.MauiContext?.Services
            ?? throw new InvalidOperationException("Service provider not available.");

        return services.GetRequiredService<T>();
    }
}