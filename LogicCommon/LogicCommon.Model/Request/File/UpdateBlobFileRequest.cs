using System.Text.Json.Serialization;
using LogicCommon.Model.Response.File;

namespace LogicCommon.Model.Request.File;
/// <summary>
/// Request para actualizar archivos blob
/// </summary>
public class UpdateBlobFileRequest(
    PathCode pathCode,
    List<UpdateBlobFileItemRequest> items,
    CommonContextRequest commonContextRequest,
    string folderPath = null
    ) : IRequest<UpdateFileResponse>, ICommonBaseRequest
{

    /// <summary>
    /// Código de la ruta
    /// </summary>
    public PathCode PathCode { get; set; } = pathCode;

    /// <summary>
    /// Ruta de la carpeta
    /// </summary>
    public string FolderPath { get; set; } = folderPath;

    /// <summary>
    /// Items de actualización de archivo
    /// </summary>
    public List<UpdateBlobFileItemRequest> Items { get; set; } = items;

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
        string folderPath = null,
        bool? replaceIfExist = true)
        : this(pathCode, [new UpdateBlobFileItemRequest { File = Convert.FromBase64String(fileEncode), FileName = fileName, ReplaceIfExist = replaceIfExist }], commonContextRequest, folderPath)
    {
    }
}
public class UpdateBlobFileItemRequest
{
    /// <summary>
    /// Archivo
    /// </summary>
    public byte[] File { get; set; }

    /// <summary>
    /// Nombre de archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Remplazar si Existe
    /// </summary>
    public bool? ReplaceIfExist { get; set; }
}