using LogicApi.Model.Response.BranchEquipment;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.BranchEquipment;

/// <summary>
/// Solicitud para obtener detalle de equipo de sucursal por GUID
/// </summary>
public class GetBranchEquipmentByGuidRequest : IApiBaseRequest<GetBranchEquipmentByGuidResponse>
{
    /// <summary>
    /// Guid del equipo de sucursal
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

