using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.Equipment;
using Common.WebCommon.Models;
using LogicCommon.Model.Request.File;

namespace LogicAdministratorApi.Model.Request.Equipment;

/// <summary>
/// Solicitud para crear un equipamiento
/// </summary>
public class CreateEquipmentRequest : IApiBaseRequest<CreateEquipmentResponse>
{
    /// <summary>
    /// GUID de la sucursal de gimnasio
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Nombre del equipamiento
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del equipamiento
    /// </summary>
    [StringLength(1000)]
    public string Description { get; set; }

    /// <summary>
    /// Id del tipo de equipamiento (catálogo)
    /// </summary>
    [Required]
    public string EquipmentTypeCatalogCode { get; set; }

    /// <summary>
    /// Imágenes del equipamiento
    /// </summary>
    public List<RequestEncodeFile> Images { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
