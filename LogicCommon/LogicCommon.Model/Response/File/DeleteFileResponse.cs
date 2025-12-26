namespace LogicCommon.Model.Response.File;
/// <summary>
/// Respuesta de actualización de archivo
/// </summary>
public class DeleteFileResponse(List<DeleteFileItemResponse> items)
{
    /// <summary>
    /// Items de actualización de archivo
    /// </summary>
    public List<DeleteFileItemResponse> Items { get; set; } = items;
}
/// <summary>
/// Respuesta de actualización de archivo item
/// </summary>
public class DeleteFileItemResponse
{
    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del archivo
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Success
    /// </summary>
    public bool Success { get; set; }
}

