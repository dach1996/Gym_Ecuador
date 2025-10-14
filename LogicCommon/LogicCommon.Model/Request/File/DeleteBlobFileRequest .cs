using System.Text.Json.Serialization;
using LogicCommon.Model.Response;

namespace LogicCommon.Model.Request.File;
/// <summary>
/// Request para descargar archivo blob
/// </summary>
public class DeleteBlobFileRequest : IRequest<GenericCommonOperationResponse>, ICommonBaseRequest
{
    /// <summary>
    /// Nombre de Archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Ruta o Contenedor
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="contextRequest"></param>
    public DeleteBlobFileRequest(string fileName, string path, CommonContextRequest contextRequest)
    {
        FileName = fileName;
        Path = path;
        CommonContextRequest = contextRequest;
    }
}

