using Common.WebApi.Attributes.ContextAttribute;
using Common.WebApi.Attributes.DecryptAttribute;
using Common.WebApi.Attributes.ValidateDevelopServices;
using Common.WebApi.Attributes.ValidateScope;
using Common.WebApi.Attributes.ValidateToken;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApi.Attributes.Infrastructure;

public static class AttributesExtension
{
    public static void AddCustomAttributes(this IServiceCollection services)
    {
        _ = services.AddSingleton<AddContextAttribute>();
        _ = services.AddSingleton<DecryptFieldAttribute>();
        _ = services.AddSingleton<ValidateScopeAttribute>();
        _ = services.AddSingleton<ValidateDevelopServicesAttribute>();
        _ = services.AddSingleton<ValidateTokenAttribute>();
    }
}