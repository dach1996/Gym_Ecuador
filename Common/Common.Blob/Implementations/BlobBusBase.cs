
using Common.Blob.Models;
using Common.Blob.Models.Response;
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
    /// <param name="path"></param>
    /// <returns></returns>
    public abstract Task DeleteFileAsync(string fileName, string path);

    /// <summary>
    /// Permite descargar un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public abstract Task<BlobFile> DownloadFileAsync(string fileName, string path);

    /// <summary>
    /// Permite subir y obtener un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    public async Task<BlobFile> UpdateAndGetFileAsync(string fileName, string path, Stream fileStream, bool replaceIfExist)
    {
        await UpdateFileAsync(fileName, path, fileStream, replaceIfExist).ConfigureAwait(false);
        return await DownloadFileAsync(fileName, path).ConfigureAwait(false);
    }


    /// <summary>
    /// Permite subir un archivo
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    public abstract Task<UpdateFileResponse> UpdateFileAsync(string fileName, string path, Stream fileStream, bool replaceIfExist);
}