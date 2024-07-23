using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using DragonSpiritsINFight.Assets;
using DragonSpiritsINFight.ViewModels.MainWindow.Pages;
using DynamicData.Binding;
using Material.Icons;
using ReactiveUI;

namespace DragonSpiritsINFight.ViewModels.MainWindow;

/// <summary>
///     主窗口逻辑
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        ToggleSideNav = ReactiveCommand.Create(() => _isSideNavOpen = !_isSideNavOpen);
        this.WhenValueChanged(x => x.SideNavItem)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(OnSelectNavItemChanged);
    }

    public ICommand ToggleSideNav { get; }
    // 反应变量 ---------------------------------------------------------------------------------------------------------
    private bool _isSideNavOpen;

    public bool IsSideNavOpen
    {
        get => _isSideNavOpen;
        set => this.RaiseAndSetIfChanged(ref _isSideNavOpen, value);
    }

    private SideNavItem? _sideNavItem;

    public SideNavItem? SideNavItem
    {
        get => _sideNavItem;
        set => this.RaiseAndSetIfChanged(ref _sideNavItem, value);
    }

    private ViewModelBase _currentPage = new HomePageViewModel();

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    // -----------------------------------------------------------------------------------------------------------------

    // 路由表
    public ObservableCollection<SideNavItem> NavItems { get; } =
    [
        new SideNavItem(
            typeof(HomePageViewModel),
            Resources.Home_PageName,
            MaterialIconKind.ViewDashboardOutline
        ),
        new SideNavItem(
            typeof(PartyPageViewModel),
            Resources.Party_PageName,
            MaterialIconKind.ListBoxOutline
        ),
        new SideNavItem(
            typeof(BattlePageViewModel),
            Resources.Battle_PageName,
            MaterialIconKind.Fencing
        ),
        new SideNavItem(
            typeof(ExplorerPageViewModel),
            Resources.Explorer_PageName,
            MaterialIconKind.CompassOutline
        ),
        new SideNavItem(
            typeof(SynthesizePageViewModel),
            Resources.Synthesize_PageName,
            MaterialIconKind.VectorCombine
        ),
        new SideNavItem(
            typeof(TrainingPageViewModel),
            Resources.Training_PageName,
            MaterialIconKind.Compost
        ),
        new SideNavItem(
            typeof(SettingPageViewModel),
            Resources.Setting_PageName,
            MaterialIconKind.SettingsOutline
        ),
    ];

    /// <summary>
    ///     当选择左侧项目时，改变右侧页面
    /// </summary>
    private void OnSelectNavItemChanged(SideNavItem? item)
    {
        if (item is null) return;
        var instance = Activator.CreateInstance(item.ViewModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }
}
