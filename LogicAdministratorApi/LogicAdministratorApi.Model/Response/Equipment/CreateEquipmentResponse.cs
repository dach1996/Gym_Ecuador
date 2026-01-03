namespace LogicAdministratorApi.Model.Response.Equipment;

/// <summary>
/// Respuesta de crear equipamiento
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="equipmentGuid"></param>
/// <param name="name"></param>
/// <param name="gymBranchGuid"></param>
public class CreateEquipmentResponse(Guid equipmentGuid, string name, Guid gymBranchGuid) : IApiBaseResponse
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
    /// Guid del equipamiento creado
    /// </summary>
    public Guid EquipmentGuid { get; set; } = equipmentGuid;

    /// <summary>
    /// Nombre del equipamiento
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    public Guid GymBranchGuid { get; set; } = gymBranchGuid;
}
