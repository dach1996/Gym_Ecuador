using System.Net.Http.Headers;
using Common.ArtificialIntelligence.CustomExceptions;
using Common.ArtificialIntelligence.Implementations.Gemini.Model.Configuration;
using Common.ArtificialIntelligence.Implementations.Gemini.Model.Request;
using Common.ArtificialIntelligence.Implementations.Gemini.Model.Response;
using Common.ArtificialIntelligence.Model.Common;
using Common.ArtificialIntelligence.Model.Request;
using Common.ArtificialIntelligence.Model.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Common.ArtificialIntelligence.Implementations.Gemini.Model.Request.GeminiProcessTextRequest;
namespace Common.ArtificialIntelligence.Implementations.Gemini;

/// <summary>
/// Implementacion para procesar texto en Gemini
/// </summary>
internal class GeminiArtificialIntelligence : ArtificialIntelligenceImplementationBase, IArtificialIntelligence
{
    /// <summary>
    /// Tipo de implementacion
    /// </summary>
    protected override ArtificialIntelligenceImplementationType ImplementationType => ArtificialIntelligenceImplementationType.Gemini;

    private readonly string _key;
    private readonly string _model;

    /// <summary>
    /// Constructor de la implementacion
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpClientFactory"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public GeminiArtificialIntelligence(
        ILogger<GeminiArtificialIntelligence> logger,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration
    ) : base(
        logger,
        httpClientFactory,
        configuration)
    {
        var artificialIntelligenceConfiguration = GetConfiguration<GeminiConfiguration>();
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.BaseUrl);
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.ApiKey);
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.Model);
        _key = artificialIntelligenceConfiguration.ApiKey;
        _model = artificialIntelligenceConfiguration.Model;
        HttpClient.BaseAddress = new Uri(artificialIntelligenceConfiguration.BaseUrl);
        HttpClient.DefaultRequestHeaders.Accept.Clear();
        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <summary>
    /// Procesa texto en Gemini
    /// </summary>
    /// <param name="request">Request de texto a procesar</param>
    /// <returns>Response de texto a procesar</returns>
    public async Task<string> ProcessTextAsync(ProcessTextRequest request)
    => await ExecuteAsync(async () =>
    {
        var geminiRequest = new GeminiProcessTextRequest
        {
            Contents = [new Content { Parts = [
                new () { Text = $"{request.Behavior}. {request.Indications}" },
            ] }],
            GenerationConfiguration = request.ResponseType switch
            {
                ProcessResponseType.Json => new GenerationConfig { ResponseMimeType = "application/json" },
                ProcessResponseType.Text => new GenerationConfig { ResponseMimeType = "text/plain" },
                _ => throw new ArtificialIntelligenceException($"Tipo de respuesta no soportada: {request.ResponseType}")
            }
        };
        var fullpath = $"models/{_model}:generateContent?key={_key}";
        var responseContent = await SendPostModelAsync(fullpath, geminiRequest);
        var geminiResponse = JsonConvert.DeserializeObject<GeminiProcessTextResponse>(responseContent);
        var response = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
        if (string.IsNullOrEmpty(response))
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de Gemini");
        return response;
    });

    /// <summary>
    /// Procesa documento en Gemini
    /// </summary>
    /// <param name="request">Request de documento a procesar</param>
    /// <returns>Response de documento procesado</returns>
    public async Task<string> ProcessDocumentAsync(ProcessDocumentRequest request)
    => await ExecuteAsync(async () =>
    {
        var imageBase64 = Convert.ToBase64String(request.Document);
        if (string.IsNullOrEmpty(imageBase64))
            throw new ArtificialIntelligenceException("No se pudo obtener una imagen válida");
        var geminiRequest = new GeminiProcessTextRequest
        {
            Contents = [new Content { Parts = [
                new () { Text = $"{request.Behavior}. {request.Indications}" },
                new () { InlineData = new InlineData { MimeType = "image/jpeg", Data = imageBase64 } }
            ] }],
            GenerationConfiguration = request.ResponseType switch
            {
                ProcessResponseType.Json => new GenerationConfig { ResponseMimeType = "application/json" },
                ProcessResponseType.Text => new GenerationConfig { ResponseMimeType = "text/plain" },
                _ => throw new ArtificialIntelligenceException($"Tipo de respuesta no soportada: {request.ResponseType}")
            }
        };
        var fullpath = $"models/{_model}:generateContent?key={_key}";
        var responseContent = await SendPostModelAsync(fullpath, geminiRequest);
        var geminiResponse = JsonConvert.DeserializeObject<GeminiProcessTextResponse>(responseContent);
        var response = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
        if (string.IsNullOrEmpty(response))
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de Gemini");
        return response;
    });

    /// <summary>
    /// Procesa tool calling en Gemini (implementación básica)
    /// </summary>
    /// <param name="request">Request de tool calling a procesar</param>
    /// <returns>Response de tool calling procesado</returns>
    public async Task<ProcessToolCallResponse> ProcessToolCallAsync(ProcessToolCallRequest request)
    => await ExecuteAsync(async () =>
    {
        // Gemini soporta function calling pero con un formato diferente
        // Por ahora, implementación básica sin tool calls para mantener compatibilidad
        
        var parts = new List<Part>();
        
        // Agregar comportamiento del sistema
        parts.Add(new Part { Text = request.Behavior });

        // Agregar mensajes
        foreach (var msg in request.Messages)
        {
            if (!string.IsNullOrEmpty(msg.Content))
            {
                parts.Add(new Part { Text = msg.Content });
            }
        }

        var geminiRequest = new GeminiProcessTextRequest
        {
            Contents = [new Content { Parts = parts.ToArray() }],
            GenerationConfiguration = new GenerationConfig { ResponseMimeType = "text/plain" }
        };

        var fullpath = $"models/{_model}:generateContent?key={_key}";
        var responseContent = await SendPostModelAsync(fullpath, geminiRequest);
        var geminiResponse = JsonConvert.DeserializeObject<GeminiProcessTextResponse>(responseContent);
        
        var candidate = geminiResponse?.Candidates?.FirstOrDefault();
        if (candidate == null)
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de Gemini");

        return new ProcessToolCallResponse
        {
            Content = candidate.Content?.Parts?.FirstOrDefault()?.Text,
            FinishReason = candidate.FinishReason ?? "STOP",
            ToolCalls = null // Gemini requiere implementación adicional para function calling
        };
    });
}
