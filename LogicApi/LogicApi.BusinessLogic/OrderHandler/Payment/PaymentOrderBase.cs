namespace LogicApi.BusinessLogic.OrderHandler.Payment;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class PaymentOrderBase<TRequest>(
    ILogger<PaymentOrderBase<TRequest>> logger,
    IPluginFactory pluginFactory) : OrderBase<TRequest, HandlerResponse>(
        logger,
        pluginFactory)
     where TRequest : IRequest<HandlerResponse>
{
}
