namespace LogicApi.Model.Response;
/// <summary>
/// Respuesta generica de Handler
/// </summary>
public class HandlerResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessage"></param>
    /// <param name="showMessage"></param>
    private HandlerResponse(string userMessage, bool showMessage)
    {
        UserMessage = userMessage;
        ShowMessage = showMessage;
    }

    /// <summary>
    /// Crea una instancia
    /// </summary>
    /// <param name="message"></param>
    /// <param name="showMessage"></param>
    /// <returns></returns>
    public static HandlerResponse Complete(string message = "Operación Existosa", bool showMessage = false)
        => new(message, showMessage);

}