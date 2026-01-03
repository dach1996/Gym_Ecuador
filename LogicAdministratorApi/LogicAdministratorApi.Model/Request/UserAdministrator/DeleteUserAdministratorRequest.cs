using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserAdministrator;

/// <summary>
/// Solicitud para eliminar un usuario administrador
/// </summary>
public class DeleteUserAdministratorRequest : IApiBaseRequest<DeleteUserAdministratorResponse>
{
    /// <summary>
    /// GUID del usuario a eliminar
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

