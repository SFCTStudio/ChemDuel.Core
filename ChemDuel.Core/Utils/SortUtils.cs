namespace ChemDuel.Core.Utils;

public static class SortUtils
{
    public static int SortByLength(string a, string b)
    {
        if (a.Length != b.Length) return a.Length - b.Length;

        return string.Compare(a, b, StringComparison.Ordinal);
    }
}