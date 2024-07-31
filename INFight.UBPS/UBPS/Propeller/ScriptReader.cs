using System.Collections.Concurrent;
using System.Reflection;

namespace DragonSpiritsINFight.UBPS.Propeller;

/// <summary>
///     脚本读取器
/// </summary>
internal class ScriptReader
{
    private readonly Assembly _assembly = Assembly.GetExecutingAssembly();
    private readonly ConcurrentDictionary<string, string> _scriptCache = [];

    /// <summary>
    ///     读取脚本文件
    /// </summary>
    /// <param name="script">脚本名称</param>
    /// <returns>脚本内容</returns>
    public string ReadToString(string script)
    {
        if (_scriptCache.TryGetValue(script, out var content))
            return content;

        var filename = $"{script}.lua";
        var file = _assembly.GetManifestResourceStream(filename);
        if (file is null)
            throw new FileNotFoundException("脚本文件丢失！", filename);

        using var reader = new StreamReader(file);
        content = reader.ReadToEnd();
        _scriptCache[script] = content;

        return content;
    }
}
