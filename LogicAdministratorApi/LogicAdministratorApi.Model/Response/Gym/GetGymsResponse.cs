namespace LogicAdministratorApi.Model.Response.Gym;

/// <summary>
/// Respuesta de obtener gimnasios paginados
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetGymsResponse(int totalRegister, IEnumerable<GymItem> registers) : IPaginatorApiResponse<GymItem>
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
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; } = totalRegister;

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<GymItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de gimnasio
/// </summary>
public class GymItem
{
    /// <summary>
    /// Id del gimnasio
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Estado del gimnasio
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Sucursales del gimnasio
    /// </summary>
    public IEnumerable<GymBranchPartialItem> GymBranches { get; set; }

    /// <summary>
    /// Item de sucursal de gimnasio
    /// </summary>
    public class GymBranchPartialItem
    {
        /// <summary>
        /// Guid de la sucursal
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Nombre de la sucursal
        /// </summary>
        public string Name { get; set; }
    
         /// <summary>
         /// Cantidad de miembros
         /// </summary>
         public int MemberCount { get; set; }
    }
}

