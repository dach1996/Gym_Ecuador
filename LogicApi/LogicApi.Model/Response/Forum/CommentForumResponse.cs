namespace LogicApi.Model.Response.Forum;

/// <summary>
/// Respuesta de comentar en foro
/// </summary>
public class CommentForumResponse : IApiBaseResponse
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
    /// Guid del comentario creado
    /// </summary>
    public Guid CommentGuid { get; set; }

    /// <summary>
    /// Comentario
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="commentGuid"></param>
    /// <param name="comment"></param>
    public CommentForumResponse(Guid commentGuid, string comment)
    {
        CommentGuid = commentGuid;
        Comment = comment;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CommentForumResponse()
    {
    }
}

