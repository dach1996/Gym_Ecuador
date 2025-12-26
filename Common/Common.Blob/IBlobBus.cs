using Common.Blob.Models;
using Common.Blob.Models.Request;
using Common.Blob.Models.Response;

namespace Common.Blob;
public interface IBlobBus
{
    /// <summary>
    /// Actualizar archivos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<UpdateFileResponse> UpdateFileAsync(UpdateFileRequest request);

    /// <summary>
    /// Permite subir un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    Task<BlobFile> UpdateAndGetFileAsync(string fileName, string path, byte[] file, bool replaceIfExist);

    /// <summary>
    /// Permite descargar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    Task<BlobFile> DownloadFileAsync(string fileName, string path);

    /// <summary>
    /// Permite eliminar archivos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request);
}
