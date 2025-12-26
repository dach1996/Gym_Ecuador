namespace Common.Blob.Models.Response;

/// <summary>
/// Respuesta de actualización de archivo
/// </summary>
public class DeleteFileResponse
{
    /// <summary>
    /// Items de actualización de archivo
    /// </summary>
    /// <value></value>
    public List<DeleteFileItemResponse> Items { get; set; }
}

public class DeleteFileItemResponse
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