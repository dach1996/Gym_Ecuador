using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Common.WebHub.Filters;
/// <summary>
/// Agregar contexto 
/// </summary>
public abstract class HubFilterBase(ILogger<HubFilterBase> logger) : IHubFilter
{
    protected readonly ILogger<HubFilterBase> Logger = logger;

   
}