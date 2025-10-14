using System.Net;
using System.Runtime.Serialization;

namespace Common.Utils.CustomExceptions;

/// <summary>
/// Excepción personalizada
/// </summary>

public class WebJobException : BaseException
{
    /// <summary>
    /// Excepción para manejo de recurso no encontrado
    /// </summary>
    /// <param name="additionalInfo">Razón del error</param>
    public WebJobException(string additionalInfo) : base(additionalInfo) => CodeHttp = (int)HttpStatusCode.NotFound;

}
