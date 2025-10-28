
using Common.Blob.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Common.Blob.Implementations;
/// <summary>
/// Clase abstracta de blob Bus
/// </summary>
public abstract class BlobBusBase(ILogger<BlobBusBase> logger, IConfiguration configuration) : IBlobBus
{
    protected abstract BlobImplementationNames Implementation { get; }
    protected readonly ILogger<BlobBusBase> Logger = logger;
    protected readonly IConfiguration Configuration = configuration;

    /// <summary>
    /// Permite eliminar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blobContainerName"></param>
    /// <returns></returns>
    public abstract Task DeleteFileAsync(string fileName, string blobContainerName);

    /// <summary>
    /// Permite descargar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blobContainerName"></param>
    /// <returns></returns>
    public abstract Task<BlobFile> DownloadFileAsync(string fileName, string blobContainerName);

    /// <summary>
    /// Permite subir y obtener un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blocContainerName"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    public async Task<BlobFile> UpdateAndGetFileAsync(string fileName, string blocContainerName, Stream fileStream, bool replaceIfExist)
    {
        await UpdateFileAsync(fileName, blocContainerName, fileStream, replaceIfExist).ConfigureAwait(false);
        return await DownloadFileAsync(fileName, blocContainerName).ConfigureAwait(false);
    }


    /// <summary>
    /// Permite subir un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blocContainerName"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    public abstract Task UpdateFileAsync(string fileName, string blocContainerName, Stream fileStream, bool replaceIfExist);
}