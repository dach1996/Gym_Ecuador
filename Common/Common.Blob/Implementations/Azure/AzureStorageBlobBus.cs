using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Common.Blob.BlobException;
using Common.Blob.Models;
using Common.Blob.Models.Configuration;
using Common.Blob.Models.Configuration.Azure;
using Common.Blob.Models.Request;
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
    public override async Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request)
    {
        var results = new List<DeleteFileItemResponse>();
        foreach (var item in request.Items)
        {
            var pathSplits = item.FullPathName.Split('/');
            if (pathSplits.Length < 2)
                throw new CustomBlobException($"El path del archivo no es válido: {item.FullPathName}");
            var containerName = pathSplits[1];
            var filePath = string.Join('/', pathSplits.Skip(2));
            try
            {
                //create object of BlocContainerClient to verify if exist
                BlobContainerClient blobContainer = new(AzureBlobConfiguration.ConnectionString, containerName);
                await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
                // Create the Blob container name and specifict file name
                BlobClient blobClient = blobContainer.GetBlobClient(filePath);
                var result = await blobClient.DeleteIfExistsAsync().ConfigureAwait(false);
                Logger.LogInformation("Archivo {@FileName} Eliminado del Contenedor: '{@Container}': {@State}", item.FullPathName, containerName, result);
                results.Add(new DeleteFileItemResponse
                {
                    Identifier = item.Identifier,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al Eliminar la Archivo: " +
                  "Path: '{@Path}' - " +
                  "FileName: '{@FileName}' - " +
                  "Message: '{@Message}'", containerName, item.FullPathName, ex.Message);
                if (ex.InnerException is not null)
                    Logger.LogError(ex.InnerException, "Inner Exception: " +
                   "Path: '{@Path}' - " +
                   "FileName: '{@FileName}' - " +
                   "Message: '{@Message}'", containerName, item.FullPathName, ex.InnerException.Message);
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
        var items = new List<UpdateFileItemResponse>();
        var pathSplits = request.Path.Split('/');
        var containerName = request.Path.StartsWith('/')
         ? pathSplits[1] : pathSplits[0];
        int index = request.Path.IndexOf(containerName) + containerName.Length;
        var fileDirectoryPath = request.Path[index..].TrimStart('/');
        foreach (var item in request.Items)
        {
            try
            {
                containerName = containerName.ToLower();
                //create object of BlocContainerClient to verify if exist
                BlobContainerClient blobContainer = new(AzureBlobConfiguration.ConnectionString, containerName);
                await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
                var filePath = $"/{fileDirectoryPath}/{item.FileName}";
                // Create the Blob container name and specifict file name
                BlobClient blobClient = blobContainer.GetBlobClient(filePath);
                // Upload the file
                using var stream = new MemoryStream(item.File);
                await blobClient.UploadAsync(stream, new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(item.FileName),
                        ContentDisposition = "inline"
                    }
                }).ConfigureAwait(false);
                Logger.LogInformation("Archivo {@FileName} cargado correctamente en el contenedor: '{@Container}' - Remplazar si Existe: {@Replace}", item.FileName, request.Path, item.ReplaceIfExist);
                items.Add(new UpdateFileItemResponse
                {
                    FileName = item.FileName,
                    Path = $"/{containerName}{filePath}",
                    Success = true
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al cargar la Archivo: " +
                    "Path: '{@Path}' - " +
                    "FileName: '{@FileName}' - " +
                    "Message: '{@Message}'", request.Path, item.FileName, ex.Message);
                if (ex.InnerException is not null)
                    Logger.LogError(ex.InnerException, "Inner Exception: " +
                   "Path: '{@Path}' - " +
                   "FileName: '{@FileName}' - " +
                   "Message: '{@Message}'", request.Path, item.FileName, ex.InnerException.Message);
                items.Add(new UpdateFileItemResponse
                {
                    FileName = item.FileName,
                    Path = $"{request.Path}/{item.FileName}",
                    Success = false
                });
            }
        }
        return new UpdateFileResponse
        {
            Items = items
        };
    }

    // Función auxiliar
    private static string GetContentType(string fileName) => Path.GetExtension(fileName).ToLower() switch
    {
        ".png" => "image/png",
        ".jpg" or ".jpeg" => "image/jpeg",
        ".gif" => "image/gif",
        ".bmp" => "image/bmp",
        ".svg" => "image/svg+xml",
        _ => "application/octet-stream"
    };
}
