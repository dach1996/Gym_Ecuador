namespace LogicCommon.Model.Response.File;
/// <summary>
/// Respuesta de URL de archivo
/// </summary>
public class FileUrlResponse
{
    /// <summary>
    /// Guid del archivo
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Ruta del archivo
    /// </summary>
    public string Url { get; set; }
}



