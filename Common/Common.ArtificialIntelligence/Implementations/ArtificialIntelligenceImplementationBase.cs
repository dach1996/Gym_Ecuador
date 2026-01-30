using System.Text;
using Common.ArtificialIntelligence.CustomExceptions;
using Common.ArtificialIntelligence.Model.Configuration;
using Common.ArtificialIntelligence.JsonIgnore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.ArtificialIntelligence.Implementations;

internal abstract class ArtificialIntelligenceImplementationBase
{
    protected readonly ILogger<ArtificialIntelligenceImplementationBase> Logger;
    protected readonly IConfiguration Configuration;
    protected readonly HttpClient HttpClient;

    /// <summary>
    /// Constructor de la implementacions
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="httpClientFactory">HttpClientFactory</param>
    /// <param name="configuration">Configuracion</param>
    protected ArtificialIntelligenceImplementationBase(
        ILogger<ArtificialIntelligenceImplementationBase> logger,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        Logger = logger;
        Configuration = configuration;
        HttpClient = httpClientFactory.CreateClient($"{ImplementationType}");
    }

    protected abstract ArtificialIntelligenceImplementationType ImplementationType { get; }

    /// <summary>
    /// Obtiene la configuracion de la implementacion
    /// </summary>
    /// <typeparam name="T">Tipo de configuracion</typeparam>
    /// <returns>Configuracion de la implementacion</returns>
    protected T GetConfiguration<T>() where T : ArtificialIntelligenceImplementation
    {
        var nationalCarrierConfiguration = Configuration.GetSection(nameof(ArtificialIntelligenceConfiguration))
            .Get<ArtificialIntelligenceConfiguration<T>>()
            ?? throw new ArtificialIntelligenceException("No se encontro las configuraciones de Artificial Intelligence");
        if (!nationalCarrierConfiguration.Implementations.TryGetValue($"{ImplementationType}", out var configurationModel))
            throw new ArtificialIntelligenceException($"No se encontro la configuracion de : {ImplementationType} ");
        Logger.LogInformation("Configuracion de {@ImplementationType}: {@Configuration}", ImplementationType, configurationModel);
        return configurationModel;
    }

    /// <summary>
    /// Envia una solicitud a la API
    /// </summary>
    /// <typeparam name="TRequest">Tipo de request</typeparam>
    /// <param name="fullpath">Path a la API</param>
    /// <param name="processRequestModel">Request a enviar</param>
    /// <returns>Response de la API</returns>
    protected async Task<string> SendPostModelAsync<TRequest>(string fullpath, TRequest processRequestModel) where TRequest : class
    {
        Logger.LogInformation("Request {@ImplementationType} BodySensitive {@Request}", $"{ImplementationType}", processRequestModel.ToJsonSensitve());
        var requestContent = JsonConvert.SerializeObject(processRequestModel);
        var response = await HttpClient.PostAsync(fullpath, new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var responseContent = await response.Content.ReadAsStringAsync();
        Logger.LogInformation("Response {@ImplementationType} BodySensitive {@Response}", $"{ImplementationType}", responseContent);
        if (!response.IsSuccessStatusCode)
            throw new ArtificialIntelligenceException($"Error en respuesta del servicio: {responseContent}");
        return responseContent;
    }
    /// <summary>
    /// Ejecuta una solicitud a la API
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns>Response</returns>
    protected async Task<T> ExecuteAsync<T>(Func<Task<T>> process)
    {
        try
        {
            return await process();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al ejecutar la solicitud a la API: {@ImplementationType}", $"{ImplementationType}");
            throw new ArtificialIntelligenceException(ex.Message, ex);
        }
    }
}