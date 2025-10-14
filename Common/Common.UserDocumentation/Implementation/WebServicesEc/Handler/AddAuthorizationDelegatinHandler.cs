using Common.UserDocumentation.DocumentationException;
using Common.UserDocumentation.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace Common.UserDocumentation.Implementation.WebServicesEc.Handler;

public class AddAuthorizationDelegatinHandler : DelegatingHandler
{
    private readonly WebServicesEcConfiguration _webServicesEcConfiguration;
    /// <summary>
    /// Constructor
    /// </summary>
    public AddAuthorizationDelegatinHandler(IConfiguration configuration)
    {
        _webServicesEcConfiguration = configuration.GetSection(nameof(DocumentationServicesConfiguration)).Get<DocumentationServicesConfiguration<WebServicesEcConfiguration>>()
            ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{DocumentationImplementationName.WebServicesEc}")?.Information
            ?? throw new CustomDocumentationException($"No se encontró la configuración {nameof(WebServicesEcConfiguration)}");
    }

    /// <summary>
    /// Envía mensaje
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Authorization", $"Bearer {_webServicesEcConfiguration.TokenAuthorization}");
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
