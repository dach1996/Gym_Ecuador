using System.Runtime.Serialization;
using Autofac;
using Common.Cooperative.Implementation.Imbabura;
using Common.Cooperative.Implementation.Panamericana;
using Common.Cooperative.Implementation.Trasandina;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Cooperative.Infrastructure;
public static class CooperativeServicesExtension
{
    public static void UserCooperativeServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<PanamericanaCooperativeServices>().Keyed<ICooperativeServices>($"{CooperativeImplementationName.Panamericana.GetEnumMember().ToString().ToUpper()}");
        containerBuilder.RegisterType<ImbaburaCooperativeServices>().Keyed<ICooperativeServices>($"{CooperativeImplementationName.Imbabura.GetEnumMember().ToString().ToUpper()}");
        containerBuilder.RegisterType<TrasandinaExpressCooperativeServices>().Keyed<ICooperativeServices>($"{CooperativeImplementationName.TrasandinaExpress.GetEnumMember().ToString().ToUpper()}");
    }

    public static void AddCooperativeServices(this IServiceCollection services)
    {
        services.AddHttpClient($"{CooperativeImplementationName.Panamericana}");
        services.AddHttpClient($"{CooperativeImplementationName.Imbabura}");
        services.AddHttpClient($"{CooperativeImplementationName.TrasandinaExpress}");
    }

    /// <summary>
    /// Obtiene el Enumerable
    /// </summary>
    private static string GetEnumMember(this Enum @enum)
    {
        var attr = @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?
                                  .GetCustomAttributes(false).OfType<EnumMemberAttribute>()
                                  .FirstOrDefault();
        return attr == null ? @enum.ToString() : attr.Value;
    }
}
