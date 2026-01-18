using LogicCommon.Model.Response.File;

namespace LogicAdministratorApi.Model.Response.GymBranch;

/// <summary>
/// Respuesta de obtener sucursales de gimnasio paginadas
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetGymBranchesResponse(int totalRegister, IEnumerable<GymBranchItem> registers) : IPaginatorApiResponse<GymBranchItem>
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
    public IEnumerable<GymBranchItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de sucursal de gimnasio
/// </summary>
public class GymBranchItem
{
    /// <summary>
    /// Guid de la sucursal
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Dirección de la sucursal
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Teléfono de la sucursal
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Email de la sucursal
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Estado de la sucursal
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Latitud de la sucursal
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Longitud de la sucursal
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Fecha de apertura
    /// </summary>
    public DateTime OpeningDate { get; set; }

    /// <summary>
    /// URL de la imagen de la sucursal
    /// </summary>
    public FileUrlResponse ImageUrl { get; set; }
}

