namespace LogicAdministratorApi.Model.Response.GymBranch;

/// <summary>
/// Respuesta de crear sucursal de gimnasio
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="gymBranchGuid"></param>
/// <param name="name"></param>
/// <param name="code"></param>
/// <param name="gymGuid"></param>
public class CreateGymBranchResponse(Guid gymBranchGuid, string name, string code, Guid gymGuid) : IApiBaseResponse
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
    /// Guid de la sucursal creada
    /// </summary>
    public Guid GymBranchGuid { get; set; } = gymBranchGuid;

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Código de la sucursal
    /// </summary>
    public string Code { get; set; } = code;

    /// <summary>
    /// Guid del gimnasio principal
    /// </summary>
    public Guid GymGuid { get; set; } = gymGuid;
}

