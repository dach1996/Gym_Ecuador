using Autofac;
using Common.Authentication.Implementation.Google;
using Common.Authentication.Implementation.Mock;

namespace Common.Authentication.Infrastructure;
public static class AuthenticationServicesExtension
{
    public static void UseAuthenticationServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<GoogleAuthenticationServicesImplementation>()
            .Keyed<IAuthenticationService>($"{AuthenticationImplementationName.Google.ToString().ToUpper()}");
           containerBuilder.RegisterType<MockAuthenticationServicesImplementation>()
            .Keyed<IAuthenticationService>($"{AuthenticationImplementationName.Mock.ToString().ToUpper()}");
    }
}
