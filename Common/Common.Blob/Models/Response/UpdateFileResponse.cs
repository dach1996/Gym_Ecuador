namespace Common.Blob.Models.Response;

/// <summary>
/// Respuesta de actualización de archivo
/// </summary>
public class UpdateFileResponse
{
    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Ruta del archivo
    /// </summary>
    public string Path { get; set; }
}