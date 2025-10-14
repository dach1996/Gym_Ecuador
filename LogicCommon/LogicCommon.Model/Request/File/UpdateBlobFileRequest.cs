using System.Text.Json.Serialization;
using LogicCommon.Model.Response.File;

namespace LogicCommon.Model.Request.File;
/// <summary>
/// Request para cargar archivo blob
/// </summary>
public class UpdateBlobFileRequest : IRequest<FileResponse>, ICommonBaseRequest
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
    /// Extension
    /// </summary>
    public string FileExtension { get; set; }

    /// <summary>
    /// Dirección
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Remplazar si Existe
    /// </summary>
    public bool? ReplaceIfExist { get; set; }


    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="file"></param>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileExtension"></param>
    /// <param name="CommonContextRequest"></param>
    /// <param name="replaceIfExist"></param>
    public UpdateBlobFileRequest(byte[] file, string fileName, string path, CommonContextRequest commonContextRequest, string fileExtension = "", bool? replaceIfExist = true)
    {
        File = file;
        var extension = !string.IsNullOrEmpty(fileExtension) ? $".{fileExtension}" : string.Empty;
        FileName = $"{fileName}{extension}";
        Path = path;
        ReplaceIfExist = replaceIfExist;
        CommonContextRequest = commonContextRequest;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileEncode"></param>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileExtension"></param>
    /// <param name="CommonContextRequest"></param>
    /// <param name="replaceIfExist"></param>
    public UpdateBlobFileRequest(string fileEncode, string fileName, string path, CommonContextRequest commonContextRequest, string fileExtension = "", bool? replaceIfExist = true)
    {
        var bytes = Convert.FromBase64String(fileEncode);
        File = bytes;
        var extension = !string.IsNullOrEmpty(fileExtension) ? $".{fileExtension}" : string.Empty;
        FileName = $"{fileName}{extension}";
        Path = path;
        ReplaceIfExist = replaceIfExist;
        CommonContextRequest = commonContextRequest;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="file"></param>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileExtension"></param>
    /// <param name="CommonContextRequest"></param>
    /// <param name="replaceIfExist"></param>
    public UpdateBlobFileRequest(Stream file, string fileName, string path, CommonContextRequest commonContextRequest, string fileExtension = "", bool? replaceIfExist = true)
    {
        using MemoryStream ms = new();
        file.CopyTo(ms);
        File = ms.ToArray();
        var extension = !string.IsNullOrEmpty(fileExtension) ? $".{fileExtension}" : string.Empty;
        FileName = $"{fileName}{extension}";
        Path = path;
        ReplaceIfExist = replaceIfExist;
        CommonContextRequest = commonContextRequest;
    }
}
