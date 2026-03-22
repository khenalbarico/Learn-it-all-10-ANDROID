namespace Learnitallandroid1.Views.Menu;

public partial class IndexView : ContentPage
{
    private bool _isSidebarOpen;

    public IndexView()
    {
        InitializeComponent();

        SidebarView.MenuSelected += OnMenuSelected;
        SidebarView.SetActiveMenu("Home");
        ShowView("Home");
    }

    private async void OnMenuButtonClicked(object sender, EventArgs e)
    {
        if (_isSidebarOpen)
            await CloseSidebarAsync();
        else
            await OpenSidebarAsync();
    }

    private async void OnBackdropTapped(object sender, TappedEventArgs e)
    {
        await CloseSidebarAsync();
    }

    private async void OnMenuSelected(object? sender, string menuKey)
    {
        ShowView(menuKey);
        await CloseSidebarAsync();
    }

    private void ShowView(string menuKey)
    {
        switch (menuKey)
        {
            case "Home":
            default:
                HomeContentView.IsVisible = true;
                break;
        }
    }

    private async Task OpenSidebarAsync()
    {
        if (_isSidebarOpen)
            return;

        _isSidebarOpen = true;
        SidebarOverlay.IsVisible = true;

        SidebarContainer.TranslationX = -280;
        BackdropArea.Opacity = 0;

        await Task.WhenAll(
            SidebarContainer.TranslateTo(0, 0, 220, Easing.CubicOut),
            BackdropArea.FadeTo(1, 180, Easing.CubicOut)
        );
    }

    private async Task CloseSidebarAsync()
    {
        if (!_isSidebarOpen)
            return;

        await Task.WhenAll(
            SidebarContainer.TranslateTo(-280, 0, 220, Easing.CubicIn),
            BackdropArea.FadeTo(0, 180, Easing.CubicIn)
        );

        SidebarOverlay.IsVisible = false;
        _isSidebarOpen = false;
    }
}