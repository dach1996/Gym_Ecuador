using Common.Blob.BlobException;
using Common.Blob.Models;
using Common.Blob.Models.Request;
using Common.Blob.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Common.Blob.Implementations.Local;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="configuration"></param>
public class LocalStorageBlobBus(
    ILogger<LocalStorageBlobBus> logger,
    IConfiguration configuration) : BlobBusBase(logger, configuration)
{
    protected override BlobImplementationNames Implementation => BlobImplementationNames.LocalStorage;

    /// <summary>
    /// Descargar Imagen
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blobContainerName"></param>
    /// <returns></returns>
    /// <exception cref="CustomBlobException"></exception>
    public override async Task<BlobFile> DownloadFileAsync(string fileName, string path)
    {
        var finalPath = GetFinalPathDirectory(path);
        try
        {
            //Verificamos que la imágen no esté vacía
            var pathExists = Directory.Exists(finalPath);
            if (!pathExists)
                throw new CustomBlobException($"No existe la ruta: '{finalPath}'");
            var filePath = $"{finalPath}/{fileName}";
            var file = new FileInfo(filePath);
            if (!file.Exists)
                throw new CustomBlobException($"No existe el archivo: '{filePath}'");

            return new BlobFile
            {
                Length = file.Length,
                LastModified = file.LastWriteTime,
                FileName = file.Name,
                Content = await File.ReadAllBytesAsync(filePath).ConfigureAwait(false)
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al descargar la Archivo: " +
               "Folder: '{@Path}' - " +
               "FileName: '{@FileName}' - " +
               "Message: '{@Message}'", finalPath, fileName, ex.Message);
            if (ex.InnerException is not null)
                logger.LogError(ex.InnerException, "Inner Exception: " +
               "Folder: '{@Path}' - " +
               "FileName: '{@FileName}' - " +
               "Message: '{@Message}'", finalPath, fileName, ex.InnerException.Message);
            throw new CustomBlobException(ex.Message);
        }
    }

    /// <summary>
    /// Descargar Imagen
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blobContainerName"></param>
    /// <returns></returns>
    /// <exception cref="CustomBlobException"></exception>
    public override async Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request)
    {
        var results = new List<DeleteFileItemResponse>();
        foreach (var item in request.Items)
        {
            var finalPath = GetFinalPathDirectory(item.FullPathName);
            try
            {

                var file = new FileInfo(finalPath);
                //Validamos si existe la imagen para eliminarlo
                if (!file.Exists)
                    throw new CustomBlobException($"No existe el archivo en la ruta: {finalPath}");
                await Task.Run(file.Delete).ConfigureAwait(false);
                logger.LogInformation("Archivo {@FileName} eliminado correctamente en la ruta: '{@Path}'", file.Name, finalPath);
                results.Add(new DeleteFileItemResponse
                {
                    Identifier = item.Identifier,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al Eliminar la Archivo: " +
                  "Folder: '{@Path}' - " +
                  "FileName: '{@FileName}' - " +
                  "Message: '{@Message}'", finalPath, item.FullPathName, ex.Message);
                if (ex.InnerException is not null)
                    logger.LogError(ex.InnerException, "Inner Exception: " +
                   "Folder: '{@Path}' - " +
                   "FileName: '{@FileName}' - " +
                   "Message: '{@Message}'", finalPath, item.FullPathName, ex.InnerException.Message);
                results.Add(new DeleteFileItemResponse
                {
                    Identifier = item.Identifier,
                    Success = false
                });
            }
        }
        return new DeleteFileResponse
        {
            Items = results
        };
    }

    /// <summary>
    /// Actualizar archivos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override async Task<UpdateFileResponse> UpdateFileAsync(UpdateFileRequest request)
    {
        var finalPath = GetFinalPathDirectory(request.Path);
        var items = new List<UpdateFileItemResponse>();
        foreach (var image in request.Items)
        {
            var item = new UpdateFileItemResponse
            {
                FileName = image.FileName,
                Path = $"{request.Path}/{image.FileName}",
                Success = true
            };
            try
            {
                var pathExists = Directory.Exists(finalPath);
                //Validamos que exista la ruta
                if (!pathExists)
                    Directory.CreateDirectory(finalPath);
                //Creamos la ruta final del archivo
                var finalPathFile = $"{finalPath}/{image.FileName}";
                await File.WriteAllBytesAsync(finalPathFile, image.File).ConfigureAwait(false);
                logger.LogInformation("Archivo {@FileName} cargado correctamente en la ruta: '{@Path}'", image.FileName, finalPath);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Error al cargar la Archivo: " +
                    "Folder: '{@Path}' - " +
                    "FileName: '{@FileName}' - " +
                    "Message: '{@Message}'",
                    request.Path, image.FileName, ex.Message);
                if (ex.InnerException is not null)
                    logger.LogError(ex.InnerException, "Inner Exception: " +
                   "Path: '{@Path}' - " +
                   "FileName: '{@FileName}' - " +
                   "Message: '{@Message}'", request.Path, image.FileName, ex.InnerException.Message);
                item.Success = false;
            }
            items.Add(item);
        }
        return new UpdateFileResponse
        {
            Items = items
        };
    }

    /// <summary>
    /// Verifica si es una ruta relativa y la convierte en absoluta
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string GetFinalPathDirectory(string path)
    {
        var finalPath = path;
        if (!string.IsNullOrEmpty(path) && (path.StartsWith('\\') || path.StartsWith('/')))
        {
            finalPath = finalPath.TrimStart('/', '\\');
            finalPath = Path.Combine(
                AppContext.BaseDirectory,
                finalPath
            );
        }
        return finalPath;
    }
}
