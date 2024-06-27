using System;
using Material.Icons;

namespace DragonSpiritsINFight.ViewModels.MainWindow;

/// <summary>
///     导航项模板
/// </summary>
public class NavItemTemplate(Type viewModelType, string label, MaterialIconKind icon)
{
    public Type ViewModelType { get; } = viewModelType;
    public string Label { get; } = label;
    public MaterialIconKind Icon { get; } = icon;
}