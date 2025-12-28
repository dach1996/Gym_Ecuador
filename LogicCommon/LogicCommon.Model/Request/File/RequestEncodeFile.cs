using Common.WebCommon.Attributes.CustomDataAnnotations;
using Common.WebCommon.Json;
namespace LogicCommon.Model.Request.File;

/// <summary>
/// Request de archivo codificado
/// </summary>
public class RequestEncodeFile
{
    /// <summary>
    /// Contenido Codificado
    /// </summary>
    [JsonCompresse]
    public string EncodeContent { get; set; }

    /// <summary>
    /// Extensión
    /// </summary>
    public string FileExtension { get; set; }

    /// <summary>
    /// Nombre de Archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Guid del archivo
    /// </summary>
    public Guid? Guid { get; set; }

    /// <summary>
    /// Acción a realizar con el archivo
    /// </summary>
    [ValidateEnum]
    public ActionFile Action { get; set; }

    /// <summary>
    /// Convierte a RequestEncodeFile para crear
    /// </summary>
    /// <param name="encodeContent"></param>
    /// <param name="fileExtension"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static RequestEncodeFile ToCreate(string encodeContent, string fileExtension, string fileName) => new()
    {
        EncodeContent = encodeContent,
        FileExtension = fileExtension,
        FileName = fileName,
        Action = ActionFile.Create
    };

    /// <summary>
    /// Convierte a RequestEncodeFile para eliminar
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static RequestEncodeFile ToDelete(Guid guid) => new()
    {
        Guid = guid,
        Action = ActionFile.Delete
    };
}
/// <summary>
/// Acciones para el archivo
/// </summary>
public enum ActionFile
{
    /// <summary>
    /// Crear archivo
    /// </summary>
    Create = 1,
    /// <summary>
    /// Eliminar archivo
    /// </summary>
    Delete = 2,
    /// <summary>
    /// No realizar ninguna acción
    /// </summary>
    None = 3
}
