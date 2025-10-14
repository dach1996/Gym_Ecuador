using LogicApi.Model.Request.Order.Payment;
using LogicApi.Model.Response;

namespace LogicApi.Abstractions.Interfaces.Order.Payment;

public interface IPaymentOrderLink
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<HandlerResponse> Handle(PaymentOrderByLinkRequest request);
}