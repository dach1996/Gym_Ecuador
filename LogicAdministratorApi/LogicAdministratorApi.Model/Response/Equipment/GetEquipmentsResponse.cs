using LogicCommon.Model.Response.File;

namespace LogicAdministratorApi.Model.Response.Equipment;

/// <summary>
/// Respuesta de obtener equipamientos paginados
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetEquipmentsResponse(int totalRegister, IEnumerable<EquipmentItem> registers) : IPaginatorApiResponse<EquipmentItem>
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
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; } = totalRegister;

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<EquipmentItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de equipamiento
/// </summary>
public class EquipmentItem
{
    /// <summary>
    /// Guid del equipamiento
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del equipamiento
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del equipamiento
    /// </summary>
    public string Description { get; set; }

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
    /// <value></value>
    public FileUrlResponse Image { get; set; }
}
