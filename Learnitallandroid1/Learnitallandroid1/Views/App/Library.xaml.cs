namespace Learnitallandroid1.Views.App;

public partial class Library : ContentPage
{
    private bool _isMenuOpen;

    public Library()
    {
        InitializeComponent();
    }

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        if (_isMenuOpen)
            await CloseMenu();
        else
            await OpenMenu();
    }

    private async void OnOverlayTapped(object sender, TappedEventArgs e)
    {
        if (_isMenuOpen)
            await CloseMenu();
    }

    private async Task OpenMenu()
    {
        MenuPanel.IsVisible = true;
        Overlay.IsVisible = true;

        await Task.WhenAll(
            MenuPanel.TranslateTo(0, 0, 250, Easing.CubicOut),
            Overlay.FadeTo(1, 250, Easing.CubicOut)
        );

        _isMenuOpen = true;
    }

    private async Task CloseMenu()
    {
        await Task.WhenAll(
            MenuPanel.TranslateTo(-220, 0, 250, Easing.CubicIn),
            Overlay.FadeTo(0, 250, Easing.CubicIn)
        );

        MenuPanel.IsVisible = false;
        Overlay.IsVisible = false;

        _isMenuOpen = false;
    }
}