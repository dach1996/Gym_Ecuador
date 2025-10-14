
using Autofac;
using Common.Security.Implementation.Rsa;
using Common.Security.Interface;
using Common.Security.Model.Enum;

namespace Common.Security.Infrastructure.Modules;
public class RsaSecurityModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ServerGeneralRsaSecurity>().Keyed<IRsaSecurity>(nameof(RsaSecurityImplementation.ServerGeneral).ToUpper()).SingleInstance();
        builder.RegisterType<DeviceGeneralRsaSecurity>().Keyed<IRsaSecurity>(nameof(RsaSecurityImplementation.DeviceGeneral).ToUpper()).SingleInstance();
    }
}