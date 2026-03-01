using LogicLibrary1.LoadingHandler1;

namespace Learnitallandroid1.Views.Authentication;

public partial class Welcome : ContentPage
{
    private readonly ILoadingScreenManager _loadingScreenManager = new LoadingScreenManager1();

    public bool IsLoadingVisible
        => _loadingScreenManager.IsActive<Welcome>();

    public Welcome()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        RootGrid.Opacity = 0;
        TitleLabel.TranslationY = -20;
        LogoSection.Scale = 0.8;
        ButtonsSection.TranslationY = 40;

        await RootGrid.FadeToAsync(1, 400, Easing.CubicOut);
        _ = TitleLabel.TranslateToAsync(0, 0, 400, Easing.CubicOut);
        await LogoSection.ScaleToAsync(1, 500, Easing.SpringOut);
        await ButtonsSection.TranslateToAsync(0, 0, 400, Easing.CubicOut);
    }

    private async void OnButtonPressed(object sender, EventArgs e)
    {
        if (sender is Button btn)
            await btn.ScaleToAsync(0.96, 80, Easing.CubicInOut);
    }

    private async void OnButtonReleased(object sender, EventArgs e)
    {
        if (sender is Button btn)
            await btn.ScaleToAsync(1, 80, Easing.CubicInOut);
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        _loadingScreenManager.ShowLoadingScreen<Welcome>();
        OnPropertyChanged(nameof(IsLoadingVisible));

        await Task.Yield();

        await Task.Delay(2000);

        _loadingScreenManager.CloseLoadingScreen();
        OnPropertyChanged(nameof(IsLoadingVisible));
    }
}