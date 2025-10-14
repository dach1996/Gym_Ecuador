using System.Net;
using System.Runtime.Serialization;
namespace Common.Utils.CustomExceptions;

/// <summary>
/// Excepción para indicar que hay un error en la configuración del servicio
/// </summary>

public class ConfigurationException : BaseException
{
    /// <summary>
    /// Excepción para manejo de peticiones con error de configuración de parámetros
    /// </summary>
    /// <param name="additionalInfo">Razón del error</param>
    public ConfigurationException(string additionalInfo) : base(additionalInfo) => CodeHttp = (int)HttpStatusCode.BadRequest;
}
