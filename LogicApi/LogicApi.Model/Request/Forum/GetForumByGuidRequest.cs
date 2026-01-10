using LogicApi.Model.Response.Forum;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Forum;

/// <summary>
/// Request to get forum detail by GUID
/// </summary>
public class GetForumByGuidRequest : IApiBaseRequest<GetForumByGuidResponse>
{
    /// <summary>
    /// Forum GUID
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid ForumGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

