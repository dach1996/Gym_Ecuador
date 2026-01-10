using LogicApi.Model.Response.Article;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Article;

/// <summary>
/// Request to get article detail by GUID
/// </summary>
public class GetArticleByGuidRequest : IApiBaseRequest<GetArticleByGuidResponse>
{
    /// <summary>
    /// Article GUID
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid ArticleGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

