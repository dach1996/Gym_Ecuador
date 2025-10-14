using System.Net;
using System.Runtime.Serialization;

namespace Common.Utils.CustomExceptions;

/// <summary>
/// Excepción personalizada con texto
/// </summary>

public class CustomTextException : BaseException
{
    /// <summary>
    /// Código de respuesta del error
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Mensaje al cliente
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Excepción personalizada, que permite arrojar algún error en especifico
    /// </summary>
    /// <param name="code">Código de respuesta del error</param>
    /// <param name="text">Texto de respuesta</param>
    /// <param name="additionalInfo">Razón del error</param>
    /// <param name="codeHttp">Código HTTP que desea responder</param>
    public CustomTextException(int code, string text, string additionalInfo, HttpStatusCode codeHttp) : base(additionalInfo, codeHttp)
    {
        Code = code;
        Text = text;
    }
}
