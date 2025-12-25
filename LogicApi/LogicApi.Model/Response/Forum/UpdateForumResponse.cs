namespace LogicApi.Model.Response.Forum;

/// <summary>
/// Respuesta de actualizar foro
/// </summary>
public class UpdateForumResponse : IApiBaseResponse
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
    /// Guid del foro actualizado
    /// </summary>
    public Guid ForumGuid { get; set; }

    /// <summary>
    /// Título del foro
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="forumGuid"></param>
    /// <param name="title"></param>
    public UpdateForumResponse(Guid forumGuid, string title)
    {
        ForumGuid = forumGuid;
        Title = title;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateForumResponse()
    {
    }
}

