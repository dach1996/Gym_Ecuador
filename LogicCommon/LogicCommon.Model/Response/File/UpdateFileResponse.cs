namespace LogicCommon.Model.Response.File;
/// <summary>
/// Respuesta de actualización de archivo
/// </summary>
public class UpdateFileResponse
{
    /// <summary>
    /// Items de actualización de archivo
    /// </summary>
    public List<UpdateFileItemResponse> Items { get; set; }
}
/// <summary>
/// Respuesta de actualización de archivo item
/// </summary>
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
    public int Id { get; set; }
}

