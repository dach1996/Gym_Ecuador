using System.Net;
using System.Runtime.Serialization;
using Common.Utils.Extensions;

namespace Common.Utils.CustomExceptions;

/// <summary>
/// Excepción para  objeto en Null o no configurado
/// </summary>

public class NullException : BaseException
{
    /// <summary>
    /// Excepción para manejo de recurso no encontrado
    /// </summary>
    /// <param name="additionalInfo">Razón del error</param>
    public NullException(string additionalInfo) : base(additionalInfo) => CodeHttp = (int)HttpStatusCode.NotFound;

    /// <summary>
    /// Validación de Nombre null
    /// </summary>
    /// <param name="value"></param>
    /// <param name="argumentName"></param>
    /// <param name="methodName"></param>
    public static void ThrowIfNullOrEmpty(object value, string argumentName, string methodName)
    {
        if ((value is string stringValue && stringValue.IsNullOrEmpty()) || value is null)
            throw new NullException($"El argumento: '{argumentName}' es null o está vacío en el método: '{methodName}'");
    }

    /// <summary>
    /// Validación de Nombre null
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    public static void ThrowIfNullOrEmpty(object value, string message)
    {
        if ((value is string stringValue && stringValue.IsNullOrEmpty()) || value is null)
            throw new NullException(message);
    }



}