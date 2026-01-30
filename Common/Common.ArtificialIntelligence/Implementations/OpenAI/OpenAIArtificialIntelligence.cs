using System.Net.Http.Headers;
using Common.ArtificialIntelligence.CustomExceptions;
using Common.ArtificialIntelligence.Implementations.OpenAI.Model.Configuration;
using Common.ArtificialIntelligence.Implementations.OpenAI.Model.Request;
using Common.ArtificialIntelligence.Implementations.OpenAI.Model.Response;
using Common.ArtificialIntelligence.Model.Common;
using Common.ArtificialIntelligence.Model.Request;
using Common.ArtificialIntelligence.Model.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.ArtificialIntelligence.Implementations.OpenAI;

/// <summary>
/// Implementación para procesar texto en OpenAI
/// </summary>
internal class OpenAIArtificialIntelligence : ArtificialIntelligenceImplementationBase, IArtificialIntelligence
{
    /// <summary>
    /// Tipo de implementación
    /// </summary>
    protected override ArtificialIntelligenceImplementationType ImplementationType => ArtificialIntelligenceImplementationType.OpenAI;

    private readonly string _model;
    private readonly double _temperature;
    private readonly int? _maxTokens;

    /// <summary>
    /// Constructor de la implementación
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="httpClientFactory">HttpClientFactory</param>
    /// <param name="configuration">Configuración</param>
    public OpenAIArtificialIntelligence(
        ILogger<OpenAIArtificialIntelligence> logger,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration
    ) : base(logger, httpClientFactory, configuration)
    {
        var artificialIntelligenceConfiguration = GetConfiguration<OpenAIConfiguration>();
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.BaseUrl);
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.ApiKey);
        ArgumentNullException.ThrowIfNull(artificialIntelligenceConfiguration.Model);

        _model = artificialIntelligenceConfiguration.Model;
        _temperature = artificialIntelligenceConfiguration.Temperature;
        _maxTokens = artificialIntelligenceConfiguration.MaxTokens;

        HttpClient.BaseAddress = new Uri(artificialIntelligenceConfiguration.BaseUrl);
        HttpClient.DefaultRequestHeaders.Accept.Clear();
        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", artificialIntelligenceConfiguration.ApiKey);

        if (!string.IsNullOrEmpty(artificialIntelligenceConfiguration.Organization))
            HttpClient.DefaultRequestHeaders.Add("OpenAI-Organization", artificialIntelligenceConfiguration.Organization);
    }

    /// <summary>
    /// Procesa texto en OpenAI
    /// </summary>
    /// <param name="request">Request de texto a procesar</param>
    /// <returns>Response de texto procesado</returns>
    public async Task<string> ProcessTextAsync(ProcessTextRequest request)
    => await ExecuteAsync(async () =>
    {
        var responseFormat = request.ResponseType switch
        {
            ProcessResponseType.Json => "Debes responder únicamente con JSON válido.",
            ProcessResponseType.Text => "",
            _ => throw new ArtificialIntelligenceException($"Tipo de respuesta no soportada: {request.ResponseType}")
        };
        var openAIRequest = new OpenAIProcessTextRequest
        {
            Model = _model,
            Messages =
           [
               new OpenAIProcessTextRequest.Message
                {
                    Role = "system",
                    Content = $"{request.Behavior}. {responseFormat}"
                },
                new OpenAIProcessTextRequest.Message
                {
                    Role = "user",
                    Content =  new List<object>
                    {
                        new OpenAIProcessTextRequest.ContentTextInfo(request.Indications),
                    }
                }
           ],
            Temperature = _temperature,
            MaxTokens = _maxTokens,
        };
        const string endpoint = "v1/chat/completions";
        var responseContent = await SendPostModelAsync(endpoint, openAIRequest);

        // Parsear la respuesta para extraer solo el contenido del mensaje
        var openAIResponse = JsonConvert.DeserializeObject<OpenAIProcessTextResponse>(responseContent);

        var response = openAIResponse?.Choices?.FirstOrDefault()?.Message?.Content;
        if (string.IsNullOrEmpty(response))
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de OpenAI");

        return response;
    });

    /// <summary>
    /// Procesa documento en OpenAI
    /// </summary>
    /// <param name="request">Request de documento a procesar</param>
    /// <returns>Response de documento procesado</returns>
    public async Task<string> ProcessDocumentAsync(ProcessDocumentRequest request)
    => await ExecuteAsync(async () =>
    {
        var imageBase64 = Convert.ToBase64String(request.Document);
        if (string.IsNullOrEmpty(imageBase64))
            throw new ArtificialIntelligenceException("No se pudo obtener una imagen válida");
        var responseFormat = request.ResponseType switch
        {
            ProcessResponseType.Json => "Debes responder únicamente con JSON válido.",
            ProcessResponseType.Text => "",
            _ => throw new ArtificialIntelligenceException($"Tipo de respuesta no soportada: {request.ResponseType}")
        };
        var openAIRequest = new OpenAIProcessTextRequest
        {
            Model = _model,
            Messages =
            [
                new OpenAIProcessTextRequest.Message
                {
                    Role = "system",
                    Content = $"{request.Behavior}. {responseFormat}"
                },
                new OpenAIProcessTextRequest.Message
                {
                    Role = "user",
                    Content =  new List<object>
                    {
                        new OpenAIProcessTextRequest.ContentTextInfo(request.Indications),
                        new OpenAIProcessTextRequest.ContentImageInfo( $"data:image/png;base64,{imageBase64}")
                    }
                }
            ],
            Temperature = _temperature,
            MaxTokens = _maxTokens,
        };
        const string endpoint = "v1/chat/completions";
        var responseContent = await SendPostModelAsync(endpoint, openAIRequest);

        // Parsear la respuesta para extraer solo el contenido del mensaje
        var openAIResponse = JsonConvert.DeserializeObject<OpenAIProcessTextResponse>(responseContent);

        var response = openAIResponse?.Choices?.FirstOrDefault()?.Message?.Content;
        if (string.IsNullOrEmpty(response))
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de OpenAI");
        return response;
    });

    /// <summary>
    /// Procesa tool calling en OpenAI
    /// </summary>
    /// <param name="request">Request de tool calling a procesar</param>
    /// <returns>Response de tool calling procesado</returns>
    public async Task<ProcessToolCallResponse> ProcessToolCallAsync(ProcessToolCallRequest request)
    => await ExecuteAsync(async () =>
    {
        // Convertir mensajes del formato común al formato de OpenAI
        var openAIMessages = new List<OpenAIProcessTextRequest.Message>();
        
        // Agregar mensaje del sistema
        openAIMessages.Add(new OpenAIProcessTextRequest.Message
        {
            Role = "system",
            Content = request.Behavior
        });

        // Agregar mensajes de la conversación
        foreach (var msg in request.Messages)
        {
            var openAIMessage = new OpenAIProcessTextRequest.Message
            {
                Role = msg.Role,
                Content = msg.Content
            };

            // Si el mensaje tiene tool calls, agregarlos
            if (msg.ToolCalls?.Any() == true)
            {
                openAIMessage.ToolCalls = msg.ToolCalls.Select(tc => new
                {
                    id = tc.Id,
                    type = tc.Type,
                    function = new
                    {
                        name = tc.Function.Name,
                        arguments = tc.Function.Arguments
                    }
                }).ToArray();
            }

            // Si es un mensaje de tool, agregar tool_call_id y name
            if (msg.Role == "tool")
            {
                openAIMessage.ToolCallId = msg.ToolCallId;
                openAIMessage.Name = msg.Name;
            }

            openAIMessages.Add(openAIMessage);
        }

        // Convertir herramientas al formato de OpenAI
        var openAITools = request.Tools?.Select(t =>
        {
            // Si Parameters es un string JSON, deserializarlo
            object parametersObj = t.Function.Parameters;
            if (t.Function.Parameters is string parametersStr && !string.IsNullOrEmpty(parametersStr))
            {
                parametersObj = JsonConvert.DeserializeObject(parametersStr);
            }

            return new
            {
                type = t.Type,
                function = new
                {
                    name = t.Function.Name,
                    description = t.Function.Description,
                    parameters = parametersObj
                }
            };
        }).ToArray();

        var openAIRequest = new OpenAIProcessTextRequest
        {
            Model = _model,
            Messages = openAIMessages.ToArray(),
            Temperature = _temperature,
            MaxTokens = _maxTokens,
            Tools = openAITools
        };

        const string endpoint = "v1/chat/completions";
        var responseContent = await SendPostModelAsync(endpoint, openAIRequest);

        var openAIResponse = JsonConvert.DeserializeObject<OpenAIProcessTextResponse>(responseContent);
        var choice = openAIResponse?.Choices?.FirstOrDefault();
        
        if (choice == null)
            throw new ArtificialIntelligenceException("No se pudo obtener una respuesta válida de OpenAI");

        var response = new ProcessToolCallResponse
        {
            Content = choice.Message?.Content,
            FinishReason = choice.FinishReason
        };

        // Si hay tool calls, convertirlos al formato común
        if (choice.Message?.ToolCalls?.Any() == true)
        {
            response.ToolCalls = choice.Message.ToolCalls.Select(tc => new Common.ArtificialIntelligence.Model.Request.ToolCall
            {
                Id = tc.Id,
                Type = tc.Type,
                Function = new Common.ArtificialIntelligence.Model.Request.FunctionCall
                {
                    Name = tc.Function.Name,
                    Arguments = tc.Function.Arguments
                }
            }).ToList();
        }

        return response;
    });
}
