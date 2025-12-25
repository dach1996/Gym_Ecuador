using Common.Blob.Models;
using Common.Blob.Models.Response;

namespace Common.Blob;
public interface IBlobBus
{
    /// <summary>
    /// Permite subir un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    Task<UpdateFileResponse> UpdateFileAsync(string fileName, string path, Stream fileStream, bool replaceIfExist);

    /// <summary>
    /// Permite subir un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    Task<BlobFile> UpdateAndGetFileAsync(string fileName, string path, Stream fileStream, bool replaceIfExist);

    /// <summary>
    /// Permite descargar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    Task<BlobFile> DownloadFileAsync(string fileName, string path);

    /// <summary>
    /// Permite Eliminar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    Task DeleteFileAsync(string fileName, string path);
}
