using System.Text.Json.Serialization;
using LogicCommon.Model.Response;

namespace LogicCommon.Model.Request.File;
/// <summary>
/// Request para descargar archivo blob
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="fileName"></param>
/// <param name="path"></param>
/// <param name="contextRequest"></param>
public class DeleteBlobFileRequest(string fileName, string path, string implementation, CommonContextRequest contextRequest) : IRequest<GenericCommonOperationResponse>, ICommonBaseRequest
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

