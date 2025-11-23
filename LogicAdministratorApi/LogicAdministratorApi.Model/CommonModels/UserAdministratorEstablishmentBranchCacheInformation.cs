namespace LogicAdministratorApi.Model.CommonModels;

/// <summary>
/// Información de Usuario Administrador por Sucursal
/// </summary>
public class UserAdministratorEstablishmentBranchCacheInformation
{
    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime DateTimeCreation { get; set; }

    /// <summary>
    /// Funcionalidades
    /// </summary>
    public IEnumerable<string> Functionalities { get; set; }
}