using Common.Utils.CustomExceptions;
using Common.WebApi.Models;
using Common.WebApi.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Common.WebApi.Attributes.ValidateScope;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidateScopeAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Action
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //Verifica si el servicio permite ingresar anónimanente
        if (!context.ActionDescriptor.EndpointMetadata.Any(t => t is AllowAnonymousAttribute))
        {
            var contextRequest = context.HttpContext.Items[nameof(ContextRequest)] as ContextRequest;
            var scopeString = contextRequest?.CustomClaims?.Scope;
            //Verifica si existe un scope configurato en el Token
            if (!string.IsNullOrEmpty(scopeString))
            {
                if (!Enum.TryParse<ScopeValidation>(scopeString, ignoreCase: true, out var scope))
                    throw new AuthException($"No se pudo mapear el tipo '{nameof(ScopeValidation)}' con el campo '{scope}'");
                if (context.ActionDescriptor.EndpointMetadata
                    .FirstOrDefault(t => t is ScopeAttribute) is not ScopeAttribute scopeValidationAttribute)
                    throw new AuthException($"El Token actual tiene un scope: '{scope}' y el servicio no tiene configurado ninguna validación de Token Scope");
                if (!scopeValidationAttribute.ScopesValidation.Contains(scope))
                    throw new AuthException($"El servicio no soporta el Token Scope: '{scope}'");
            }
        }
        base.OnActionExecuting(context);
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class ScopeAttribute : Attribute
{
    public ScopeValidation[] ScopesValidation { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="scopesValidation"></param>
    public ScopeAttribute(params ScopeValidation[] scopesValidation) => ScopesValidation = scopesValidation;
}


