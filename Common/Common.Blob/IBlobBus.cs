using Common.Blob.Models;

namespace Common.Blob;
public interface IBlobBus
{
     /// <summary>
    /// Permite subir un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blocContainerName"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    Task UpdateFileAsync(string fileName, string blocContainerName, Stream fileStream, bool replaceIfExist);

    /// <summary>
    /// Permite subir un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blocContainerName"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    Task<BlobFile> UpdateAndGetFileAsync(string fileName, string blocContainerName, Stream fileStream, bool replaceIfExist);

    /// <summary>
    /// Permite descargar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blobContainerName"></param>
    /// <returns></returns>
    Task<BlobFile> DownloadFileAsync(string fileName, string blobContainerName);

    /// <summary>
    /// Permite Eliminar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blobContainerName"></param>
    /// <returns></returns>
    Task DeleteFileAsync(string fileName, string blobContainerName);
}
