using System.Text.Json.Serialization;
using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Request.File;
/// <summary>
/// Request para descargar archivo blob
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="fileName"></param>
/// <param name="path"></param>
/// <param name="contextRequest"></param>
public class DownloadBlobFileRequest(string fileName, string path, string implementation, CommonContextRequest contextRequest) : IRequest<DownloadFileResponse>, ICommonBaseRequest
{

    /// <summary>
    /// Nombre de Archivo
    /// </summary>
    public string FileName { get; set; } = fileName;

    /// <summary>
    /// Ruta o Contenedor
    /// </summary>
    public string Path { get; set; } = path;

    /// <summary>
    /// Implementación
    /// </summary>
    public string Implementation { get; set; } = implementation;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; } = contextRequest;
}
