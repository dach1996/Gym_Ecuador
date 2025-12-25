using System.Text.Json.Serialization;
using LogicCommon.Model.Response.File;

namespace LogicCommon.Model.Request.File;
/// <summary>
/// Request para cargar archivo blob
/// </summary>
/// <remarks>
/// Constructor para archivo en bytes
/// </remarks>
/// <param name="file"></param>
/// <param name="fileName"></param>
/// <param name="pathCode"></param>
/// <param name="commonContextRequest"></param>
/// <param name="path"></param>
/// <param name="replaceIfExist"></param>
/// <returns></returns>
public class UpdateBlobFileRequest(
    byte[] file,
    string fileName,
    PathCode pathCode,
    CommonContextRequest commonContextRequest,
    string path = null,
    bool? replaceIfExist = true
    ) : IRequest<UpdateFileResponse>, ICommonBaseRequest
{
    /// <summary>
    /// Archivo
    /// </summary>
    public byte[] File { get; set; } = file;

    /// <summary>
    /// Nombre de archivo
    /// </summary>
    public string FileName { get; set; } = fileName;

    /// <summary>
    /// Dirección
    /// </summary>
    public string Path { get; set; } = path;

    /// <summary>
    /// Remplazar si Existe
    /// </summary>
    public bool? ReplaceIfExist { get; set; } = replaceIfExist;

    /// <summary>
    /// Código de la ruta
    /// </summary>
    public PathCode PathCode { get; set; } = pathCode;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; } = commonContextRequest;

    /// <summary>
    /// Constructor para codificar el archivo
    /// </summary>
    /// <param name="fileEncode"></param>
    /// <param name="fileName"></param>
    /// <param name="pathCode"></param>
    /// <param name="commonContextRequest"></param>
    /// <param name="path"></param>
    /// <param name="replaceIfExist"></param>
    public UpdateBlobFileRequest(
        string fileEncode,
        string fileName,
        PathCode pathCode,
        CommonContextRequest commonContextRequest,
        string path = null,
        bool? replaceIfExist = true)
        : this(Convert.FromBase64String(fileEncode), fileName, pathCode, commonContextRequest, path, replaceIfExist)
    {
    }
}
