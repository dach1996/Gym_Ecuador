namespace LogicAdministratorApi.Model.Response.Equipment;

/// <summary>
/// Respuesta de eliminar equipamiento
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
public class DeleteEquipmentResponse() : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

}

