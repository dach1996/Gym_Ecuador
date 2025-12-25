namespace LogicCommon.Model.CacheModel;
/// <summary>
///     Información del usuario en caché
/// </summary>
public class FileBasePathCacheInformation
{
    /// <summary>
    ///     Id de la ruta de almacenamiento
    /// </summary>
    /// <value></value>
    public byte Id { get; set; }
     
    /// <summary>
    ///     Código de la ruta de almacenamiento
    /// </summary>
    /// <value></value>
    public PathCode PathCode { get; set; }

    /// <summary>
    ///     URL base de la ruta de almacenamiento
    /// </summary>
    /// <value></value>
    public string BaseUrl { get; set; }

    /// <summary>
    ///     Ruta del directorio de archivos
    /// </summary>
    /// <value></value>
    public string FileDirectoryPath { get; set; }

    /// <summary>
    ///     Implementación de la ruta de almacenamiento
    /// </summary>
    /// <value></value>
    public string Implementation { get; set; }
}