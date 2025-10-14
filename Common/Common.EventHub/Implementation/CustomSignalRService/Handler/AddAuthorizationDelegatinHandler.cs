using Common.EventHub.EventHubException;
using Common.EventHub.Models.Configuration;
using Common.EventHub.Models.Configuration.CustomEventHubService;
using Microsoft.Extensions.Configuration;

namespace Common.EventHub.Implementation.CustomSignalRService.Handler;

/// <summary>
/// Constructor
/// </summary>
public class AddAuthorizationDelegatinHandler(IConfiguration configuration) : DelegatingHandler
{
    private readonly CustomSignalRConfiguration configuration = configuration.GetSection(nameof(EventHubConfiguration)).Get<EventHubConfiguration<CustomSignalRConfiguration>>()
            ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{EventHubImplementationName.CustomSignalR}")?.Information
            ?? throw new CustomEventHubException($"No se encontró la configuración {nameof(CustomSignalRConfiguration)}");

    /// <summary>
    /// Envía mensaje
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("X-Api-Key", configuration.ApiKey);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
