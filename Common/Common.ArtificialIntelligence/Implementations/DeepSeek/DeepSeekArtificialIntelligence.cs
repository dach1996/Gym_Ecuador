using System.Net.Http.Headers;
using Common.ArtificialIntelligence.CustomExceptions;
using Common.ArtificialIntelligence.Implementations.DeepSeek.Model.Configuration;
using Common.ArtificialIntelligence.Implementations.DeepSeek.Model.Request;
using Common.ArtificialIntelligence.Implementations.DeepSeek.Model.Response;
using Common.ArtificialIntelligence.Model.Common;
using Common.ArtificialIntelligence.Model.Request;
using Common.ArtificialIntelligence.Model.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.ArtificialIntelligence.Implementations.DeepSeek;

/// <summary>
/// Implementación para procesar texto en DeepSeek
/// </summary>
internal class DeepSeekArtificialIntelligence : ArtificialIntelligenceImplementationBase, IArtificialIntelligence
{
    /// <summary>
    /// Tipo de implementación
    /// </summary>
    protected override ArtificialIntelligenceImplementationType ImplementationType => ArtificialIntelligenceImplementationType.DeepSeek;

    private readonly string _model;

    /// <summary>
    /// Constructor de la implementación
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="httpClientFactory">HttpClientFactory</param>
    /// <param name="configuration">Configuración</param>
    public DeepSeekArtificialIntelligence(
        ILogger<DeepSeekArtificialIntelligence> logger,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration
    ) : base(logger, httpClientFactory, configuration)
    {
        var artificialIntelligenceConfiguration = GetConfiguration<DeepSeekConfiguration>();
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.BaseUrl);
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.ApiKey);
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.Model);

        _model = artificialIntelligenceConfiguration.Model;

        HttpClient.BaseAddress = new Uri(artificialIntelligenceConfiguration.BaseUrl);
        HttpClient.DefaultRequestHeaders.Accept.Clear();
        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", artificialIntelligenceConfiguration.ApiKey);
    }

    /// <summary>
    /// Procesa texto en DeepSeek
    /// </summary>
    /// <param name="request">Request de texto a procesar</param>
    /// <returns>Response de texto procesado</returns>
    public async Task<string> ProcessTextAsync(ProcessTextRequest request)
    {
        var deepSeekRequest = new DeepSeekProcessTextRequest
        {
            Model = _model,
            Messages =
           [
           new() {
                Role = "system",
                Content = $"{request.Behavior}. {request.Indications}"
            },
            ],
            Stream = false
        };

        // Configurar formato de respuesta según el tipo solicitado
        switch (request.ResponseType)
        {
            case ProcessResponseType.Json:
                deepSeekRequest.Messages.Add(new()
                {
                    Role = "system",
                    Content = "Responde únicamente con JSON válido."
                });
                break;
            case ProcessResponseType.Text:
                deepSeekRequest.Messages.Add(new()
                {
                    Role = "system",
                    Content = "Responde únicamente con texto válido."
                });
                break;
            default:
                throw new ArtificialIntelligenceException($"Tipo de respuesta no soportada: {request.ResponseType}");
        }

        const string endpoint = "chat/completions";
        var responseContent = await SendPostModelAsync(endpoint, deepSeekRequest);
        // Parsear la respuesta para extraer solo el contenido del mensaje
        var deepSeekResponse = JsonConvert.DeserializeObject<DeepSeekProcessTextResponse>(responseContent);
        var response = deepSeekResponse?.Choices?.FirstOrDefault()?.Message?.Content;
        if (string.IsNullOrEmpty(response))
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de DeepSeek");
        return response;
    }

    /// <summary>
    /// Procesa un documento (imagen) usando DeepSeek
    /// </summary>
    /// <param name="request">Request de documento a procesar</param>
    /// <returns>Respuesta procesada</returns>
    public async Task<string> ProcessDocumentAsync(ProcessDocumentRequest request)
    => await ExecuteAsync(async () =>
    {
        var imageBase64 = Convert.ToBase64String(request.Document);
        if (string.IsNullOrEmpty(imageBase64))
            throw new ArtificialIntelligenceException("No se pudo obtener una imagen válida");

        var deepSeekRequest = new DeepSeekProcessTextRequest
        {
            Model = _model,
            Messages =
            [
            new() {
                Role = "system",
                Content = request.Behavior
            },
            ],
            Stream = false
        };

        // Configurar formato de respuesta según el tipo solicitado
        switch (request.ResponseType)
        {
            case ProcessResponseType.Json:
                deepSeekRequest.Messages.Add(new()
                {
                    Role = "system",
                    Content = "Responde únicamente con JSON válido."
                });
                break;
            case ProcessResponseType.Text:
                deepSeekRequest.Messages.Add(new()
                {
                    Role = "system",
                    Content = "Responde únicamente con texto válido."
                });
                break;
            default:
                throw new ArtificialIntelligenceException($"Tipo de respuesta no soportada: {request.ResponseType}");
        }

        const string endpoint = "chat/completions";
        var responseContent = await SendPostModelAsync(endpoint, deepSeekRequest);

        // Parsear la respuesta para extraer solo el contenido del mensaje
        var deepSeekResponse = JsonConvert.DeserializeObject<DeepSeekProcessTextResponse>(responseContent);
        var response = deepSeekResponse?.Choices?.FirstOrDefault()?.Message?.Content;
        if (string.IsNullOrEmpty(response))
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de DeepSeek");
        return response;
    });

    /// <summary>
    /// Procesa tool calling en DeepSeek (implementación básica)
    /// </summary>
    /// <param name="request">Request de tool calling a procesar</param>
    /// <returns>Response de tool calling procesado</returns>
    public async Task<ProcessToolCallResponse> ProcessToolCallAsync(ProcessToolCallRequest request)
    => await ExecuteAsync(async () =>
    {
        // DeepSeek soporta tool calling pero por ahora implementación básica
        // sin function calling para mantener compatibilidad

        var messages = new List<DeepSeekProcessTextRequest.Message>();

        // Agregar mensaje del sistema
        messages.Add(new()
        {
            Role = "system",
            Content = request.Behavior
        });

        // Agregar mensajes del historial
        foreach (var msg in request.Messages.Where(where => !string.IsNullOrEmpty(where.Content)))
        {
            messages.Add(new()
            {
                Role = msg.Role,
                Content = msg.Content
            });
        }

        var deepSeekRequest = new DeepSeekProcessTextRequest
        {
            Model = _model,
            Messages = messages,
            Stream = false
        };

        const string endpoint = "chat/completions";
        var responseContent = await SendPostModelAsync(endpoint, deepSeekRequest);
        var deepSeekResponse = JsonConvert.DeserializeObject<DeepSeekProcessTextResponse>(responseContent);

        var choice = deepSeekResponse?.Choices?.FirstOrDefault();
        if (choice == null)
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de DeepSeek");

        return new ProcessToolCallResponse
        {
            Content = choice.Message?.Content,
            FinishReason = choice.FinishReason ?? "stop",
            ToolCalls = null // DeepSeek requiere implementación adicional para function calling
        };
    });
}
