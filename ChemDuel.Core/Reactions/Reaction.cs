using ChemDuel.Core.Utils;
using MessagePack;

namespace ChemDuel.Core.Reactions;

/// <summary>
/// 反应。
/// </summary>
[MessagePackObject]
public struct Reaction()
{
    [Key(0)] public readonly string[] Reactants;
    [Key(1)] public readonly string[] Products;
    [Key(2)] public readonly string Condition;
    [Key(3)] public readonly bool Reversible;
    [Key(4)] public readonly Dictionary<string, string> Attributes;

    public Reaction(string[] reactants, string[] products, string condition, bool reversible,
        Dictionary<string, string> attributes) : this()
    {
        Reactants = reactants;
        Products = products;
        Condition = condition;
        Reversible = reversible;
        Attributes = attributes;
    }

    public void Sort()
    {
        Array.Sort(Reactants, SortUtils.SortByLength);
        Array.Sort(Products, SortUtils.SortByLength);
    }

    public override string ToString()
    {
        var arrow = Reversible ? "<=>" : "=>";
        return $"{string.Join(" + ", Reactants)} {arrow} {string.Join(" + ", Products)}";
    }
};