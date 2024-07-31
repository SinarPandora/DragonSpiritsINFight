using JetBrains.Annotations;

namespace DragonSpiritsINFight.UBPS.Data;

/// <summary>
///     Buff
/// </summary>
[UsedImplicitly]
public class Buff(string name, bool isDebuff, bool isAbilityEffect, bool isBeforeAction, int count, int remainTime)
{
    [UsedImplicitly] public string Name { get; } = name;

    [UsedImplicitly] public bool IsDebuff { get; } = isDebuff;

    // 属性增减
    [UsedImplicitly] public bool IsAbilityEffect { get; } = isAbilityEffect;

    // 在回合开始激活
    [UsedImplicitly] public bool IsBeforeAction { get; } = isBeforeAction;

    // 层数
    [UsedImplicitly] public int Count { get; set; } = count;

    // 剩余时间
    [UsedImplicitly] public int RemainTime { get; set; } = remainTime;
}
