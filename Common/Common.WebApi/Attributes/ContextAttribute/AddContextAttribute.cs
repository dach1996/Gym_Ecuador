using Common.PluginFactory.Interface;
using Common.WebApi.Attributes.ContextAttribute.Interface;
using Common.WebApi.Models;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.WebApi.Attributes.ContextAttribute;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="webHostEnvironment"></param>
[AttributeUsage(AttributeTargets.Class)]
public class AddContextAttribute(IPluginFactory pluginFactory, AppSettingsCommon appSettings) : ActionFilterAttribute
{

    /// <summary>
    /// Ejecución de acción
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        pluginFactory
           .GetPlugin<IAddContext>(appSettings.GeneralConfiguration.AddContextImplementation.ToUpper(), true)
           .OnActionExecuting(context);
        base.OnActionExecuting(context);
    }


    /// <summary>
    /// Ejecución tras resultado
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuted(ResultExecutedContext context) => pluginFactory
           .GetPlugin<IAddContext>(appSettings.GeneralConfiguration.AddContextImplementation.ToUpper(), true)
           .OnResultExecuted(context);

}

