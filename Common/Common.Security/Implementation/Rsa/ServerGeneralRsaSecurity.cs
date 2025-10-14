using Common.Security.Model.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Security.Implementation.Rsa;
/// <summary>
/// Constructor 
/// </summary>
/// <param name="logger"></param>
/// <param name="configuration"></param>
public class ServerGeneralRsaSecurity(
    ILogger<ServerGeneralRsaSecurity> logger,
    IConfiguration configuration) : FileRsaSecurityBase(logger, configuration)
{
    /// <summary>
    /// Implementaciòn
    /// </summary>
    protected override RsaSecurityImplementation RsaSecurityImplementationName => RsaSecurityImplementation.ServerGeneral;
}
