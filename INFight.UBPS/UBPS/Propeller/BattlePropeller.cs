using DragonSpiritsINFight.UBPS.Data;
using DragonSpiritsINFight.UBPS.Feedback;
using NLog;
using NLua;

namespace DragonSpiritsINFight.UBPS.Propeller;

/// <summary>
///     战斗推进器
/// </summary>
public class BattlePropeller(Battlefield battlefield, Clock clock, IFeedback fb, BattlePropellerConfig config)
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private readonly ScriptReader _scriptReader = new();

    /// <summary>
    ///     推进回合（阻塞方法）
    /// </summary>
    /// <returns>是否已结束</returns>
    public bool Propel()
    {
        if (battlefield.Finished)
        {
            Log.Warn("战斗已结束，请不要继续调用该方法");
            return true;
        }

        var lua = new Lua();
        lua["FB"] = fb;
        lua["Field"] = battlefield;
        lua["Clock"] = clock;
        lua["Log"] = Log;
        lua["__loadFile"] = (string script, string type) =>
            _scriptReader.ReadToString($"{config.ScriptAssembly}.{type}.{script}");

        lua.DoString("""
                     SkillCache = {}
                     function LoadSkill(scriptName)
                        if SkillCache[scriptName] then
                            return SkillCache[scriptName]
                        else
                            return load(__loadFile(scriptName, 'Skill'))
                        end
                     end

                     BuffCache = {}
                     function LoadBuff(scriptName)
                        if BuffCache[scriptName] then
                            return BuffCache[scriptName]
                        else
                            return load(__loadFile(scriptName, 'Buff'))       
                        end
                     end
                     """);

        Log.Info("开始推进战斗");
        lua.DoString(_scriptReader.ReadToString(config.MainScriptName));

        Log.Info("处理完成，等待下次调用");
        return battlefield.Finished;
    }
}
