namespace LogicAdministratorApi.Model.Response.GymBranch;

/// <summary>
/// Respuesta de actualizar sucursal de gimnasio
/// </summary>
public class UpdateGymBranchResponse : IApiBaseResponse
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
    /// Guid de la sucursal actualizada
    /// </summary>
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Código de la sucursal
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Guid del gimnasio principal
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gymBranchGuid"></param>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="gymGuid"></param>
    public UpdateGymBranchResponse(Guid gymBranchGuid, string name, string code, Guid gymGuid)
    {
        GymBranchGuid = gymBranchGuid;
        Name = name;
        Code = code;
        GymGuid = gymGuid;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymBranchResponse()
    {
    }
}

