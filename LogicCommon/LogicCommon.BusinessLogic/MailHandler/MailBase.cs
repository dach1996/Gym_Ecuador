using Common.Mail.Interface;
using Common.Templates.Interface.Types;

namespace LogicCommon.BusinessLogic.MailHandler;

/// <summary>
/// Clase base para correos
/// </summary>
public abstract class MailBase<TRequest, TResponse> : BusinessLogicCommonBase,
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IMailNotification MailNotification;
    protected readonly IMailTemplate MailTemplate;
    /// <summary>
    /// Envía correo
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pluginFactory"></param>
    protected MailBase(
        ILogger<MailBase<TRequest, TResponse>> logger,
        IPluginFactory pluginFactory)
        : base(logger, pluginFactory)
    {
        MailNotification = PluginFactory.GetPlugin<IMailNotification>(AppSettings.MailNotificationConfiguration?.CurrentImplementation, true);
        MailTemplate = PluginFactory.GetType<IMailTemplate>();
    }

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

