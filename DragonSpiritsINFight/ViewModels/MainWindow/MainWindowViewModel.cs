using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DragonSpiritsINFight.ViewModels.MainWindow.Pages;

namespace DragonSpiritsINFight.ViewModels.MainWindow;

/// <summary>
///     主窗口逻辑
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isSideMenuOpen = true;

    [ObservableProperty] private ViewModelBase _currentPage = new HomePageViewModel();

    public ObservableCollection<NavItemTemplate> NavItems { get; } =
    [
        new NavItemTemplate(typeof(HomePageViewModel), "概览"),
        new NavItemTemplate(typeof(PartyPageViewModel), "编队")
    ];

    [ObservableProperty] private NavItemTemplate? _selectNavItem;

    partial void OnSelectNavItemChanged(NavItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ViewModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    [RelayCommand]
    public void ToggleSideMenu()
    {
        IsSideMenuOpen = !IsSideMenuOpen;
    }
}