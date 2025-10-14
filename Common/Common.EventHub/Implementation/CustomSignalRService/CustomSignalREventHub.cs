using System.Diagnostics;
using System.Net.Http.Json;
using Common.EventHub.EventHubException;
using Common.EventHub.Models.Configuration;
using Common.EventHub.Models.Configuration.CustomEventHubService;
using Common.EventHub.Models.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.EventHub.Implementation.CustomSignalRService;
/// <summary>
/// Implementaciòn para WebServicesEc
/// </summary>
public class CustomSignalREventHub : EventHubBase, IEventHub
{
    protected override EventHubImplementationName ImplementationName => EventHubImplementationName.CustomSignalR;
    protected readonly CustomSignalRConfiguration ConfigurationImplementation;
    protected readonly HttpClient HttpClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <returns></returns>
    public CustomSignalREventHub(
        ILogger<CustomSignalREventHub> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory
        ) : base(logger)
    {
        ConfigurationImplementation = configuration.GetSection(nameof(EventHubConfiguration)).Get<EventHubConfiguration<CustomSignalRConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
            ?? throw new CustomEventHubException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(ConfigurationImplementation)}");
        HttpClient = httpClientFactory.CreateClient($"{ImplementationName}");
        HttpClient.BaseAddress = new Uri(ConfigurationImplementation.BaseUrl);
        HttpClient.Timeout = TimeSpan.FromSeconds(ConfigurationImplementation.Timeout);
    }

    /// <summary>
    /// Envía notificación de Evento
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task SendMessageAsync(SendEventMessageByGroupRequest request)
    {
        const string path = "api/v1/EventNotify/Group";
        Logger.LogInformation("Request WebSocket - {@EventName} NotifyEvent: {@RequestModel}", $"{HttpClient.BaseAddress}/{path}", request);
        var stopwatch = Stopwatch.StartNew();
        var response = await HttpClient.PostAsJsonAsync(path, request).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new CustomEventHubException($"Error en respuesta de Petición");
        Logger.LogInformation("Respuesta  WebSocket - Servicio {@EventName} en '{@DurationTime}ms'", $"{HttpClient.BaseAddress}/{path}", stopwatch.ElapsedMilliseconds);
    }


}