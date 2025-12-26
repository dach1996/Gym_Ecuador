using System.Text.Json.Serialization;
using LogicCommon.Model.Response.File;

namespace LogicCommon.Model.Request.File;
/// <summary>
/// Request para eliminar archivos blob por guids   
/// </summary>
/// <typeparam name="Guid"></typeparam>
public class DeleteBlobFileByGuidRequest(List<Guid> guids, CommonContextRequest contextRequest) : IRequest<DeleteFileResponse>, ICommonBaseRequest
{
    /// <summary>
    /// Guids de los archivos a eliminar
    /// </summary>
    /// <value></value>
    public List<Guid> Guids { get; set; } = guids;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; } = contextRequest;
}

