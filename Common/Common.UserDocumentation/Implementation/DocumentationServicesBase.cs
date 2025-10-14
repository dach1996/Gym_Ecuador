using Microsoft.Extensions.Logging;

namespace Common.UserDocumentation.Implementation;

/// <summary>
/// Clase base para implementación de servicios
/// </summary>
public abstract class DocumentationServicesBase
{
    protected abstract DocumentationImplementationName ImplementationName { get; }

    protected readonly ILogger<DocumentationServicesBase> Logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    protected DocumentationServicesBase(ILogger<DocumentationServicesBase> logger)
    {
        Logger = logger;
    }
}