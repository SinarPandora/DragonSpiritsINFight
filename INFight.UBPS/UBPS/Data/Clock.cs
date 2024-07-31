using JetBrains.Annotations;

namespace DragonSpiritsINFight.UBPS.Data;

/// <summary>
///     游戏内时钟
/// </summary>
public class Clock(Battlefield field, Action onTick)
{
    [UsedImplicitly] public ulong Times { get; set; }

    /// <summary>
    ///     敲响一次时钟
    /// </summary>
    [UsedImplicitly]
    public void Tick()
    {
        // 战场 Buff
        field.FieldBuffs.Values
            .Where(buff => buff.RemainTime > 0)
            .ForEach(buff => buff.RemainTime -= 1);

        TeamScan(field.TeamA);
        TeamScan(field.TeamB);

        Times++;
        onTick();
    }

    /// <summary>
    ///     时钟周期内的队伍扫描
    /// </summary>
    /// <param name="team">指定队伍</param>
    private static void TeamScan(Team team)
    {
        // 队伍的召唤师技能
        team.MasterSkills.Values
            .Where(skill => skill.CoolDown > 0)
            .ForEach(skill => skill.CoolDown -= 1);

        // 前场角色 Buff
        team.Main.Buffs.Values
            .Where(buff => buff.RemainTime > 0)
            .ForEach(buff => buff.RemainTime -= 1);

        // 队伍的角色技能
        team.Main.SkillSeq
            .Where(skill => skill.CoolDown > 0)
            .ForEach(skill => skill.CoolDown -= 1);

        // 队伍 Buff
        team.Buffs.Values
            .Where(buff => buff.RemainTime > 0)
            .ForEach(buff => buff.RemainTime -= 1);
    }
}
