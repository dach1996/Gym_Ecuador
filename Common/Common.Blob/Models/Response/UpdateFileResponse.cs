namespace Common.Blob.Models.Response;

/// <summary>
/// Respuesta de actualización de archivo
/// </summary>
public class UpdateFileResponse
{
    /// <summary>
    /// Items de actualización de archivo
    /// </summary>
    /// <value></value>
    public List<UpdateFileItemResponse> Items { get; set; }
}

public class UpdateFileItemResponse
{
    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Ruta del archivo
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Id del archivo
    /// </summary>
    public bool Success { get; set; }
}