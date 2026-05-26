using Autofac;
using Common.PluginFactory.Extensions;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Abstractions.Interfaces.ProcessTracking;
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
        builder.ScanAssembliesFor<ICreateProcessTrackingHandler>();
        builder.ScanAssembliesFor<IGetProcessTrackingsPaginatedHandler>();
        builder.ScanAssembliesFor<IGetProcessTrackingByGuidHandler>();
        builder.ScanAssembliesFor<IUpdateProcessTrackingHandler>();
    }

}
