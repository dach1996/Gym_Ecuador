namespace LogicApi.Model.Response.Forum;

/// <summary>
/// Response to get forum detail by GUID
/// </summary>
public class GetForumByGuidResponse(ForumDetail forum) : IApiBaseResponse
{
    /// <summary>
    /// User message
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Show message?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Forum detail
    /// </summary>
    public ForumDetail Forum { get; set; } = forum;
}

/// <summary>
/// Forum detail
/// </summary>
public class ForumDetail : ForumItem
{
    /// <summary>
    /// Forum category
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Full content of the forum
    /// </summary>
    public string FullContent { get; set; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Author name
    /// </summary>
    public string AuthorName { get; set; }

    /// <summary>
    /// Forum comments
    /// </summary>
    public List<ForumCommentItem> Comments { get; set; }
}

/// <summary>
/// Forum comment item
/// </summary>
public class ForumCommentItem
{
    /// <summary>
    /// Comment GUID
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Comment text
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Author name
    /// </summary>
    public string AuthorName { get; set; }

    /// <summary>
    /// Registration date
    /// </summary>
    public DateTime RegistrationDate { get; set; }
}

