using System.Diagnostics;
using Common.UserDocumentation.DocumentationException;
using Common.UserDocumentation.Models.Configuration;
using Common.UserDocumentation.Models.Request;
using Common.UserDocumentation.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.UserDocumentation.Implementation.WebServicesEc;
/// <summary>
/// Implementaciòn para WebServicesEc
/// </summary>
public class WebServicesEcDocumentationServicesImplementation : DocumentationServicesBase, IDocumentationServices
{
    protected override DocumentationImplementationName ImplementationName => DocumentationImplementationName.WebServicesEc;
    protected readonly WebServicesEcConfiguration WebServicesEcConfiguration;
    protected readonly HttpClient HttpClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <returns></returns>
    public WebServicesEcDocumentationServicesImplementation(
        ILogger<WebServicesEcDocumentationServicesImplementation> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory
        ) : base(logger)
    {
        WebServicesEcConfiguration = configuration.GetSection(nameof(DocumentationServicesConfiguration)).Get<DocumentationServicesConfiguration<WebServicesEcConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
            ?? throw new CustomDocumentationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(WebServicesEcConfiguration)}");
        HttpClient = httpClientFactory.CreateClient($"{ImplementationName}");
        HttpClient.BaseAddress = new Uri(WebServicesEcConfiguration.BaseUrl);
        HttpClient.Timeout = TimeSpan.FromSeconds(WebServicesEcConfiguration.Timeout);
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
        var response = await HttpClient.GetAsync($"api/cedula/{request.DocumentNumber}").ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new CustomDocumentationException($"Error ejecutando consulta de Documentación");
        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        Logger.LogInformation("Respuesta servicio en '{@DurationTime}ms' de Validación de Cédula: '{@DocumentNumber}'", stopwatch.ElapsedMilliseconds, responseContent);
        var documentInformation = JsonConvert.DeserializeObject<InternalVerifyDocumentResponse>(responseContent)
            ?.Data?.Response
            ?? throw new CustomDocumentationException($"No se encontró información en la respuesta del servicio");
        return new VerifyDocumentResponse
        {
            FullName = documentInformation.FullName,
            Identification = documentInformation.Identification,
            Names = documentInformation.Names,
            LastNames = documentInformation.LastNames,
        };
    }
}