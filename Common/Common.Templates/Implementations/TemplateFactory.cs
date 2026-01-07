using Autofac;
using Common.Templates.Interface;
using Common.Templates.Interface.Types;

namespace Common.Templates.Implementations;
/// <summary>
/// Factoría de Templates
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="lifetimeScope"></param>
public class TemplateFactory(ILifetimeScope lifetimeScope) : ITemplateFactory
{
    protected readonly ILifetimeScope LifetimeScope = lifetimeScope;

    /// <summary>
    /// Obtiene implementación de Notificaciones
    /// </summary>
    /// <typeparam name="INotificationTemplate"></typeparam>
    /// <returns></returns>
    public INotificationTemplate NotificationTemplate => LifetimeScope.Resolve<INotificationTemplate>();


    /// <summary>
    /// Obtiene implementación de Mail
    /// </summary>
    /// <returns></returns>
    public IMailTemplate MailTemplate => LifetimeScope.Resolve<IMailTemplate>();
}