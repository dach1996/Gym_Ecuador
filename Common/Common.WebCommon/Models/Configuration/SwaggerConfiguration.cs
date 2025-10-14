namespace Common.WebCommon.Models.Configuration;
/// <summary>
/// Configuración de Swagger
/// </summary>
public class SwaggerConfiguration
{
    /// <summary>
    /// Nombre de Aplicación
    /// </summary>
    /// <value></value>
    public string ApplicationName { get; set; }

    /// <summary>
    /// Ruta
    /// </summary>
    /// <value></value>
    public string Path { get; set; }

    /// <summary>
    /// Versión
    /// </summary>
    /// <value></value>
    public string Version { get; set; }

    /// <summary>
    /// Título
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    /// <value></value>
    public string Description { get; set; }

    /// <summary>
    /// Nombres de documentos Xml
    /// </summary>
    /// <value></value>
    public IEnumerable<string> DocumentsXml { get; set; }
}