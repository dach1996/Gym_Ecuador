namespace Common.Blob.Models;
public class BlobFileInformation
{
    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Última fecha de Modificación
    /// </summary>
    public DateTimeOffset? LastModified { get; set; }

    /// <summary>
    /// Tamaño
    /// </summary>
    public long? Length { get; set; }

    /// <summary>
    /// Tipo de Contenido
    /// </summary>
    public string ContentType { get; internal set; }

    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; internal set; }
}

public class BlobFile : BlobFileInformation
{
    /// <summary>
    /// Archivo
    /// </summary>
    public byte[] Content { get; set; }
}

