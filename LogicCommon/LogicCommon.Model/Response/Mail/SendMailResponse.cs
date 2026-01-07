namespace LogicCommon.Model.Response.Mail;
/// <summary>
/// Respuesta de Envío de Mail
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="success"></param>
/// <param name="message"></param>
public class SendMailResponse(
    bool success,
    string message = null)
{
    /// <summary>
    /// Estado de Envío
    /// </summary>
    /// <value></value>
    public bool Success { get; private set; } = success;

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public string Message { get; private set; } = message;

    /// <summary>
    /// Respuesta exitosa
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static SendMailResponse SuccessResponse(string message = "Correo enviado exitosamente")
        => new(true, message);

    /// <summary>
    /// Respuesta con error
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static SendMailResponse FailResponse(string message = "Error al enviar el correo")
        => new(false, message);
}

