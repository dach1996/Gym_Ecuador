using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.Equipment;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.Equipment;

/// <summary>
/// Solicitud para eliminar un equipamiento
/// </summary>
public class DeleteEquipmentRequest : IApiBaseRequest<DeleteEquipmentResponse>
{
    /// <summary>
    /// GUID del equipamiento
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid EquipmentGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

