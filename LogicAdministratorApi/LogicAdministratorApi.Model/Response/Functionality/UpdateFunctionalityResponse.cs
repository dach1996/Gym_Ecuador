namespace LogicAdministratorApi.Model.Response.Functionality;

/// <summary>
/// Respuesta de actualizar una funcionalidad
/// </summary>
public class UpdateFunctionalityResponse : IApiBaseResponse
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
    /// Guid de la funcionalidad actualizada
    /// </summary>
    public Guid FunctionalityGuid { get; set; }

    /// <summary>
    /// Nombre de la funcionalidad actualizada
    /// </summary>
    public string Name { get; set; }
}
