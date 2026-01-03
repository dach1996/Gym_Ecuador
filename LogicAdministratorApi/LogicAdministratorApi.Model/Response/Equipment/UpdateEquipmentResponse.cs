namespace LogicAdministratorApi.Model.Response.Equipment;

/// <summary>
/// Respuesta de actualizar equipamiento
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="equipmentGuid"></param>
/// <param name="name"></param>
public class UpdateEquipmentResponse(Guid equipmentGuid, string name) : IApiBaseResponse
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
    /// Guid del equipamiento actualizado
    /// </summary>
    public Guid EquipmentGuid { get; set; } = equipmentGuid;

    /// <summary>
    /// Nombre del equipamiento
    /// </summary>
    public string Name { get; set; } = name;
}
