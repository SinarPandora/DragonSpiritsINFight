using JetBrains.Annotations;

namespace DragonSpiritsINFight.UBPS.Data;

/// <summary>
///     队伍
/// </summary>
[UsedImplicitly]
public class Team(ushort id, UBPSActor main, List<UBPSActor> subs)
{
    [UsedImplicitly] public ushort Id { get; set; } = id;

    // 前场角色
    [UsedImplicitly] public UBPSActor Main { get; set; } = main;

    // 替补
    [UsedImplicitly] public List<UBPSActor> Subs { get; } = subs;

    // 队伍效果
    [UsedImplicitly] public Dictionary<string, Buff> Buffs { get; } = [];

    // 召唤师技能
    [UsedImplicitly] public Dictionary<string, Skill> MasterSkills { get; } = [];
}
