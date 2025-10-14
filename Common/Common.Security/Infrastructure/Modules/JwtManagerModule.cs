
using Autofac;
using Common.Security.Implementation;
using Common.Security.Interface;
using Common.Security.Model.Enum;

namespace Common.Security.Infrastructure.Modules;

public class JwtManagerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MobileJwtManager>().Keyed<IJwtManager>($"{JwtIdentifier.Mobile.ToString().ToUpper()}");

    }
}