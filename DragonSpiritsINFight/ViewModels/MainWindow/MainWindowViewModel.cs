using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DragonSpiritsINFight.ViewModels.MainWindow.Pages;
using Material.Icons;

namespace DragonSpiritsINFight.ViewModels.MainWindow;

/// <summary>
///     主窗口逻辑
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    #region SideNavLogic

    [ObservableProperty] private bool _isSideNavOpen = true;

    /// <summary>
    ///     切换左侧导航栏显示状态
    /// </summary>
    [RelayCommand]
    public void ToggleSideNav()
    {
        IsSideNavOpen = !IsSideNavOpen;
    }

    #region SwitchPageLogic

    [ObservableProperty] private ViewModelBase _currentPage = new HomePageViewModel();

    /// <summary>
    ///     可选择页面列表
    /// </summary>
    public ObservableCollection<NavItemTemplate> NavItems { get; } =
    [
        new NavItemTemplate(typeof(HomePageViewModel), "概览", MaterialIconKind.ViewDashboardOutline),
        new NavItemTemplate(typeof(PartyPageViewModel), "编队", MaterialIconKind.ListBoxOutline),
        new NavItemTemplate(typeof(BattlePageViewModel), "战斗", MaterialIconKind.Fencing),
        new NavItemTemplate(typeof(ExplorerPageViewModel), "探索", MaterialIconKind.CompassOutline),
        new NavItemTemplate(typeof(TrainingPageViewModel), "训练", MaterialIconKind.TimerOutline),
        new NavItemTemplate(typeof(SettingPageViewModel), "设置", MaterialIconKind.SettingsOutline),
    ];

    /// <summary>
    ///     选择的页面
    /// </summary>
    [ObservableProperty] private NavItemTemplate? _selectNavItem;

    /// <summary>
    ///     当选择左侧项目时，改变右侧页面
    /// </summary>
    /// <param name="value">新页面</param>
    partial void OnSelectNavItemChanged(NavItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ViewModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    #endregion

    #endregion
}