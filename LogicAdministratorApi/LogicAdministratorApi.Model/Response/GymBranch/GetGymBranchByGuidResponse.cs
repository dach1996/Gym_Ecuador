using LogicCommon.Model.Response.File;

namespace LogicAdministratorApi.Model.Response.GymBranch;

/// <summary>
/// Respuesta de obtener detalle de sucursal de gimnasio por GUID
/// </summary>
public class GetGymBranchByGuidResponse(GymBranchDetail gymBranch) : IApiBaseResponse
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
    /// Datos de la sucursal
    /// </summary>
    public GymBranchDetail GymBranch { get; set; } = gymBranch;
}

/// <summary>
/// Detalle completo de sucursal de gimnasio
/// </summary>
public class GymBranchDetail
{
    /// <summary>
    /// Guid de la sucursal
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// GUID del gimnasio principal
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Código de la sucursal
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Descripción de la sucursal
    /// </summary>
    public string Description { get; set; }

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
    /// Latitud para localización
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Longitud para localización
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Capacidad máxima de personas
    /// </summary>
    public int MaxCapacity { get; set; }

    /// <summary>
    /// Número de pisos/plantas
    /// </summary>
    public byte FloorCount { get; set; }

    /// <summary>
    /// Estado de la sucursal
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de apertura de la sucursal
    /// </summary>
    public DateTime OpeningDate { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// URLs de las imágenes de la sucursal
    /// </summary>
    public List<FileUrlResponse> Images { get; set; }
}

