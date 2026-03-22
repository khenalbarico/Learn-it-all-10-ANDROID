using ToolsLib1.FirebaseClient1;

namespace Learnitallandroid1.Views.Components;

public partial class RegisterComp : ContentView
{
    public RegisterComp()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim() ?? string.Empty;
        var password = PasswordEntry.Text ?? string.Empty;
        var confirmPassword = ConfirmPasswordEntry.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            MessageLabel.TextColor = Color.FromArgb("#DC2626");
            MessageLabel.Text = "Please complete all fields.";
            return;
        }

        if (password != confirmPassword)
        {
            MessageLabel.TextColor = Color.FromArgb("#DC2626");
            MessageLabel.Text = "Passwords do not match.";
            return;
        }

        if (password.Length < 6)
        {
            MessageLabel.TextColor = Color.FromArgb("#DC2626");
            MessageLabel.Text = "Password must be at least 6 characters.";
            return;
        }

        try
        {
            SetBusy(true);
            MessageLabel.Text = string.Empty;

            var auth = ResolveService<FirebaseAuth1>();
            await auth.SignUpAsync(email, password);

            MessageLabel.TextColor = Color.FromArgb("#16A34A");
            MessageLabel.Text = "Registration successful. Please verify your email before logging in.";

            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            ConfirmPasswordEntry.Text = string.Empty;
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