using System.Text;
using Standart.Hash.xxHash;

namespace Common.Utils.Cryptography.XxHash;
/// <summary>
/// Clase para calcular el hash de un texto
/// </summary>
public static class XxHash
{
    /// <summary>
    /// Convierte un texto a un hash de 32 bits
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ToHash32(string text)
    {
        var data = Encoding.UTF8.GetBytes(text);
        var xxHash32Response = xxHash32.ComputeHash(data, data.Length);
        return xxHash32Response.ToString("x8");
    }
}