using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Common.Blob.BlobException;
using Common.Blob.Models;
using Common.Blob.Models.Configuration;
using Common.Blob.Models.Configuration.Azure;
using Common.Blob.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Common.Blob.Implementations.Azure;
/// <summary>
/// Constructor
/// </summary>
/// <returns></returns>
public class AzureStorageBlobBus : BlobBusBase
{
    protected override BlobImplementationNames Implementation => BlobImplementationNames.AzureStorage;
    protected readonly AzureBlobConfiguration AzureBlobConfiguration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public AzureStorageBlobBus(
    ILogger<AzureStorageBlobBus> logger,
    IConfiguration configuration) : base(logger, configuration)
    {
        var configurations = configuration.GetSection(nameof(BlobConfiguration)).Get<BlobConfiguration<AzureBlobConfiguration>>()
         ?? throw new CustomBlobException($"No se encontró ninguna configuración de Blob");
        var currentConfiguration = configurations
         ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{Implementation}")
          ?? throw new CustomBlobException($"No se encontró la configuración de Blob con identificador: {Implementation}");
        AzureBlobConfiguration = currentConfiguration.Information
         ?? throw new CustomBlobException($"No se encontró información de la configuración de Blob con identificador: {Implementation}");
    }


    /// <summary>
    /// Descargar Imagen
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blobContainerName"></param>
    /// <returns></returns>
    /// <exception cref="CustomBlobException"></exception>
    public override async Task<BlobFile> DownloadFileAsync(string fileName, string path)
    {
        try
        {
            //create object of BlocContainerClient to verify if exist
            BlobContainerClient blobContainer = new(AzureBlobConfiguration.ConnectionString, path);
            await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            // Create the Blob container name and specifict file name
            BlobClient blobClient = new(AzureBlobConfiguration.ConnectionString, path, fileName);
            BlobDownloadStreamingResult blob = await blobClient.DownloadStreamingAsync().ConfigureAwait(false);

            if (blob?.Content is null)
                throw new CustomBlobException($"El contenido del archivo está vacío.");

            using var ms = new MemoryStream();
            await blob.Content.CopyToAsync(ms);
            Logger.LogDebug("Archivo {@FileName} descargado correctamente del contenedor: '{@Container}' - Tamaño: {@Size}", fileName, path, blob.Details.ContentLength);
            var file = new BlobFile
            {
                FileName = fileName,
                Content = ms.ToArray(),
                Length = blob.Details.ContentLength,
                ContentType = blob.Details.ContentType,
                LastModified = blob.Details.LastModified,
            };
            return file;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al descargar la Archivo: " +
               "Path: '{@Path}' - " +
               "FileName: '{@FileName}' - " +
               "Message: '{@Message}'", path, fileName, ex.Message);
            if (ex.InnerException is not null)
                Logger.LogError(ex.InnerException, "Inner Exception: " +
               "Path: '{@Path}' - " +
               "FileName: '{@FileName}' - " +
               "Message: '{@Message}'", path, fileName, ex.InnerException.Message);
            throw new CustomBlobException(ex.Message);
        }
    }

    /// <summary>
    /// Descargar Imagen
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="CustomBlobException"></exception>
    public override async Task DeleteFileAsync(string fileName, string path)
    {
        try
        {
            //create object of BlocContainerClient to verify if exist
            BlobContainerClient blobContainer = new(AzureBlobConfiguration.ConnectionString, path);
            await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            // Create the Blob container name and specifict file name
            BlobClient blobClient = new(AzureBlobConfiguration.ConnectionString, path, fileName);
            var result = await blobClient.DeleteIfExistsAsync().ConfigureAwait(false);
            Logger.LogInformation("Archivo {@FileName} Eliminado del Contenedor: '{@Container}': {@State}", fileName, path, result);

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al Eliminar la Archivo: " +
              "Path: '{@Path}' - " +
              "FileName: '{@FileName}' - " +
              "Message: '{@Message}'", path, fileName, ex.Message);
            if (ex.InnerException is not null)
                Logger.LogError(ex.InnerException, "Inner Exception: " +
               "Path: '{@Path}' - " +
               "FileName: '{@FileName}' - " +
               "Message: '{@Message}'", path, fileName, ex.InnerException.Message);
            throw new CustomBlobException(ex.Message);
        }
    }

    /// <summary>
    /// Cargar Imagen
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="path"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    /// <exception cref="CustomBlobException"></exception>
    public override async Task<UpdateFileResponse> UpdateFileAsync(string fileName, string path, Stream fileStream, bool replaceIfExist)
    {
        try
        {
            //create object of BlocContainerClient to verify if exist
            BlobContainerClient blobContainer = new(AzureBlobConfiguration.ConnectionString, path);
            await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            // Create the Blob container name and specifict file name
            BlobClient blobClient = new(AzureBlobConfiguration.ConnectionString, path, fileName);
            // Upload the file
            await blobClient.UploadAsync(fileStream, replaceIfExist).ConfigureAwait(false);
            Logger.LogInformation("Archivo {@FileName} cargado correctamente en el contenedor: '{@Container}' - Remplazar si Existe: {@Replace}", fileName, path, replaceIfExist);
            return new UpdateFileResponse
            {
                FileName = fileName,
                Path = blobClient.Uri.AbsolutePath
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al cargar la Archivo: " +
                "Path: '{@Path}' - " +
                "FileName: '{@FileName}' - " +
                "Message: '{@Message}'", path, fileName, ex.Message);
            if (ex.InnerException is not null)
                Logger.LogError(ex.InnerException, "Inner Exception: " +
               "Path: '{@Path}' - " +
               "FileName: '{@FileName}' - " +
               "Message: '{@Message}'", path, fileName, ex.InnerException.Message);
            throw new CustomBlobException(ex.Message);
        }
        finally
        {
            await fileStream.DisposeAsync();
        }
    }
}
