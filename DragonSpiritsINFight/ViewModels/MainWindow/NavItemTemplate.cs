using System;

namespace DragonSpiritsINFight.ViewModels.MainWindow;

/// <summary>
///     导航项模板
/// </summary>
public class NavItemTemplate(Type viewModelType, string label)
{
    public Type ViewModelType { get; } = viewModelType;
    public string Label { get; } = label;
}