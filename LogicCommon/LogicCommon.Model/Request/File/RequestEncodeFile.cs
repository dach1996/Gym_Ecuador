using System.ComponentModel.DataAnnotations;
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
}
