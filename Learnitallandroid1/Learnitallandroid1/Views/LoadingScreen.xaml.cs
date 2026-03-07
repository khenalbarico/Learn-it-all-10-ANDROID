namespace Learnitallandroid1.Views;

public partial class LoadingScreen : ContentView
{
    private CancellationTokenSource? _rotationCts;

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
        set => SetValue(IsLoadingProperty, value);
    }

    public LoadingScreen()
    {
        InitializeComponent();
    }

    private static async void OnIsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (LoadingScreen)bindable;

        if ((bool)newValue)
            await control.ShowAsync();
        else
            await control.HideAsync();
    }

    private async Task ShowAsync()
    {
        Root.IsVisible = true;

        _rotationCts?.Cancel();
        _rotationCts?.Dispose();
        _rotationCts = new CancellationTokenSource();

        LoaderCircle.Opacity = 0;
        LoaderCircle.Scale = 0.85;
        SpinnerRing.Rotation = 0;

        await Task.WhenAll(
            LoaderCircle.FadeToAsync(1, 220, Easing.CubicOut),
            LoaderCircle.ScaleToAsync(1, 320, Easing.SpringOut)
        );

        _ = RotateAsync(_rotationCts.Token);
    }

    private async Task HideAsync()
    {
        _rotationCts?.Cancel();

        await Task.WhenAll(
            LoaderCircle.FadeToAsync(0, 180, Easing.CubicIn),
            LoaderCircle.ScaleToAsync(0.9, 180, Easing.CubicIn)
        );

        SpinnerRing.Rotation = 0;
        Root.IsVisible = false;
    }

    private async Task RotateAsync(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                await SpinnerRing.RotateToAsync(360, 900, Easing.Linear);
                SpinnerRing.Rotation = 0;
            }
        }
        catch
        {
        }
    }
}