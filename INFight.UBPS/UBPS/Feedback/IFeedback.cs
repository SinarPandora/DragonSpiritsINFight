using DragonSpiritsINFight.UBPS.Data;
using JetBrains.Annotations;

namespace DragonSpiritsINFight.UBPS.Feedback;

/// <summary>
///     UI 反馈
/// </summary>
public interface IFeedback
{
    /// <summary>
    ///     渲染战场
    /// </summary>
    [UsedImplicitly] public void Render();

    /// <summary>
    ///     等待 UI 处理完毕
    /// </summary>
    [UsedImplicitly] public void WaitingUiUpdate();

    /// <summary>
    ///     播放技能特效
    /// </summary>
    /// <param name="teamId">队伍编号（1 或 2）</param>
    /// <param name="type">类型编号（单位，队伍，场地）</param>
    /// <param name="name">技能名称</param>
    [UsedImplicitly] public void PlayBuffEffect(ushort? teamId, string type, string name);

    /// <summary>
    ///     播放技能特效
    /// </summary>
    /// <param name="teamId">队伍编号（1 或 2）</param>
    /// <param name="name">技能名称</param>
    [UsedImplicitly] public void PlaySkillEffect(ushort teamId, string name);

    /// <summary>
    ///     播放召唤师技能特效
    /// </summary>
    /// <param name="teamId">队伍编号（1 或 2）</param>
    /// <param name="name">技能名称</param>
    [UsedImplicitly] public void PlayMasterSkillEffect(ushort teamId, string name);

    /// <summary>
    ///     切换音乐
    /// </summary>
    /// <param name="name">名称</param>
    [UsedImplicitly] public void SwitchBGM(string name);

    /// <summary>
    ///     播放语音
    /// </summary>
    /// <param name="text">内容</param>
    [UsedImplicitly] public void PlayVoice(string text);

    /// <summary>
    ///     显示文本提示
    /// </summary>
    /// <param name="text">内容</param>
    [UsedImplicitly] public void Toast(string text);

    /// <summary>
    ///     检查玩家是否释放了召唤师技能
    /// </summary>
    /// <returns>（如果释放了）技能名称</returns>
    [UsedImplicitly] public string? CheckMasterSkillApply();

    /// <summary>
    ///     通知召唤师技能处理完毕
    /// </summary>
    [UsedImplicitly] public void NotifyMasterSkillApplied();

    /// <summary>
    ///     出错并结束战斗
    /// </summary>
    /// <param name="err">错误内容</param>
    [UsedImplicitly] public void Panic(string err);
}
