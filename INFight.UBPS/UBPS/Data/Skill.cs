using JetBrains.Annotations;

namespace DragonSpiritsINFight.UBPS.Data;

/// <summary>
///     技能
/// </summary>
[UsedImplicitly]
public class Skill(string name, string type, uint originalSP, int cooldown = 0)
{
    [UsedImplicitly] public string Name { get; } = name;

    [UsedImplicitly] public string Type { get; } = type;

    // 是否被效果封印
    [UsedImplicitly] public bool IsBlocked { get; }

    // 原始 SP
    [UsedImplicitly] public uint OriginalSP { get; } = originalSP;

    // SP
    [UsedImplicitly] public uint SP { get; set; } = originalSP;

    // 冷却
    [UsedImplicitly] public int CoolDown { get; set; } = cooldown;

    // 释放次数
    [UsedImplicitly] public uint Count { get; set; }
}
