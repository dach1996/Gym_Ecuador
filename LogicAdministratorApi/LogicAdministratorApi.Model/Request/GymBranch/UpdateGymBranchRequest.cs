using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.GymBranch;
using Common.WebCommon.Models;
using LogicCommon.Model.Request.File;

namespace LogicAdministratorApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para actualizar una sucursal de gimnasio
/// </summary>
public class UpdateGymBranchRequest : IApiBaseRequest<UpdateGymBranchResponse>
{
    /// <summary>
    /// GUID de la sucursal a actualizar
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid GymBranchGuid { get; set; }

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
    /// Fecha de apertura de la sucursal
    /// </summary>
    public DateTime OpeningDate { get; set; }

    /// <summary>
    /// Estado activo de la sucursal
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// Imagen
    /// </summary>
    /// <value></value>
    public List<RequestEncodeFile> Images { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

}

