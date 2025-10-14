using Autofac;
using Common.Templates.Interface;
using Common.Templates.Interface.Types;

namespace Common.Templates.Implementations;
/// <summary>
/// Factoría de Templates
/// </summary>
public class TemplateFactory : ITemplateFactory
{
    protected readonly ILifetimeScope LifetimeScope;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public TemplateFactory(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    /// <summary>
    /// Obtiene implementación de Notificaciones
    /// </summary>
    /// <typeparam name="INotificationTemplate"></typeparam>
    /// <returns></returns>
    public INotificationTemplate NotificationTemplate => LifetimeScope.Resolve<INotificationTemplate>();
}