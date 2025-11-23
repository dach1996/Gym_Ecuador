namespace LogicAdministratorApi.Model.Response.Authorization;
/// <summary>
/// Objeto de respuesta para ingreso de sistema
/// </summary>
public class LoginResponse : IApiBaseResponse
{
    /// <summary>
    /// Secreto para acceso
    /// </summary>
    public string AccessSecret { get; set; }

    /// <summary>
    /// Establecimientos permitidos para el Usuario
    /// </summary>
    /// <value></value>
    public IEnumerable<EstablishmentAllowedItem> EstablishmentAllowedItems { get; set; }

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}

/// <summary>
/// Item de para Estableciminetos permitidos
/// </summary>
/// <param name="Guid"></param>
/// <param name="Name"></param>
/// <param name="EstablishmentBranchAllowedItems"></param>
/// <returns></returns>
public record EstablishmentAllowedItem(Guid Guid, string Name, IEnumerable<EstablishmentBranchAllowedItem> EstablishmentBranchAllowedItems);

/// <summary>
/// Item de para Sucursales de Estableciminetos permitidos
/// </summary>
/// <param name="Guid"></param>
/// <param name="Name"></param>
/// <returns></returns>
public record EstablishmentBranchAllowedItem(Guid Guid, string Name);