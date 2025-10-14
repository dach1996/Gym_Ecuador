using System.Net;
using System.Runtime.Serialization;

namespace Common.Utils.CustomExceptions;

/// <summary>
/// Excepción personalizada
/// </summary>

public class CustomException : BaseException
{
    /// <summary>
    /// Código de respuesta del error
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Excepción personalizada, que permite arrojar algún error en especifico
    /// </summary>
    /// <param name="code">Código de respuesta del error</param>
    /// <param name="additionalInfo">Razón del error</param>
    /// <param name="codeHttp">Código HTTP que desea responder</param>
    public CustomException(int code, string additionalInfo, HttpStatusCode codeHttp = HttpStatusCode.BadRequest) : base(additionalInfo, codeHttp) => Code = code;

    /// <summary>
    /// Excepción personalizada, que permite arrojar algún error en especifico
    /// </summary>
    /// <param name="code">Código de respuesta del error</param>
    /// <param name="codeHttp">Código HTTP que desea responder</param>
    public CustomException(int code, HttpStatusCode codeHttp = HttpStatusCode.BadRequest) : base(string.Empty, codeHttp) => Code = code;
}
