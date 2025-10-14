using Common.Card.CardException;
using Common.Card.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace Common.Card.Implementation.Rapidapi.Handler;

public class AddHeadersDelegatinHandler : DelegatingHandler
{
    private readonly RapidapiConfiguration _rapidapiConfiguration;
    /// <summary>
    /// Constructor
    /// </summary>
    public AddHeadersDelegatinHandler(IConfiguration configuration)
    {
        _rapidapiConfiguration = configuration.GetSection(nameof(CardServicesConfiguration)).Get<CardServicesConfiguration<RapidapiConfiguration>>()
            ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{CardImplementationName.Rapidapi}")?.Information
            ?? throw new CustomCardServicesException($"No se encontró la configuración {nameof(RapidapiConfiguration)}");
    }

    /// <summary>
    /// Envía mensaje
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        foreach (var header in _rapidapiConfiguration.Headers)
            request.Headers.Add(header.Key, header.Value);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
