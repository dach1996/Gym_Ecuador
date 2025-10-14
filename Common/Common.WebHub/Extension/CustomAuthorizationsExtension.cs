using Common.WebHub.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebHub.Extension;

public static class CustomAuthenticationExtension
{
  public static void AddCustomAuthentications(this IServiceCollection services)
   => services.AddAuthentication()
              .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(SecuritySchemaName.ApiKeySchema, options => { })
                .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>(SecuritySchemaName.TokenSchema, options => { });
}