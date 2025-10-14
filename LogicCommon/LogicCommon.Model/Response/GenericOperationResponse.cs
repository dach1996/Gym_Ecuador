namespace LogicCommon.Model.Response;
/// <summary>
/// Respuesta Genérica
/// </summary>
public class GenericCommonOperationResponse
{
    protected const string SUCCESS = "Operación Exitosa";

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessage"></param>
    public GenericCommonOperationResponse(string userMessage)
    {
        UserMessage = userMessage;
    }

    /// <summary>
    /// Operación correcta
    /// </summary>
    /// <returns></returns>
    public static GenericCommonOperationResponse SuccessOperation()
     => new(SUCCESS);
}