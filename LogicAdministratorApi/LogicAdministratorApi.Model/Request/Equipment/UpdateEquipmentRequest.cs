using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.Equipment;
using Common.WebCommon.Models;
using LogicCommon.Model.Request.File;

namespace LogicAdministratorApi.Model.Request.Equipment;

/// <summary>
/// Solicitud para actualizar un equipamiento
/// </summary>
public class UpdateEquipmentRequest : IApiBaseRequest<UpdateEquipmentResponse>
{
    /// <summary>
    /// GUID del equipamiento
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid EquipmentGuid { get; set; }

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
    /// Estado del registro (Activo/Inactivo)
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

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
