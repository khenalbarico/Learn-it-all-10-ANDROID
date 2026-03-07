using LogicLibrary1.LoadingHandler1;

namespace Learnitallandroid1.Views;

public abstract class BasePage1 : ContentPage
{
    private readonly ILoadingScreenManager _loadingScreenManager = new LoadingScreenManager1();
    private bool _isNavigating;

    public bool IsLoadingVisible => _loadingScreenManager.IsActive(GetType());

    protected BasePage1()
    {
        Shell.SetNavBarIsVisible(this, false);
        BindingContext = this;
    }

    protected void ShowLoading()
    {
        _loadingScreenManager.ShowLoadingScreen(GetType());
        OnPropertyChanged(nameof(IsLoadingVisible));
    }

    protected void HideLoading()
    {
        _loadingScreenManager.CloseLoadingScreen();
        OnPropertyChanged(nameof(IsLoadingVisible));
    }

    protected async Task GoToAsync<TPage>() where TPage : Page
    {
        if (_isNavigating)
            return;

        try
        {
            _isNavigating = true;
            ShowLoading();
            await Shell.Current.GoToAsync(typeof(TPage).Name);
        }
        finally
        {
            HideLoading();
            _isNavigating = false;
        }
    }

    protected async Task GoBackAsync()
    {
        if (_isNavigating)
            return;

        try
        {
            _isNavigating = true;
            ShowLoading();
            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            HideLoading();
            _isNavigating = false;
        }
    }
}