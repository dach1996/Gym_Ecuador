namespace LogicApi.Model.Response.GroupClass;

/// <summary>
/// Respuesta de crear clase grupal
/// </summary>
public class CreateGroupClassResponse : IApiBaseResponse
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
    /// Guid de la clase grupal creada
    /// </summary>
    public Guid GroupClassGuid { get; set; }

    /// <summary>
    /// Nombre de la clase
    /// </summary>
    public string ClassName { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="groupClassGuid"></param>
    /// <param name="className"></param>
    public CreateGroupClassResponse(Guid groupClassGuid, string className)
    {
        GroupClassGuid = groupClassGuid;
        ClassName = className;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGroupClassResponse()
    {
    }
}
