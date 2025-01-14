using ChemDuel.Core.Utils;
using MessagePack;

namespace ChemDuel.Core.Reactions;

/// <summary>
/// 反应数据库的文件格式。
/// </summary>
[MessagePackObject(true)]
public struct ReactionDatabaseFile(string name, string version, string description, Reaction[] reactions)
{
    public readonly string Name = name;
    public readonly string Version = version;
    public readonly string Description = description;
    public readonly Reaction[] Reactions = reactions;
};

/// <summary>
/// 反应数据库。
/// </summary>
public class ReactionDatabase
{
    public string Description;
    public string Name;
    public Reaction[] Reactions;
    public string Version;

    public ReactionDatabase(string name, string version, string description, Reaction[] reactions)
    {
        Name = name;
        Version = version;
        Description = description;
        Reactions = reactions;

        Array.ForEach(Reactions, r => r.Sort());
    }

    /// <summary>
    /// 反应数据库的哈希值。
    /// </summary>
    /// 计算规则：
    /// 先对所有反应按长度排序，然后将所有反应的字符串连接起来，最后计算 MD5 哈希值。
    /// <returns>哈希值</returns>
    public string Hash()
    {
        var reactions = Reactions.Select(r => r.ToString()).ToArray();
        Array.Sort(reactions, SortUtils.SortByLength);
        return Md5Utils.Hash(string.Join("", reactions));
    }

    /// <summary>
    /// 从反应数据库文件中加载反应数据库。
    /// </summary>
    ///
    /// <see cref="ReactionDatabaseFile"/>
    /// <param name="path">数据库路径</param>
    /// <returns>反应数据库</returns>
    public static ReactionDatabase LoadFromFile(string path)
    {
        var file = MessagePackSerializer.Deserialize<ReactionDatabaseFile>(File.ReadAllBytes(path));
        return new ReactionDatabase(file.Name, file.Version, file.Description, file.Reactions);
    }

    /// <summary>
    /// 保存反应数据库到文件。
    /// </summary>
    /// <param name="path">保存路径</param>
    public void SaveToFile(string path)
    {
        var file = new ReactionDatabaseFile(Name, Version, Description, Reactions);
        File.WriteAllBytes(path, MessagePackSerializer.Serialize(file));
    }
}