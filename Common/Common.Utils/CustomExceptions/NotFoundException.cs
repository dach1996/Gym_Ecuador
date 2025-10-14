using System.Net;
using System.Runtime.Serialization;

namespace Common.Utils.CustomExceptions;

/// <summary>
/// Excepción para recurso no encontrado
/// </summary>

public class NotFoundException : BaseException
{
    /// <summary>
    /// Excepción para manejo de recurso no encontrado
    /// </summary>
    /// <param name="additionalInfo">Razón del error</param>
    public NotFoundException(string additionalInfo) : base(additionalInfo) => CodeHttp = (int)HttpStatusCode.NotFound;
}
