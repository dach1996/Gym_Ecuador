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
    /// Constructor
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="url"></param>
    public FileUrlResponse(Guid guid, string url)
    {
        Guid = guid;
        Url = url;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="url"></param>
    public FileUrlResponse(Guid guid, string baseUrl, string path)
    {
        Guid = guid;
        Url = $"{baseUrl}{path}";
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public FileUrlResponse()
    {
    }

    /// <summary>
    /// Ruta del archivo
    /// </summary>
    public string Url { get; set; }
}



