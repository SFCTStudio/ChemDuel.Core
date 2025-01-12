namespace ChemDuel.Core.Reactions;

/// <summary>
/// 物质。
///
/// 物质的化学式使用 LaTeX 格式。
/// </summary>
public readonly struct Substance
{
    public readonly string Formula;
    public readonly int Charge = 0;

    public Substance(string formula ,int charge=0)
    {
        Formula = formula;
        Charge = charge;
    }
    
    public static Substance Parse(string formula)
    {
        var charge = 0;
        if (formula.Contains("^{"))
        {
            var chargeRaw = formula.Split("^{")[1].Split("}")[0];
            charge = chargeRaw switch
            {
                "-" => -1,
                "+" => 1,
                _ => int.Parse(chargeRaw)
            };
        }
        return new Substance(formula, charge);
    }

    private char get_pair(char c) => c switch
    {
        '(' => ')',
        '[' => ']',
        _ => c
    };
    
    
        
     /// <summary>
    /// 计算物质的原子数。
    /// </summary>
    /// <returns></returns>
    public Dictionary<Element, int> CalculateAtom()
    {
        var atoms = new Dictionary<Element, int>();
        Element cacheElem = Element.None;
        var cacheDict = new Dictionary<Element, int>();

        for (int i = 0; i < Formula.Length; i++)
        {
            var s = Formula[i];
            if (char.IsUpper(s))
            {
                AddElementToAtoms(ref atoms, ref cacheElem, ref cacheDict);
                cacheElem = GetElement(ref i, s);
            }
            else if (char.IsDigit(s))
            {
                // FIXME: 不支持多位数
                // 但对于高中常见物质，一般不存在多位数
                int num = int.Parse(s.ToString());
                AddElementToAtoms(ref atoms, ref cacheElem, ref cacheDict, num);
            }
            else if (get_pair(s) != s)
            {
                AddElementToAtoms(ref atoms, ref cacheElem, ref cacheDict);
                cacheDict = GetSubstanceAtoms(ref i, s);
            }
        }

        AddElementToAtoms(ref atoms, ref cacheElem, ref cacheDict);
        return atoms;
    }

    private static void AddElementToAtoms(ref Dictionary<Element, int> atoms, ref Element cacheElem, ref Dictionary<Element, int> cacheDict, int num = 1)
    {
        if (cacheElem != Element.None)
        {
            if (!atoms.TryAdd(cacheElem, num))
                atoms[cacheElem] += num;
        }
        foreach (var (key, value) in cacheDict)
        {
            if (atoms.ContainsKey(key))
                atoms[key] += value * num;
            else
                atoms[key] = value * num;
        }
        cacheElem = Element.None;
        cacheDict.Clear();
    }

    private Element GetElement(ref int i, char s)
    {
        if (i != Formula.Length - 1 && char.IsLower(Formula[i + 1]))
        {
            i++;
            return Enum.Parse<Element>(string.Concat(s, Formula[i]));
        }
        return Enum.Parse<Element>(s.ToString());
    }

    private Dictionary<Element, int> GetSubstanceAtoms(ref int i, char s)
    {
        var pair = get_pair(s);
        for (int j = i + 1; j < Formula.Length; j++)
        {
            if (Formula[j] == pair)
            {
                var sub = new Substance(Formula.Substring(i + 1, j - i - 1));
                i = j;
                return sub.CalculateAtom();
            }
        }
        return new Dictionary<Element, int>();
    }
};