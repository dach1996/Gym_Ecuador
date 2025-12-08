
using Autofac;
using Common.Security.Implementation.Jwt;
using Common.Security.Interface;
using Common.Security.Model.Enum;

namespace Common.Security.Infrastructure.Modules;

public class JwtManagerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MobileJwtManager>().Keyed<IJwtManager>($"{JwtIdentifier.Mobile.ToString().ToUpper()}").SingleInstance();
        builder.RegisterType<WebJwtManager>().Keyed<IJwtManager>($"{JwtIdentifier.Web.ToString().ToUpper()}").SingleInstance();
    }
}