using System.Security.Cryptography;
using Common.Utils.Extensions;

namespace Common.Utils;
public static class Util
{

    #region Conversion
    public enum UnixConvertion
    {
        Seconds,
        Miliseconds
    }

    /// <summary>
    /// Método que permite convertir mili-segundos a un objeto Fecha
    /// </summary>
    /// <param name="timestamp">Fecha en mili-segundos</param>
    /// <param name="typeConverter">Tipo de Conversión en segundos o mili-segundos</param>
    /// <returns></returns>
    public static DateTime ConvertUnixToDate(long timestamp, UnixConvertion typeConverter = UnixConvertion.Miliseconds)
    {
        var dtDateTime = DateTime.UnixEpoch;
        return typeConverter switch
        {
            UnixConvertion.Seconds => dtDateTime.AddSeconds(timestamp).ToLocalTime(),
            UnixConvertion.Miliseconds => dtDateTime.AddMilliseconds(timestamp).ToLocalTime(),
            _ => throw new ArgumentException($"Comportamiento inesperado: {timestamp} - Tipo: {typeConverter}"),
        };
    }

    #endregion

    #region String Handlers


    /// <summary>
    /// Formatea un texto para que se muestre con la primera letra mayúscula en y el resto en minúscula 
    /// </summary>
    /// <param name="input">Texto</param>
    /// <returns></returns>
    public static string SentenceCase(string input)
    {
        try
        {
            //Valida el input
            if (input.Length <= 1)
                return input.ToUpper();

            //Hace trim al texto
            input = input.Trim();

            //Corta la oración de la primera letra y la hace toda minúsculas
            string sentence = input[1..].ToLower();

            //Agrega la primera letra en mayúscula y el resto de la Oración
            return input[0].ToString().ToUpper() + sentence;
        }
        catch (Exception)
        {
            //En caso de error retorna el mismo texto
            return input;
        }
    }

    #endregion

    #region Bool

    /// <summary>
    /// Obtiene un aleatorio Boleano
    /// </summary>
    /// <returns></returns>
    public static bool RandomBool() => RandomNumberGenerator.GetInt32(0, 2) switch
    {
        0 => true,
        1 => false,
        _ => false,
    };

    #endregion

    #region Enum

    /// <summary>
    /// Obtiene la lista de los enumerables
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<string> GetListEnumMember<T>() where T : Enum =>
        Enum.GetValues(typeof(T)).Cast<T>().Select(t => t.GetEnumMember()).ToList();

    /// <summary>
    /// Obtiene la lista de los enumerables
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetListEnums<T>() where T : Enum =>
        Enum.GetValues(typeof(T)).Cast<T>().ToList();

    /// <summary>
    /// Obtiene un diccionario con los enumerables y los enumsMember
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IDictionary<T, string> GetDictionaryEnums<T>() where T : Enum =>
        Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(t => t, t => t.GetEnumMember());

    #endregion

}
