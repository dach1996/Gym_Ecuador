using Common.UserDocumentation;
using Common.UserDocumentation.DocumentationException;
using Common.UserDocumentation.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace Common.UserDocumentation.Implementation.ServiciosEcuador.Handler;

/// <summary>
/// Handler para agregar autorización a las peticiones
/// </summary>
public class AddAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly ServiciosEcuadorConfiguration _serviciosEcuadorConfiguration;
    
    /// <summary>
    /// Constructor
    /// </summary>
    public AddAuthorizationDelegatingHandler(IConfiguration configuration)
    {
        _serviciosEcuadorConfiguration = configuration.GetSection(nameof(DocumentationServicesConfiguration)).Get<DocumentationServicesConfiguration<ServiciosEcuadorConfiguration>>()
            ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{DocumentationImplementationName.ServiciosEcuador}")?.Information
            ?? throw new CustomDocumentationException($"No se encontró la configuración {nameof(ServiciosEcuadorConfiguration)}");
    }

    /// <summary>
    /// Envía mensaje
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(_serviciosEcuadorConfiguration.ApiKey))
        {
            request.Headers.Add("X-Api-Key", _serviciosEcuadorConfiguration.ApiKey);
        }
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}

