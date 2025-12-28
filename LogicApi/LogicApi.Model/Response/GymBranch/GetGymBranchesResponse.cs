using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Response.GymBranch;

/// <summary>
/// Respuesta de obtener sucursales de gimnasio
/// </summary>
public class GetGymBranchesResponse : IPaginatorApiResponse<GymBranchItem>
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
    public GetGymBranchesResponse(int totalRegister, IEnumerable<GymBranchItem> registers)
    {
        TotalRegister = totalRegister;
        Registers = registers;
    }

    /// <summary>
    /// Registros
    /// </summary>
    /// <value></value>
    public IEnumerable<GymBranchItem> Registers { get; set; }
}

/// <summary>
/// Item de sucursal de gimnasio
/// </summary>
public class GymBranchItem
{
    /// <summary>
    /// Guid de la sucursal
    /// </summary>
    /// <value></value> 
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    /// <value></value> 
    public string Name { get; set; }

    /// <summary>
    /// Calificación de la sucursal
    /// </summary>
    /// <value></value>
    public byte CalificationPercentage { get; set; }

    /// <summary>
    /// Dirección de la sucursal
    /// </summary>
    /// <value></value>
    public string Address { get; set; }

    /// <summary>
    /// URL de la imagen de la sucursal
    /// </summary>
    /// <value></value>
    public List<FileUrlResponse> ImageUrls { get; set; }

}

