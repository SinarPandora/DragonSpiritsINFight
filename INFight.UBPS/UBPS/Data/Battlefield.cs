using JetBrains.Annotations;

namespace DragonSpiritsINFight.UBPS.Data;

/// <summary>
///     战场
///     目前只支持两队对战
/// </summary>
public class Battlefield(Team teamA, Team teamB)
{
    // 队伍 A（默认我方，不做特殊处理）
    [UsedImplicitly] public Team TeamA { get; } = teamA;

    // 队伍 B（默认对方，不做特殊处理）
    [UsedImplicitly] public Team TeamB { get; } = teamB;

    // 场地效果
    [UsedImplicitly] public Dictionary<string, Buff> FieldBuffs { get; } = [];

    // 是否已结束
    [UsedImplicitly]
    public bool Finished =>
        (!TeamA.Main.Alive && TeamA.Subs.All(x => !x.Alive))
        || (!TeamB.Main.Alive && TeamB.Subs.Any(x => !x.Alive));
}
