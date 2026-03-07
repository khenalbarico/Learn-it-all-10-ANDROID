using Learnitallandroid1.Views.App;

namespace Learnitallandroid1.Views.Authentication;

public partial class Welcome : AuthPage1
{
    public Welcome()
    => InitializeComponent();
    

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        RootGrid.Opacity            = 0;
        TitleLabel.TranslationY     = -20;
        LogoSection.Scale           = 0.8;
        ButtonsSection.TranslationY = 40;

        await RootGrid      .FadeToAsync(1, 400, Easing.CubicOut);
        _ =   TitleLabel    .TranslateToAsync(0, 0, 400, Easing.CubicOut);
        await LogoSection   .ScaleToAsync(1, 500, Easing.SpringOut);
        await ButtonsSection.TranslateToAsync(0, 0, 400, Easing.CubicOut);
    }

    private async void OnButtonPressed(object sender, EventArgs e)
    {
        if (sender is Button button)
            await button.ScaleToAsync(0.96, 80, Easing.CubicInOut);
    }

    private async void OnButtonReleased(object sender, EventArgs e)
    {
        if (sender is Button button)
            await button.ScaleToAsync(1, 80, Easing.CubicInOut);
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    => await GoToAsync<LoginRegister>();
    private async void OnContinueClicked(object sender, EventArgs e)
    => await GoToAsync<Library>();

}