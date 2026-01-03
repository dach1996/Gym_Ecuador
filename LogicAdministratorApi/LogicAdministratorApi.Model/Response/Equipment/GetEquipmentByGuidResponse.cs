using LogicCommon.Model.Response.File;

namespace LogicAdministratorApi.Model.Response.Equipment;

/// <summary>
/// Respuesta de obtener equipamiento por GUID
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="equipment"></param>
public class GetEquipmentByGuidResponse(EquipmentDetail equipment) : IApiBaseResponse
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
    /// Detalle del equipamiento
    /// </summary>
    public EquipmentDetail Equipment { get; set; } = equipment;
}

/// <summary>
/// Detalle del equipamiento
/// </summary>
public class EquipmentDetail
{
    /// <summary>
    /// Guid del equipamiento
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Nombre del equipamiento
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del equipamiento
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Id del tipo de equipamiento
    /// </summary>
    public int EquipmentTypeCatalogId { get; set; }

    /// <summary>
    /// Código del tipo de equipamiento
    /// </summary>
    public string EquipmentTypeCode { get; set; }

    /// <summary>
    /// Nombre del tipo de equipamiento
    /// </summary>
    public string EquipmentTypeName { get; set; }

    /// <summary>
    /// Estado del registro
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Imágenes del equipamiento
    /// </summary>
    public List<FileUrlResponse> Images { get; set; }
}
