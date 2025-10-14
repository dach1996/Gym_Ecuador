using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Common.Utils.CustomExceptions;
using Newtonsoft.Json;
namespace Common.Utils.Extensions;

public static class StringExtension
{
    /// <summary>
    /// Convierte en una lista de strings en Enums
    /// </summary>
    public static List<T> ToListEnum<T>(this string cadena, string separator = ",") where T : struct
    {
        if (cadena is null)
            return [];
        var listString = cadena.Split(separator);
        try
        {
            var listeType = new List<T>();
            foreach (var type in listString)
                listeType.Add((T)Enum.Parse(typeof(T), type));
            return listeType;
        }
        catch
        {
            return [];
        }
    }

    /// <summary>
    /// Convierte a Enum un string
    /// </summary>
    public static T? ToEnum<T>(this string cadena) where T : struct
    {
        if (cadena == null)
            return null;
        var isEnum = Enum.TryParse(typeof(T), cadena, out var result);
        return isEnum ? (T?)result : null;
    }

    /// <summary>
    /// Transforma en enumerable desde un EnumMember
    /// </summary>
    /// <param name="enumMemberString"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToEnumFromMember<T>(this string enumMemberString)
    {
        var enumType = typeof(T);
        foreach (var name in Enum.GetNames(enumType))
        {
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            if (enumMemberAttribute.Value == enumMemberString) return (T)Enum.Parse(enumType, name);
        }
        throw new FormatException($"No se pudo Parcear a un tipo '{typeof(T)}' el valor de '{enumMemberString}'");
    }

    /// <summary>
    /// Convierte la Json a Objeto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T ToObject<T>(this string source)
    => JsonConvert.DeserializeObject<T>(source);

    /// <summary>
    /// Intenta convertir un Json a Objeto
    /// </summary>
    /// <param name="source"></param>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool TryToObject<T>(this string source, out T result)
    {
        try
        {
            result = JsonConvert.DeserializeObject<T>(source);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// Calcula Hash MACSHA256 con una Clave Secreta
    /// </summary>
    /// <param name="secretKey"></param>
    /// <param name="dataInput"></param>
    /// <param name="base64Output"></param>
    /// <returns></returns>
    public static string ToSha256(this string dataInput, string secretKey, bool base64Output = false)
    {
        var encoder = new UTF8Encoding();
        var byteSecret = encoder.GetBytes(secretKey);
        var byteDataInput = encoder.GetBytes(dataInput);

        var hmac = new HMACSHA256(byteSecret);
        var hmacResult = hmac.ComputeHash(byteDataInput);

        //Devuelve en Base64
        if (base64Output)
            return hmacResult.ToBase64();

        //Devuelve en HEX
        var sb = new StringBuilder();
        foreach (var t in hmacResult)
            sb.Append(t.ToString("X2"));

        return sb.ToString().ToLower();
    }

    /// <summary>
    /// Calcula Hash sha256
    /// </summary>
    /// <param name="secretKey"></param>
    /// <param name="dataInput"></param>
    /// <param name="base64Output"></param>
    /// <returns></returns>
    public static string ToSha256(this string dataInput, bool base64Output = false)
    {
        var encoder = new UTF8Encoding();
        var byteDataInput = encoder.GetBytes(dataInput);
        var hmacResult = SHA256.HashData(byteDataInput);
        //Devuelve en Base64
        if (base64Output)
            return hmacResult.ToBase64();
        //Devuelve en HEX
        var sb = new StringBuilder();
        foreach (var t in hmacResult)
            sb.Append(t.ToString("X2"));
        return sb.ToString().ToLower();
    }

    /// <summary>
    /// Calcula Hash sha512
    /// </summary>
    /// <param name="dataInput"></param>
    /// <param name="base64Output"></param>
    /// <returns></returns>
    public static string ToSha512(this string dataInput, bool base64Output = false)
    {
        var encoder = new UTF8Encoding();
        var byteDataInput = encoder.GetBytes(dataInput);
        var hmacResult = SHA512.HashData(byteDataInput);
        //Devuelve en Base64
        if (base64Output)
            return hmacResult.ToBase64();
        //Devuelve en HEX
        var sb = new StringBuilder();
        foreach (var t in hmacResult)
            sb.Append(t.ToString("X2"));
        return sb.ToString().ToLower();
    }

    /// <summary>
    /// Verifica si es null or empty y envía excepción
    /// </summary>
    /// <param name="input"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string input, Exception exception = null)
    {
        var isNullOrEmpty = string.IsNullOrEmpty(input);
        if (isNullOrEmpty && exception is not null)
            throw exception;
        return isNullOrEmpty;
    }

    /// <summary>
    /// Decodifica un String
    /// </summary>
    /// <param name="encodeStringInput"></param>
    /// <returns></returns>
    public static string Decode(this string encodeStringInput, bool removeNewLine = true)
    {
        var decodeString = Encoding.UTF8.GetString(Convert.FromBase64String(encodeStringInput));
        if (removeNewLine)
            decodeString = decodeString.Replace("\n", "");
        return decodeString;
    }

    /// <summary>
    /// Codifica un String
    /// </summary>
    /// <param name="encodeStringInput"></param>
    /// <returns></returns>
    public static string Encode(this string decodeStringInput)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(decodeStringInput);
        return Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    /// Codifica un String
    /// </summary>
    /// <param name="encodeStringInput"></param>
    /// <returns></returns>
    public static int? ToIntNull(this string inputInt, Exception exception = null)
    {
        var isNumber = int.TryParse(inputInt, out var intValue);
        if (!isNumber && exception is not null)
            throw exception;
        return isNumber ? intValue : null;
    }

    /// <summary>
    /// Convierte un valor a entero
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static int ToInt(this string input)
    {
        var isNumber = int.TryParse(input, out var intValue);
        if (!isNumber)
            throw new ArgumentException($"No se puede convertir el string: '{input}' ", nameof(input));
        return intValue;
    }

    /// <summary>
    /// Verifica si el texto contiene solo números
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool ContainsOnlyNumbers(this string input)
        => input.All(char.IsDigit);

    /// <summary>
    /// Obtiene el texto eliminando tíldes
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string RemoveDiacritics(this string input)
    {
        string normalizedString = input.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new();
        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Verifica si existe una cadena
    /// </summary>
    /// <param name="fullString"></param>
    /// <param name="subQuery"></param>
    /// <returns></returns>
    public static bool ExistString(this string fullString, string subQuery) =>
        // Realizar la búsqueda sin distinguir mayúsculas y minúsculas
        RemoveDiacritics(fullString.ToLower()).Contains(RemoveDiacritics(subQuery.ToLower()), StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Comparar ignorando caracteres
    /// </summary>
    /// <param name="input"></param>
    /// <param name="compare"></param>
    /// <param name="stringComparison"></param>
    /// <returns></returns>
    public static bool CompareIgnore(this string input, string compare, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        => input.Equals(compare, stringComparison);
}
