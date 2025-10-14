using System.Text.Json.Serialization;
using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Request.File;
/// <summary>
/// Request para descargar archivo blob
/// </summary>
public class DownloadBlobFileRequest : IRequest<FileResponse>, ICommonBaseRequest
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="connectionString"></param>
    /// <param name="contextRequest"></param>
    public DownloadBlobFileRequest(string fileName, string path, string connectionString, CommonContextRequest contextRequest)
    {
        FileName = fileName;
        Path = path;
        ConnectionString = connectionString;
        CommonContextRequest = contextRequest;
    }

    /// <summary>
    /// Nombre de Archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Ruta o Contenedor
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Conexión
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; }
}
