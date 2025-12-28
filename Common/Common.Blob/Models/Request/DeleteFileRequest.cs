namespace Common.Blob.Models.Request;

/// <summary>
/// Request para actualizar un archivo
/// </summary>
public class DeleteFileRequest(List<DeleteFileItemRequest> items)
{
    /// <summary>
    /// Guids de los archivos a eliminar
    /// </summary>
    /// <value></value>
    public List<DeleteFileItemRequest> Items { get; set; } = items;

    /// <summary>
    /// Constructor para eliminar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public DeleteFileRequest(string fullPathName) : this([new DeleteFileItemRequest { FullPathName = fullPathName }]) { }
}
public class DeleteFileItemRequest
{
    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string FullPathName { get; set; }

    /// <summary>
    /// Identificador del archivo
    /// </summary>
    public Guid Identifier { get; set; }
}