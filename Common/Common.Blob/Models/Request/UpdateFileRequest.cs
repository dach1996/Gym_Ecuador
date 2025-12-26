namespace Common.Blob.Models.Request;

/// <summary>
/// Request para actualizar un archivo
/// </summary>
public class UpdateFileRequest
{
    /// <summary>
    /// Ruta del archivo
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// Items de actualización de archivo
    /// </summary>
    /// <value></value>
    public List<UpdateFileItemRequest> Items { get; set; }

    /// <summary>
    /// Constructor para actualizar un archivo
    /// </summary>
    /// <param name="path"></param>
    /// <param name="items"></param>
    public UpdateFileRequest(string path, List<UpdateFileItemRequest> items)
    {
        Path = path;
        Items = items;
    }

    /// <summary>
    /// Constructor para actualizar un archivo
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    public UpdateFileRequest(
        string path,
        string fileName,
        byte[] file,
        bool replaceIfExist
    )
    {
        Path = path;
        Items =
        [
            new() {
                FileName = fileName,
                File = file,
                ReplaceIfExist = replaceIfExist
            }
        ];
    }
}
public class UpdateFileItemRequest
{
    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Bytes del archivo
    /// </summary>
    public byte[] File { get; set; }

    /// <summary>
    /// Remplazar si existe
    /// </summary>
    public bool ReplaceIfExist { get; set; }
}