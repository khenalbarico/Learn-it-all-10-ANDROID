using Learnitallandroid1.Views.Components;

namespace Learnitallandroid1.Views.Menu;

public partial class IndexView : ContentPage
{
    readonly SidebarComp _sidebarComp;

    public IndexView(SidebarComp sidebarComp)
    {
        InitializeComponent();

        _sidebarComp = sidebarComp;
        SidebarHost.Content = _sidebarComp;

        _sidebarComp.MenuSelected += OnMenuSelected;
        _sidebarComp.SetActiveMenu("Home");
    }

    private async void OnMenuSelected(object? sender, string menu)
    {
        if (menu == "Home")
        {
            HomeContentView.IsVisible = true;
            LibraryContentView.IsVisible = false;
        }
        else if (menu == "Library")
        {
            HomeContentView.IsVisible = false;
            LibraryContentView.IsVisible = true;
        }
        else if (menu == "SignOut")
        {
            await Shell.Current.GoToAsync("//AuthView");
        }

        await CloseSidebar();
    }

    private async void OnMenuButtonClicked(object sender, EventArgs e)
    {
        SidebarOverlay.IsVisible = true;
        await Task.WhenAll(
            SidebarContainer.TranslateTo(0, 0, 250, Easing.CubicOut),
            BackdropArea.FadeTo(1, 250, Easing.CubicOut)
        );
    }

    private async void OnBackdropTapped(object sender, TappedEventArgs e)
    {
        await CloseSidebar();
    }

    private async Task CloseSidebar()
    {
        await Task.WhenAll(
            SidebarContainer.TranslateTo(-280, 0, 250, Easing.CubicIn),
            BackdropArea.FadeTo(0, 250, Easing.CubicIn)
        );

        SidebarOverlay.IsVisible = false;
    }
}