using System.Security.Cryptography;
using System.Text;

namespace ChemDuel.Core.Utils;

public static class Md5Utils
{
    public static string Hash(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);
        var sb = new StringBuilder();
        foreach (var t in hashBytes) sb.Append(t.ToString("X2"));
        return sb.ToString();
    }
}