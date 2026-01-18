namespace LogicAdministratorApi.Model.Response.GymBranch;

/// <summary>
/// Respuesta de obtener sucursales de gimnasio por funcionalidad
/// </summary>
public class GetGymBranchesByFunctionalityResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Lista de sucursales con acceso a la funcionalidad
    /// </summary>
    public IEnumerable<GymByFunctionalityItem> Branches { get; set; } = new List<GymByFunctionalityItem>();
}

/// <summary>
/// Item de sucursal filtrada por funcionalidad
/// </summary>
public class GymByFunctionalityItem
{
    /// <summary>
    /// Guid de la sucursal
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Lista de sucursales
    /// </summary>
    public IEnumerable<GymBranchByFunctionalityItem> Branches { get; set; } = [];

    /// <summary>
    /// Item de sucursal
    /// </summary>
    public class GymBranchByFunctionalityItem
    {
        /// <summary>
        /// Guid de la sucursal
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Nombre de la sucursal
        /// </summary>
        public string Name { get; set; }
    }
}
