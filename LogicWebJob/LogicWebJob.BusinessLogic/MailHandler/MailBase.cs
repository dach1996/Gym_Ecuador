using Common.Mail.Interface;

namespace LogicWebJob.BusinessLogic.SendMailHandler;

/// <summary>
/// Clase base para correos
/// </summary>
public abstract class MailBase<TRequest, TResponse> : BusinessLogicWebJobBase,
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IMailNotification MailNotification;
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
        MailNotification = PluginFactory.GetPlugin<IMailNotification>(AppSettingsWebJob.MailNotificationConfiguration?.CurrentImplementation, true);
    }

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}