using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.Equipment;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.Equipment;

/// <summary>
/// Solicitud para obtener un equipamiento por GUID
/// </summary>
public class GetEquipmentByGuidRequest : IApiBaseRequest<GetEquipmentByGuidResponse>
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

