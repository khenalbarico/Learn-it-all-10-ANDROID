namespace Learnitallandroid1.Views;

public partial class LoadingScreen : ContentView
{
    private CancellationTokenSource? _animationCts;

    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(LoadingScreen),
            false,
            propertyChanged: OnIsLoadingChanged);

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set =>       SetValue(IsLoadingProperty, value);
    }

    public LoadingScreen()
    => InitializeComponent();
    

    private static async void OnIsLoadingChanged(
        BindableObject bindable,
        object         oldValue,
        object         newValue)
    {
        var control = (LoadingScreen)bindable;

        if ((bool)newValue)
            await control.ShowAsync();
        else
            await control.HideAsync();
    }

    private async Task ShowAsync()
    {
        if (Root.IsVisible)
            return;

        _animationCts?.Cancel();
        _animationCts?.Dispose();
        _animationCts = new CancellationTokenSource();

        Root.IsVisible        = true;
        Root.InputTransparent = false;

        Root.Opacity         = 0;
        LoaderCircle.Opacity = 0;
        LoaderCircle.Scale   = 0.82;
        SpinnerHost.Rotation = 0;
        LogoImage.Scale      = 0.96;

        await Task.WhenAll(
            Root        .FadeToAsync (0.55, 180, Easing.CubicOut),
            LoaderCircle.FadeToAsync (1, 220,    Easing.CubicOut),
            LoaderCircle.ScaleToAsync(1, 320,    Easing.SpringOut)
        );

        _ = RunSpinnerAsync   (_animationCts.Token);
        _ = RunPulseAsync     (_animationCts.Token);
        _ = RunLogoBreathAsync(_animationCts.Token);
    }

    private async Task HideAsync()
    {
        _animationCts?.Cancel();

        SpinnerHost .AbortAnimation("SpinnerRotation");
        LoaderCircle.AbortAnimation("LoaderPulse");
        LogoImage   .AbortAnimation("LogoBreath");

        await Task.WhenAll(
            Root        .FadeToAsync (0, 140,   Easing.CubicIn),
            LoaderCircle.FadeToAsync (0, 140,   Easing.CubicIn),
            LoaderCircle.ScaleToAsync(0.9, 140, Easing.CubicIn)
        );

        SpinnerHost.Rotation  = 0;
        LogoImage.Scale       = 0.96;
        Root.IsVisible        = false;
        Root.InputTransparent = true;
    }

    private async Task RunSpinnerAsync(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                await SpinnerHost.RotateToAsync(
                      SpinnerHost.Rotation + 360,
                      900,
                      Easing.Linear);
            }
        }
        catch
        {
        }
    }

    private async Task RunPulseAsync(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                await LoaderCircle.ScaleToAsync(1.03, 700, Easing.CubicInOut);
                await LoaderCircle.ScaleToAsync(1.00, 700, Easing.CubicInOut);
            }
        }
        catch
        {
        }
    }

    private async Task RunLogoBreathAsync(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                await LogoImage.ScaleToAsync(1.03, 650, Easing.CubicInOut);
                await LogoImage.ScaleToAsync(0.96, 650, Easing.CubicInOut);
            }
        }
        catch
        {
        }
    }
}