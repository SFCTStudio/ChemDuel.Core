using MessagePack;

namespace ChemDuel.Core.Reactions;

/// <summary>
/// 反应数据库的文件格式。
/// </summary>
[MessagePackObject(true)]
public struct ReactionDatabaseFile(string name, string version, string description, Reaction[] reactions)
{
    public string Name = name;
    public string Version = version;
    public string Description = description;
    public Reaction[] Reactions = reactions;
};

/// <summary>
/// 反应数据库。
/// </summary>
public class ReactionDatabase
{
    public string Description ;
    public string Name ;
    public Reaction[] Reactions ;
    public string Version ;

    public ReactionDatabase(string name, string version, string description, Reaction[] reactions)
    {
        Name = name;
        Version = version;
        Description = description;
        Reactions = reactions;
        
        Array.ForEach(Reactions, r => r.Sort());
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
