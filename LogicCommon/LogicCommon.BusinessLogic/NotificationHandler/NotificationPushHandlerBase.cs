using Common.PushNotification;
namespace LogicCommon.BusinessLogic.NotificationHandler;
public abstract class NotificationPushHandlerBase<TRequest, TResponse> : BusinessLogicCommonBase,
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IPushNotification PushNotificationPlatform;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pluginFactory"></param>
    protected NotificationPushHandlerBase(
        ILogger<NotificationPushHandlerBase<TRequest, TResponse>> logger,
        IPluginFactory pluginFactory)
        : base(logger, pluginFactory)
    {
        PushNotificationPlatform = PluginFactory.GetPlugin<IPushNotification>(AppSettings.PushNotificationConfiguration.CurrentImplementation);
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}