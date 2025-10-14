using Azure.Storage.Queues;
using Common.Queue.Interface;
using Common.Queue.Model.Configuration;
using Common.Queue.Model.Configuration.Azure;
using Common.Queue.Model.Enum;
using Common.Queue.Model.Request;
using Common.Queue.Model.Response;
using Common.Queue.QueueException;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Common.Queue.Implementation.Azure;
/// <summary>
/// Implementación de Queue con Azure
/// </summary>
public class AzureQueue : QueueBase
{
    protected override QueueImplementationName QueueImplementationName => QueueImplementationName.Azure;
    protected readonly AzureQueueConfiguration AzureQueueConfiguration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public AzureQueue(
        ILogger<AzureQueue> logger,
        IConfiguration configuration) : base(logger, configuration)
    {
        AzureQueueConfiguration = configuration.GetSection(nameof(QueueConfiguration)).Get<QueueConfiguration<AzureQueueConfiguration>>()
         ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{QueueImplementationName}")?.Information
          ?? throw new CustomQueueException($"No se encontró la configuración de {nameof(IQueue)} con identificador: {QueueImplementationName}");
    }

    /// <summary>
    /// Añade un mensaje a la cola
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async override Task<SendQueueMessageResponse> SendMessageAsync(SendQueueMessageRequest request)
    {
        try
        {
            // Instantiate a QueueClient which will be used to manipulate the queue
            var queueClient = new QueueClient(AzureQueueConfiguration.ConnectionString, request.QueueName);

            // Create the queue if it doesn't already exist
            var queueVerify = await queueClient.CreateIfNotExistsAsync();
            if (queueVerify is not null)
                Logger.LogInformation("Nuevo Queue creado: {@QueueName}", request.QueueName);
            // Async enqueue the message
            var jsonStringMessageBytes = System.Text.Encoding.UTF8.GetBytes(request.Message);
            var messageBase64String = Convert.ToBase64String(jsonStringMessageBytes);
            var response = await queueClient.SendMessageAsync(messageBase64String, TimeSpan.FromSeconds(request.DelaySeconds));
            Logger.LogInformation("Queue: {@QueueName} registrado con Id: {@MessageId} Ejecutar: {@TimeExecution} UTC", request.QueueName, response.Value.MessageId, response.Value.TimeNextVisible);
            Logger.LogDebug("Mensaje del Queue {@MessageId}: {@Message}", response.Value.MessageId, request.Message);
            return new SendQueueMessageResponse(response.Value.MessageId, response.Value.PopReceipt);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error en el envío de Queue: {@QueueName} - Mensaje: {@Message}", request.QueueName, ex.Message);
            return new SendQueueMessageResponse(ex.Message);
        }
    }

    /// <summary>
    /// Elimina un mensaje de la cola
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async override Task<DeleteQueueMessageResponse> DeleteMessageAsync(DeleteQueueMessageRequest request)
    {
        var countSuccess = 0;
        foreach (var deleteQueueMessageGroup in request.DeleteQueueMessageItems.GroupBy(groupBy => groupBy.QueueName))
        {
            try
            {
                // Instantiate a QueueClient which will be used to manipulate the queue
                var queueClient = new QueueClient(AzureQueueConfiguration.ConnectionString, deleteQueueMessageGroup.Key);
                // Create the queue if it doesn't already exist
                var queueVerify = await queueClient.CreateIfNotExistsAsync();
                if (queueVerify is not null)
                    throw new CustomQueueException($"No se encontró el Queue con nombre: {deleteQueueMessageGroup.Key}");
                // Async enqueue the message
                foreach (var deleteQueueMessageItem in deleteQueueMessageGroup.ToList())
                {
                    _ = await queueClient.DeleteMessageAsync(deleteQueueMessageItem.MessageId, deleteQueueMessageItem.PopReceipt);
                    Logger.LogInformation("Se eliminó el mensaje {@MessageId} del Queue: {@QueueName}", deleteQueueMessageItem.MessageId, deleteQueueMessageItem.QueueName);
                    countSuccess++;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error eliminado de Queue: {@QueueName} - Mensaje: {@Message}", deleteQueueMessageGroup.Key, ex.Message);
            }
        }
        return DeleteQueueMessageResponse.Sucess(countSuccess, request.DeleteQueueMessageItems.Count());
    }
}