namespace LogicAdministratorApi.Model.CommonModels;
/// <summary>
/// Roles y Funcionalidades por Sesión de Usuario
/// </summary>
public class RolUserSession(IDictionary<Guid, string[]> functionalitiesEstablishmentBranch)
{
    /// <summary>
    /// Diccionario de Sucursales con funcionalidades
    /// </summary>
    /// <value></value>
    public IDictionary<Guid, string[]> FunctionalitiesEstablishmentBranch { get; set; } = functionalitiesEstablishmentBranch;
}