namespace LogicApi.Model.Response.Person;

/// <summary>
/// Respuesta de verificación de documento con IA
/// </summary>
public class VerifyDocumentWithAIResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje
    /// </summary>
    public bool ShowMessage { get; set; }
    
    /// <summary>
    /// Respuesta exitosa
    /// </summary>
    /// <returns></returns>
    public static VerifyDocumentWithAIResponse Success() => new()
    {
        UserMessage = "Documento verificado correctamente",
        ShowMessage = true
    };
}
