using System.Collections.Immutable;
using JetBrains.Annotations;

namespace DragonSpiritsINFight.UBPS.Data;

/// <summary>
///     核心对象，参与对战的单位
/// </summary>
[UsedImplicitly]
public class UBPSActor(
    string name,
    Dictionary<string, object> props,
    ImmutableDictionary<string, object> originalProps,
    List<Skill> skillSeq
)
{
    // 唯一 ID
    public Guid Id { get; } = Guid.NewGuid();

    [UsedImplicitly] public string Name { get; set; } = name;

    // 当前体力
    [UsedImplicitly] public int HP { get; set; } = (int)originalProps[PropKey.MHP.ToString()];

    // 是否存活
    [UsedImplicitly] public bool Alive => HP > 0;

    // 当前属性及能力
    [UsedImplicitly] public Dictionary<string, object> Props { get; set; } = props;

    // 原始属性及能力
    [UsedImplicitly] public ImmutableDictionary<string, object> OriginalProps { get; } = originalProps;

    // Buff
    [UsedImplicitly] public Dictionary<string, Buff> Buffs { get; } = [];

    // 技能
    [UsedImplicitly] public int SkillSeqIndex { get; set; }

    // 技能序列
    [UsedImplicitly] public List<Skill> SkillSeq { get; set; } = skillSeq;
}
