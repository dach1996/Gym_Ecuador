using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Response.BranchEquipment;

/// <summary>
/// Respuesta de obtener detalle de equipo de sucursal por GUID
/// </summary>
public class GetBranchEquipmentByGuidResponse(BranchEquipmentDetail equipment) : IApiBaseResponse
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
    /// Datos del equipo
    /// </summary>
    public BranchEquipmentDetail Equipment { get; set; } = equipment;
}

/// <summary>
/// Detalle completo de equipo de sucursal
/// </summary>
public class BranchEquipmentDetail : BranchEquipmentItem
{
    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Estado del registro (Activo/Inactivo)
    /// </summary>
    public bool IsActive { get; set; }
}

