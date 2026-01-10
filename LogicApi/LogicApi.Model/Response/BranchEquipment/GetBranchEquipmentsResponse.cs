using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Response.BranchEquipment;

/// <summary>
/// Respuesta de obtener equipos de sucursal
/// </summary>
public class GetBranchEquipmentsResponse : IPaginatorApiResponse<BranchEquipmentItem>
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
    public int TotalRegister { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="totalRegister"></param>
    /// <param name="registers"></param>
    public GetBranchEquipmentsResponse(int totalRegister, IEnumerable<BranchEquipmentItem> registers)
    {
        TotalRegister = totalRegister;
        Registers = registers;
    }

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<BranchEquipmentItem> Registers { get; set; }
}

/// <summary>
/// Item de equipo de sucursal
/// </summary>
public class BranchEquipmentItem
{
    /// <summary>
    /// Guid del equipo
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del equipo
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del equipo
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Código del tipo de equipo
    /// </summary>
    public string EquipmentTypeCode { get; set; }

    /// <summary>
    /// Nombre del tipo de equipo
    /// </summary>
    public string EquipmentTypeName { get; set; }

    /// <summary>
    /// URL de la imagen del equipo
    /// </summary>
    public List<FileUrlResponse> ImageUrls { get; set; }
}

