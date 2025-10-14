using System.Diagnostics;
using System.Net.Http.Json;
using Common.Card.CardException;
using Common.Card.Models.Configuration;
using Common.Card.Models.Request;
using Common.Card.Models.Response;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.Card.Implementation;
/// <summary>
/// Implementaciòn para RapiDapi
/// /// </summary>
public class RapidapiCardServicesImplementation : CardServicesBase, ICardServices
{
    protected override CardImplementationName ImplementationName => CardImplementationName.Rapidapi;
    protected readonly RapidapiConfiguration RapidapiConfiguration;
    protected readonly HttpClient HttpClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <returns></returns>
    public RapidapiCardServicesImplementation(
        ILogger<RapidapiCardServicesImplementation> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory
        ) : base(logger)
    {
        RapidapiConfiguration = configuration.GetSection(nameof(CardServicesConfiguration)).Get<CardServicesConfiguration<RapidapiConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
            ?? throw new CustomCardServicesException($"No se encontró la configuración de servicios de tarjeta con identificador{nameof(RapidapiConfiguration)}");
        HttpClient = httpClientFactory.CreateClient($"{ImplementationName}");
        HttpClient.BaseAddress = new Uri(RapidapiConfiguration.BaseUrl);
    }

    /// <summary>
    /// Verificar Bin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<VerifyBinResponse> VerifyBinAsync(VerifyBinRequest request)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Restart();
        var query = new Dictionary<string, string>()
        {
            ["bin"] = request.Bin,
        };
        var queryParameters = QueryHelpers.AddQueryString(string.Empty, query);
        var response = await HttpClient.PostAsJsonAsync(queryParameters, request).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new CustomCardServicesException($"Error ejecutando consulta de Bines");
        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        Logger.LogInformation("Respuesta servicio en '{@DurationTime}ms' de Validación de Bin: '{@BinValidationResponse}'", stopwatch.ElapsedMilliseconds, responseContent);
        var binInformation = JsonConvert.DeserializeObject<InternalVerifyBinResponse>(responseContent)?.Bin;
        return new(
            cardBrand: GetCardBrand(binInformation),
            cardType: GetCardType(binInformation));
    }

    /// <summary>
    /// Obtiene la marca de la tarejta
    /// </summary>
    /// <param name="binInformation"></param>
    /// <returns></returns>
    private static CardBrand GetCardBrand(BinInformation binInformation) => binInformation.Brand switch
    {
        "AMERICAN EXPRESS" => CardBrand.AmericanExpress,
        "VISA" => CardBrand.Visa,
        "MASTERCARD" => CardBrand.Mastercard,
        "DISCOVER" => CardBrand.Diners,
        "DINERS CLUB INTERNATIONAL" => CardBrand.Discover,
        "JCB" => CardBrand.Jcb,
        "MAESTRO" => CardBrand.Maestro,
        _ => throw new ArgumentException($"El argumento : {binInformation.Brand} no pudo ser transformado en {typeof(CardBrand)}", nameof(binInformation))
    };
    /// <summary>
    /// Obtiene el tipo de Tarjeta
    /// </summary>
    /// <param name="binInformation"></param>
    /// <returns></returns>
    private static CardType GetCardType(BinInformation binInformation) => binInformation.Type switch
    {
        "CREDIT" => CardType.Credit,
        "DEBIT" => CardType.Debit,
        _ => throw new ArgumentException($"El argumento : {binInformation.Type} no pudo ser transformado en {typeof(CardType)}", nameof(binInformation))
    };
}