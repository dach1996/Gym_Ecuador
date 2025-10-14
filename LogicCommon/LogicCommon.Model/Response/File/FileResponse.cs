namespace LogicCommon.Model.Response.File;
/// <summary>
/// Respuesta de archivo
/// </summary>
public class FileResponse
{

    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Última fecha de Modificación
    /// </summary>
    public DateTimeOffset? LastModified { get; set; }

    /// <summary>
    /// Tamaño
    /// </summary>
    public long? Length { get; set; }

    /// <summary>
    /// Tipo de Contenido
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Archivo
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; set; }
}

