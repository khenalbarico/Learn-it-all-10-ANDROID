using LogicLibrary1.AppAuth1;

namespace Learnitallandroid1.Views.Components;

public partial class SidebarComp : ContentView
{
    readonly IAppAuthentication _auth;

    public event EventHandler<string>? MenuSelected;

    public SidebarComp(IAppAuthentication appAuthentication)
    {
        InitializeComponent();
        _auth = appAuthentication;
    }

    private void OnSignOutClicked(object sender, EventArgs e)
    {
        _auth.SignOut();
        MenuSelected?.Invoke(this, "SignOut");
    }

    private void OnHomeClicked(object sender, EventArgs e)
    {
        SetActiveMenu("Home");
        MenuSelected?.Invoke(this, "Home");
    }

    private void OnLibraryClicked(object sender, EventArgs e)
    {
        SetActiveMenu("Library");
        MenuSelected?.Invoke(this, "Library");
    }

    public void SetActiveMenu(string selectedMenu)
    {
        bool isHome = selectedMenu == "Home";
        bool isLibrary = selectedMenu == "Library";

        HomeButton.BackgroundColor = isHome ? Colors.White : Colors.Transparent;
        HomeButton.TextColor = isHome ? Color.FromArgb("#512BD4") : Colors.White;

        LibraryButton.BackgroundColor = isLibrary ? Colors.White : Colors.Transparent;
        LibraryButton.TextColor = isLibrary ? Color.FromArgb("#512BD4") : Colors.White;
    }
}