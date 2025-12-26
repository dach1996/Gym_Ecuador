using System.ComponentModel.DataAnnotations;
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
    [Required]
    [JsonCompresse]
    public string EncodeContent { get; set; }

    /// <summary>
    /// Extensión
    /// </summary>
    [Required]
    public string FileExtension { get; set; }

    /// <summary>
    /// Nombre de Archivo
    /// </summary>
    [Required]
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