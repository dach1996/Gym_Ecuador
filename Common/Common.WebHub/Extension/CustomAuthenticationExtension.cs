using Common.WebHub.Authorization;
using Common.WebHub.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebHub.Extension;

public static class CustomAuthorizationsExtension
{
    public static void AddCustomAuthorizations(this IServiceCollection services)
     => services.AddAuthorizationBuilder()
      .AddPolicy(AuthorizationPolicyName.JwtPolicy, policy =>
          {
              policy.AddAuthenticationSchemes(SecuritySchemaName.TokenSchema);
              policy.RequireAuthenticatedUser();
          })
      .AddPolicy(AuthorizationPolicyName.ApiKeyPolicy, policy =>
          {
              policy.AddAuthenticationSchemes(SecuritySchemaName.ApiKeySchema);
              policy.RequireAuthenticatedUser();
          });
}