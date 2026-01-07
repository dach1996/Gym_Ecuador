using System.Diagnostics;
using Common.UserDocumentation.DocumentationException;
using Common.UserDocumentation.Implementation.ServiciosEcuador.Models.Response;
using Common.UserDocumentation.Models.Configuration;
using Common.UserDocumentation.Models.Request;
using Common.UserDocumentation.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.UserDocumentation.Implementation.ServiciosEcuador;

/// <summary>
/// Implementación para ServiciosEcuador
/// </summary>
public class ServiciosEcuadorDocumentationServicesImplementation : DocumentationServicesBase, IDocumentationServices
{
    protected override DocumentationImplementationName ImplementationName => DocumentationImplementationName.ServiciosEcuador;
    protected readonly ServiciosEcuadorConfiguration ServiciosEcuadorConfiguration;
    protected readonly HttpClient HttpClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <param name="httpClientFactory"></param>
    public ServiciosEcuadorDocumentationServicesImplementation(
        ILogger<ServiciosEcuadorDocumentationServicesImplementation> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory
        ) : base(logger)
    {
        ServiciosEcuadorConfiguration = configuration.GetSection(nameof(DocumentationServicesConfiguration)).Get<DocumentationServicesConfiguration<ServiciosEcuadorConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
            ?? throw new CustomDocumentationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(ServiciosEcuadorConfiguration)}");
        HttpClient = httpClientFactory.CreateClient($"{ImplementationName}");
        HttpClient.BaseAddress = new Uri(ServiciosEcuadorConfiguration.BaseUrl);
        HttpClient.Timeout = TimeSpan.FromSeconds(ServiciosEcuadorConfiguration.Timeout);
    }

    /// <summary>
    /// Verificar documento
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<VerifyDocumentResponse> GetPersonInformationAsync(VerifyDocumentRequest request)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Restart();
        var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(
            "api/v1/person/GetByDocumentNumber",
            new Dictionary<string, string> { { "DocumentNumber", request.DocumentNumber } });

        var response = await HttpClient.GetAsync(queryParams).ConfigureAwait(false);
        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        Logger.LogInformation("Respuesta servicio en '{@DurationTime}ms' de Validación de Cédula ServiciosEcuador: '{@DocumentNumber}' - Respuesta: '{@ResponseContent}'", stopwatch.ElapsedMilliseconds, request.DocumentNumber, responseContent);

        if (!response.IsSuccessStatusCode)
            throw new CustomDocumentationException($"Error ejecutando consulta de Documentación en ServiciosEcuador");

        var apiResponse = JsonConvert.DeserializeObject<PersonByDocumentResponse>(responseContent)
            ?? throw new CustomDocumentationException($"No se pudo deserializar la respuesta del servicio ServiciosEcuador");

        if (apiResponse.Code != 0 || apiResponse.Content?.Person == null)
            throw new CustomDocumentationException($"No se encontró información en la respuesta del servicio ServiciosEcuador: {apiResponse.Message}");

        var person = apiResponse.Content.Person;
        return new VerifyDocumentResponse
        {
            FullName = person.FullName,
            Identification = person.DocumentNumber,
            Names = person.Names,
            LastNames = person.LastNames,
        };
    }
}

