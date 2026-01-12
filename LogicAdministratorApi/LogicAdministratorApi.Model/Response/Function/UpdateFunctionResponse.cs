namespace LogicAdministratorApi.Model.Response.Function;

/// <summary>
/// Respuesta de actualizar una función
/// </summary>
public class UpdateFunctionResponse : IApiBaseResponse
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
    /// ID de la función actualizada
    /// </summary>
    public int FunctionId { get; set; }

    /// <summary>
    /// Nombre de la función actualizada
    /// </summary>
    public string Name { get; set; }
}
