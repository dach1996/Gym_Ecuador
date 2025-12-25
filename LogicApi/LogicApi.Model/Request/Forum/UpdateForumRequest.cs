using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Forum;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Forum;

/// <summary>
/// Solicitud para actualizar un foro
/// </summary>
public class UpdateForumRequest : IRequest<UpdateForumResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del foro
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid ForumGuid { get; set; }

    /// <summary>
    /// Título del foro
    /// </summary>
    [StringLength(200)]
    public string Title { get; set; }

    /// <summary>
    /// Descripción del foro
    /// </summary>
    [StringLength(5000)]
    public string Description { get; set; }

    /// <summary>
    /// Estado activo del foro
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateForumRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateForumRequest()
    {
    }
}

