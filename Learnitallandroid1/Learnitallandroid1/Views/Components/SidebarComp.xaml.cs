using System;

namespace Learnitallandroid1.Views.Components;

public partial class SidebarComp : ContentView
{
    public event EventHandler<string>? MenuSelected;

    public SidebarComp()
    {
        InitializeComponent();
    }

    private void OnHomeClicked(object sender, EventArgs e)
    {
        SetActiveMenu("Home");
        MenuSelected?.Invoke(this, "Home");
    }

    public void SetActiveMenu(string selectedMenu)
    {
        HomeButton.BackgroundColor = selectedMenu == "Home"
            ? Colors.White
            : Colors.Transparent;

        HomeButton.TextColor = selectedMenu == "Home"
            ? Color.FromArgb("#512BD4")
            : Colors.White;
    }
}