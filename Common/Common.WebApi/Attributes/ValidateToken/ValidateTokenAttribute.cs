using Common.Cache.Interface;
using Common.Messages;
using Common.PluginFactory.Interface;
using Common.Utils.ConstansCodes;
using Common.Utils.CustomExceptions;
using Common.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Common.WebApi.Attributes.ValidateToken;
/// <summary>
/// Constructor
/// </summary>
/// <param name="pluginFactory"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidateTokenAttribute(IPluginFactory pluginFactory) : ActionFilterAttribute
{
    private readonly IAdministratorCache _administratorCache = pluginFactory.GetType<IAdministratorCache>();

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //Obtiene el contexto
        var contextRequest = context.HttpContext.Items[nameof(ContextRequest)] as ContextRequest;
        //Verifica si se registró un token
        if (!string.IsNullOrEmpty(contextRequest?.Headers?.Authorization) && await _administratorCache.ExistKeyAsync(CacheCodes.LogOutToken(contextRequest.Headers.Authorization)).ConfigureAwait(false))
            throw new CustomException((int)MessagesCodesError.TokenNotAllow, "El token utilizado ya ha sido registrado anteriormente como LogOut o Refresh Token.");
        await base.OnActionExecutionAsync(context, next).ConfigureAwait(false);
    }
}

