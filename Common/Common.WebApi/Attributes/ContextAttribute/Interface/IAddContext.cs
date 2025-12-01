using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.WebApi.Attributes.ContextAttribute.Interface;

/// <summary>
/// Interface para agregar contexto
/// </summary>
public interface IAddContext
{
    /// <summary>
    /// Ejecución de método
    /// </summary>
    /// <param name="context"></param>
    void OnActionExecuting(ActionExecutingContext context);

    /// <summary>
    /// Acción luego de ejecutar
    /// </summary>
    /// <param name="context"></param>
    void OnResultExecuted(ResultExecutedContext context);
}