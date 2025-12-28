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
    /// Identificador del archivo
    /// </summary>
    public Guid Identifier { get; set; }

    /// <summary>
    /// Id del archivo
    /// </summary>
    public bool Success { get; set; }
}