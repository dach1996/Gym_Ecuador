using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Common.WebHub.Filters;
/// <summary>
/// Agregar contexto 
/// </summary>
public class ValidatorModelHubFilter(ILogger<ValidatorModelHubFilter> logger) : HubFilterBase(logger), IHubFilter
{
    /// <summary>
    /// Contexto
    /// </summary>
    /// <param name="invocationContext"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async ValueTask<object> InvokeMethodAsync(
      HubInvocationContext invocationContext,
      Func<HubInvocationContext, ValueTask<object>> next)
    {
        foreach (var arg in invocationContext.HubMethodArguments)
        {
            if (arg == null) continue;
            var validationContext = new ValidationContext(arg);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(arg, validationContext, validationResults, true))
            {
                logger.LogError("Modelo de Request invalido para el Evento: {@EventName} con argumentos: {@Arguments}", invocationContext.HubMethodName, invocationContext.HubMethodArguments);
                throw new HubException("Modelo de Request invalido: " + string.Join(", ", validationResults.Select(v => v.ErrorMessage)));
            }
        }
        return await next(invocationContext);

    }
}