using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Forum;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Forum;

/// <summary>
/// Solicitud para comentar en un foro
/// </summary>
public class CommentForumRequest : IRequest<CommentForumResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del foro
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid ForumGuid { get; set; }

    /// <summary>
    /// Comentario
    /// </summary>
    [Required]
    [StringLength(2000)]
    public string Comment { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CommentForumRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CommentForumRequest()
    {
    }
}

