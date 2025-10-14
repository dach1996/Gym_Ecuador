using System.Net;
using System.Runtime.Serialization;

/// <summary>
/// Excepción para manejo de peticiones no autorizadas
/// </summary>
namespace Common.Utils.CustomExceptions;

/// <summary>
/// Excepción para manejo de peticiones no autorizadas
/// </summary>
public class AuthException : BaseException
{
    /// <summary>
    /// Excepción para manejo de peticiones no autorizadas
    /// </summary>
    /// <param name="additionalInfo">Razón del error</param>
    public AuthException(string additionalInfo) : base(additionalInfo) => CodeHttp = (int)HttpStatusCode.Unauthorized;
}
