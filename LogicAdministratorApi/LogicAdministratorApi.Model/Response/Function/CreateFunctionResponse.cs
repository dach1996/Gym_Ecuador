namespace LogicAdministratorApi.Model.Response.Function;

/// <summary>
/// Respuesta de crear una nueva función
/// </summary>
public class CreateFunctionResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// ID de la función creada
    /// </summary>
    public int FunctionId { get; set; }

    /// <summary>
    /// Nombre de la función creada
    /// </summary>
    public string Name { get; set; }
}
