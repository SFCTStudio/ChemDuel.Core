using MessagePack;

namespace ChemDuel.Core.Reactions;

/// <summary>
/// 反应。
/// </summary>
[MessagePackObject]
public struct Reaction()
{
    [Key(0)] public string[] Reactants;
    [Key(1)] public string[] Products;
    [Key(2)] public string Condition;
    [Key(3)] public bool Reversible;
    [Key(4)] public Dictionary<string, string> Attributes;

    public Reaction(string[] reactants, string[] products, string condition, bool reversible,
        Dictionary<string, string> attributes) : this()
    {
        Reactants = reactants;
        Products = products;
        Condition = condition;
        Reversible = reversible;
        Attributes = attributes;
    }

    private int _sort(string a, string b)
    {
        if (a.Length != b.Length)
        {
            return a.Length - b.Length;
        }

        return string.Compare(a, b, StringComparison.Ordinal);
    }

    public void Sort()
    {
        Array.Sort(Reactants, _sort);
        Array.Sort(Products, _sort);
    }

    public override string ToString()
    {
        return $"{string.Join(" + ", Reactants)} -> {string.Join(" + ", Products)}";
    }

};