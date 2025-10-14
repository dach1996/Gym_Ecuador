using LogicApi.Abstractions.Interfaces.Order.Payment;
using LogicApi.Model.Request.Order.Payment;
namespace LogicApi.BusinessLogic.OrderHandler.Payment.Card;
/// <summary>
/// Pago base de órden
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class PaymentOrderCardBase(
    ILogger<PaymentOrderCardBase> logger,
    IPluginFactory pluginFactory) : PaymentOrderBase<PaymentOrderByCardRequest>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(PaymentOrderByCardRequest request, CancellationToken cancellationToken)
      => await ExecuteHandlerAsync(OperationApiName.PaymentOrder, request, async () =>
         await PluginFactory.GetPlugin<IPaymentOrderCardHandler>($"{request.Implementation.GetEnumMember().ToUpper()}").Handle(request).ConfigureAwait(false));
}
