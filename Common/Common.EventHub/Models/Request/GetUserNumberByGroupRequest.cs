namespace Common.EventHub.Models.Request;
/// <summary>
/// Request para obtener número de usuarios por grupo
/// </summary>
public class GetUserNumberByGroupRequest(string groupName)
{
    /// <summary>
    /// Nombre de Grupo
    /// </summary>
    /// <value></value>
    public string GroupName { get; set; } = groupName;
}
