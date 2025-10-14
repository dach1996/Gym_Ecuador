using System.Net;
using System.Runtime.Serialization;

namespace Common.Utils.CustomExceptions;

/// <summary>
/// Excepción para errores en la validación del modelo 
/// </summary>

public class ModelException : BaseException
{
    /// <summary>
    /// Excepción para manejo de errores en la validación del modelo 
    /// </summary>
    /// <param name="additionalInfo">Razón del error</param>
    public ModelException(string additionalInfo) : base(additionalInfo) => CodeHttp = (int)HttpStatusCode.BadRequest;

    /// <summary>
    /// Lanza una excepción por validación de tamaño
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lessThan"></param>
    /// <param name="argumentName"></param>
    public static void ThrowIfLengthIsLessThan(string value, int lessThan, string argumentName)
    {
        if (value.Length < lessThan)
            throw new ModelException($"La longitud del argumento: '{argumentName}' es menor a :'{lessThan}' - Valor Ingresado:'{value}'");
    }
}
