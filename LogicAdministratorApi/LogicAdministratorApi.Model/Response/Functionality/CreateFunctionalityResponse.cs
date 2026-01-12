namespace LogicAdministratorApi.Model.Response.Functionality;

/// <summary>
/// Respuesta de crear una nueva funcionalidad
/// </summary>
public class CreateFunctionalityResponse : IApiBaseResponse
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
    /// Guid de la funcionalidad creada
    /// </summary>
    public Guid FunctionalityGuid { get; set; }

    /// <summary>
    /// Nombre de la funcionalidad creada
    /// </summary>
    public string Name { get; set; }
}
