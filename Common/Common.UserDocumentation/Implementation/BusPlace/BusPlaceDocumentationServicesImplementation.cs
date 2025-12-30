using System.Diagnostics;
using System.Linq;
using Common.UserDocumentation.DocumentationException;
using Common.UserDocumentation.Implementation.BusPlace.Models.Response;
using Common.UserDocumentation.Models.Configuration;
using Common.UserDocumentation.Models.Request;
using Common.UserDocumentation.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.UserDocumentation.Implementation.BusPlace;
/// <summary>
/// Implementación para BusPlace
/// </summary>
public class BusPlaceDocumentationServicesImplementation : DocumentationServicesBase, IDocumentationServices
{
    protected override DocumentationImplementationName ImplementationName => DocumentationImplementationName.BusPlace;
    protected readonly BusPlaceConfiguration BusPlaceConfiguration;
    protected readonly HttpClient HttpClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <param name="httpClientFactory"></param>
    public BusPlaceDocumentationServicesImplementation(
        ILogger<BusPlaceDocumentationServicesImplementation> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory
        ) : base(logger)
    {
        BusPlaceConfiguration = configuration.GetSection(nameof(DocumentationServicesConfiguration)).Get<DocumentationServicesConfiguration<BusPlaceConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
            ?? throw new CustomDocumentationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(BusPlaceConfiguration)}");
        HttpClient = httpClientFactory.CreateClient($"{ImplementationName}");
        HttpClient.BaseAddress = new Uri(BusPlaceConfiguration.BaseUrl);
        HttpClient.Timeout = TimeSpan.FromSeconds(BusPlaceConfiguration.Timeout);
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
        var response = await HttpClient.GetAsync($"v2/rest/pasajes/empresas?accion=getreferente&ref_cedula={request.DocumentNumber}").ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new CustomDocumentationException($"Error ejecutando consulta de Documentación en BusPlace");
        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        Logger.LogInformation("Respuesta servicio en '{@DurationTime}ms' de Validación de Cédula BusPlace: '{@DocumentNumber}' - Respuesta: '{@ResponseContent}'", stopwatch.ElapsedMilliseconds, request.DocumentNumber, responseContent);
        
        var documentInformation = JsonConvert.DeserializeObject<InternalBusPlaceResponse>(responseContent)
            ?? throw new CustomDocumentationException($"No se encontró información en la respuesta del servicio BusPlace");
        
        if (!documentInformation.Exists || documentInformation.Referent == null)
            throw new CustomDocumentationException($"No se encontró información del referente en BusPlace");
        
        var referent = documentInformation.Referent;
        var fullName = referent.FullName ?? string.Empty;
        
        var nameParts = fullName.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        string names = string.Empty;
        string lastNames = string.Empty;
        
        if (nameParts.Length > 0)
        {
            if (nameParts.Length >= 2)
            {
                names = string.Join(" ", nameParts.TakeLast(Math.Min(2, nameParts.Length)));
                lastNames = string.Join(" ", nameParts.Take(nameParts.Length - Math.Min(2, nameParts.Length)));
            }
            else
            {
                names = fullName;
            }
        }
        
        return new VerifyDocumentResponse
        {
            FullName = fullName,
            Identification = referent.ReferentIdentification ?? request.DocumentNumber,
            Names = names,
            LastNames = lastNames,
        };
    }
}

