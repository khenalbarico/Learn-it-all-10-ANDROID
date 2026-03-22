using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Learnitallandroid1.Views.Components;
using LogicLibrary1.AppBookInit1;
using LogicLibrary1.AppDb1;

namespace Learnitallandroid1.Views.Menu;

public partial class IndexView : ContentPage, INotifyPropertyChanged
{
    readonly SidebarComp _sidebarComp;
    private readonly IAppDbOperator? _appDbOperator;
    public ObservableCollection<BooksResultAll1> CachedBooks { get; } = [];

    public bool HasBooks   => CachedBooks.Count > 0;
    public bool HasNoBooks => CachedBooks.Count == 0;

    public new event PropertyChangedEventHandler? PropertyChanged;

    public IndexView(SidebarComp sidebarComp)
    {
        InitializeComponent();
        BindingContext = this;

        _sidebarComp = sidebarComp;
        SidebarHost.Content = _sidebarComp;

        _sidebarComp.MenuSelected += OnMenuSelected;
        _sidebarComp.SetActiveMenu("Home");

        _appDbOperator = Application.Current?
            .Handler?
            .MauiContext?
            .Services
            .GetService<IAppDbOperator>();

        Loaded += IndexView_Loaded;
    }

    private async void IndexView_Loaded(object? sender, EventArgs e)
    {
        await LoadBooksAsync();
    }

    private async Task LoadBooksAsync()
    {
        if (_appDbOperator == null)
            return;

        var result = await _appDbOperator.LoadAllBooksAsync<BooksResultAll1>();

        CachedBooks.Clear();

        if (result != null)
        {
            foreach (var book in result)
                CachedBooks.Add(book);
        }

        OnPropertyChanged(nameof(HasBooks));
        OnPropertyChanged(nameof(HasNoBooks));
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

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}