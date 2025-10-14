using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Common.Blob.BlobException;
using Common.Blob.Models;
using Common.Blob.Models.Configuration;
using Common.Blob.Models.Configuration.Azure;
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
        AzureBlobConfiguration = configuration.GetSection(nameof(BlobConfiguration)).Get<BlobConfiguration<AzureBlobConfiguration>>()
         ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{Implementation}")?.Information
          ?? throw new CustomBlobException($"No se encontró la configuración de Blob con identificador: {Implementation}");
    }


    /// <summary>
    /// Descargar Imagen
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blobContainerName"></param>
    /// <returns></returns>
    /// <exception cref="CustomBlobException"></exception>
    public override async Task<BlobFile> DownloadFileAsync(string fileName, string blobContainerName)
    {
        try
        {
            //create object of BlocContainerClient to verify if exist
            BlobContainerClient blobContainer = new(AzureBlobConfiguration.ConnectionString, blobContainerName);
            await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            // Create the Blob container name and specifict file name
            BlobClient blobClient = new(AzureBlobConfiguration.ConnectionString, blobContainerName, fileName);
            BlobDownloadStreamingResult blob = await blobClient.DownloadStreamingAsync().ConfigureAwait(false);

            if (blob?.Content is null)
                throw new CustomBlobException($"El contenido del archivo está vacío.");

            using var ms = new MemoryStream();
            await blob.Content.CopyToAsync(ms);
            var file = new BlobFile
            {
                FileName = fileName,
                Content = ms.ToArray(),
                Length = blob.Details.ContentLength,
                ContentType = blob.Details.ContentType,
                LastModified = blob.Details.LastModified,
                Url = blobClient.Uri.AbsoluteUri
            };
            return file;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al descargar la Archivo: " +
               "BlobContainerName: '{@BlobContainerName}' - " +
               "FileName: '{@FileName}' - " +
               "Message: '{@Message}'", blobContainerName, fileName, ex.Message);
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
    public override async Task DeleteFileAsync(string fileName, string blobContainerName)
    {
        try
        {
            //create object of BlocContainerClient to verify if exist
            BlobContainerClient blobContainer = new(AzureBlobConfiguration.ConnectionString, blobContainerName);
            await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            // Create the Blob container name and specifict file name
            BlobClient blobClient = new(AzureBlobConfiguration.ConnectionString, blobContainerName, fileName);
            var result = await blobClient.DeleteIfExistsAsync().ConfigureAwait(false);
            Logger.LogDebug("Archivo {@FileName} Eliminado del Contenedor: '{@Container}': {@State}", fileName, blobContainerName, result);

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al Eliminar la Archivo: " +
              "BlobContainerName: '{@BlobContainerName}' - " +
              "FileName: '{@FileName}' - " +
              "Message: '{@Message}'", blobContainerName, fileName, ex.Message);
            throw new CustomBlobException(ex.Message);
        }
    }

    /// <summary>
    /// Cargar Imagen
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="blocContainerName"></param>
    /// <param name="fileStream"></param>
    /// <param name="replaceIfExist"></param>
    /// <returns></returns>
    /// <exception cref="CustomBlobException"></exception>
    public override async Task UpdateFileAsync(string fileName, string blocContainerName, Stream fileStream, bool replaceIfExist)
    {
        try
        {
            //create object of BlocContainerClient to verify if exist
            BlobContainerClient blobContainer = new(AzureBlobConfiguration.ConnectionString, blocContainerName);
            await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            // Create the Blob container name and specifict file name
            BlobClient blobClient = new(AzureBlobConfiguration.ConnectionString, blocContainerName, fileName);
            // Upload the file
            await blobClient.UploadAsync(fileStream, replaceIfExist).ConfigureAwait(false);
            Logger.LogDebug("Archivo {@FileName} cargado correctamente en el contenedor: '{@Container}' - Remplazar si Existe: {@Replace}", fileName, blocContainerName, replaceIfExist);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al cargar la Archivo: " +
                "BlobContainerName: '{@BlobContainerName}' - " +
                "FileName: '{@FileName}' - " +
                "Message: '{@Message}'", blocContainerName, fileName, ex.Message);
            throw new CustomBlobException(ex.Message);
        }
        finally
        {
            await fileStream.DisposeAsync();
        }
    }
}
