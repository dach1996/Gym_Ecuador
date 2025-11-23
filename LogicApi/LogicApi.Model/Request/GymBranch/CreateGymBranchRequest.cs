using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicApi.Model.Response.GymBranch;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para crear una sucursal de gimnasio
/// </summary>
public class CreateGymBranchRequest : IRequest<CreateGymBranchResponse>, IApiBaseRequest
{
    /// <summary>
    /// GUID del gimnasio principal
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// Código de la sucursal
    /// </summary>
    [StringLength(50)]
    public string Code { get; set; }

    /// <summary>
    /// Descripción de la sucursal
    /// </summary>
    [StringLength(1000)]
    public string Description { get; set; }

    /// <summary>
    /// Dirección de la sucursal
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Address { get; set; }

    /// <summary>
    /// Teléfono de la sucursal
    /// </summary>
    [StringLength(50)]
    public string Phone { get; set; }

    /// <summary>
    /// Email de la sucursal
    /// </summary>
    [StringLength(200)]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Latitud para localización
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitud para localización
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Capacidad máxima de personas
    /// </summary>
    public int? MaxCapacity { get; set; }

    /// <summary>
    /// Área en metros cuadrados
    /// </summary>
    public decimal? AreaSquareMeters { get; set; }

    /// <summary>
    /// Número de pisos/plantas
    /// </summary>
    public byte? FloorCount { get; set; }

    /// <summary>
    /// Tiene estacionamiento
    /// </summary>
    public bool HasParking { get; set; }

    /// <summary>
    /// Tiene vestuarios
    /// </summary>
    public bool HasLockerRooms { get; set; }

    /// <summary>
    /// Tiene regaderas
    /// </summary>
    public bool HasShowers { get; set; }

    /// <summary>
    /// Tiene wifi
    /// </summary>
    public bool HasWifi { get; set; }

    /// <summary>
    /// Es sucursal principal/matriz
    /// </summary>
    public bool IsMainBranch { get; set; }

    /// <summary>
    /// Fecha de apertura de la sucursal
    /// </summary>
    public DateTime? OpeningDate { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateGymBranchRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymBranchRequest()
    {
    }
}

