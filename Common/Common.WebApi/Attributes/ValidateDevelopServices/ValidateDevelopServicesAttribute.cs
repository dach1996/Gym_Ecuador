
using Common.Utils.CustomExceptions;
using Common.WebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace Common.WebApi.Attributes.ValidateDevelopServices;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidateDevelopServicesAttribute : ActionFilterAttribute
{
    private readonly IWebHostEnvironment _env;
    public ValidateDevelopServicesAttribute(IWebHostEnvironment env) => _env = env;

    /// <summary>
    /// Action
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //Verifica si existe un scope configurato en el Token

        if (context.ActionDescriptor.EndpointMetadata.Any(t => t is OnlyDevelopAttribute) && _env.IsProduction())
            throw new AuthException($"El Servicio no puede ser utilizado en ambientes Productivos");

        base.OnActionExecuting(context);
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class OnlyDevelopAttribute : Attribute
{

}


