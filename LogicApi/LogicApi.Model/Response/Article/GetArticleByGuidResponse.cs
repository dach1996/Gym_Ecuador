namespace LogicApi.Model.Response.Article;

/// <summary>
/// Response to get article detail by GUID
/// </summary>
public class GetArticleByGuidResponse(ArticleDetail article) : IApiBaseResponse
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
    /// Article detail
    /// </summary>
    public ArticleDetail Article { get; set; } = article;
}

/// <summary>
/// Article detail
/// </summary>
public class ArticleDetail : ArticleItem
{
    /// <summary>
    /// Full content of the article
    /// </summary>
    public string FullContent { get; set; }

    /// <summary>
    /// Publication date
    /// </summary>
    public DateTime PublicationDate { get; set; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreationDate { get; set; }
}

