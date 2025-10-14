using Autofac;
using Common.PluginFactory.Extensions;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Abstractions.Interfaces.Order.Payment;
using LogicApi.Abstractions.Interfaces.Seat;
using LogicApi.Abstractions.Interfaces.Security;

namespace LogicApi.Abstractions.Infrastructure;
public static class AbstractionsExtension
{
    public static void UseAbstractionsAssemblies(this ContainerBuilder builder)
    {
        builder.ScanAssembliesFor<ILoginHandler>();
        builder.ScanAssembliesFor<ICreateUserHandler>();
        builder.ScanAssembliesFor<IAssignPersonHandler>();
        builder.ScanAssembliesFor<IDocumentValidationHandler>();
        builder.ScanAssembliesFor<IGetSeatAvailableHandler>();
        builder.ScanAssembliesFor<IPaymentOrderCardHandler>();
        builder.ScanAssembliesFor<IPaymentOrderCrypto>();
        builder.ScanAssembliesFor<IPaymentOrderLink>();
    }

}
