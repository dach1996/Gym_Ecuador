namespace LogicCommon.Model.Response.File;
/// <summary>
/// Respuesta de archivo
/// </summary>
public class DownloadFileResponse
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
    /// Contenido del archivo
    /// </summary>
    public byte[] Content { get; set; }
}

