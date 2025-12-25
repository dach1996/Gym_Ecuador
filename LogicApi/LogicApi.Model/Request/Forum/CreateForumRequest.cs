using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Forum;
using Common.WebCommon.Models;

namespace LogicApi.Model.Request.Forum;

/// <summary>
/// Solicitud para crear un foro
/// </summary>
public class CreateForumRequest : IRequest<CreateForumResponse>, IApiBaseRequest
{
    /// <summary>
    /// Título del foro
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    /// <summary>
    /// Descripción del foro
    /// </summary>
    [Required]
    [StringLength(5000)]
    public string Description { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateForumRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateForumRequest()
    {
    }
}

