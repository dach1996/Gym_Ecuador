using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.Order;
using LogicApi.Model.Response.Order;
using LogicApi.Model.Response;
using LogicApi.Model.Request.Order.Payment;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Constructor
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class OrderController(
    IUserMessages userMessages,
    ILogger<OrderController> logger,
    IMediator mediator
          ) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{

      #region Methods Controller

      /// <summary>
      /// Genera Orden
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpPost]
      [ProducesResponseType(200, Type = typeof(GenericResponse<GenerateOrderResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse<GenerateOrderResponse>))]
      public async Task<IActionResult> GenerateOrder([FromBody] GenerateOrderRequest request)
              => Success(await Mediator.Send(request).ConfigureAwait(false));

      /// <summary>
      /// Cancelar Orden
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpDelete("Cancel")]
      [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse<HandlerResponse>))]
      public async Task<IActionResult> ManualCancelOrder([FromBody] ManualCancelOrderRequest request)
            => Success(await Mediator.Send(request).ConfigureAwait(false));

      /// <summary>
      /// Verifica los datos del request para cáculos e información
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpPost("VerifyValues")]
      [ProducesResponseType(200, Type = typeof(GenericResponse<VerifyOrderValuesResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse<GenerateOrderResponse>))]
      public async Task<IActionResult> VerifyOrderValues([FromBody] VerifyOrderValuesRequest request)
            => Success(await Mediator.Send(request).ConfigureAwait(false));

      /// <summary>
      /// Obtiene las ordenes paginados
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpGet("MyOrderPaginated")]
      [ProducesResponseType(200, Type = typeof(GenericResponse<GetMyOrdersPaginatedResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse))]
      public async Task<IActionResult> GetMyOrderPaginated([FromQuery] GetMyOrdersPaginatedRequest request)
            => Success(await Mediator.Send(request).ConfigureAwait(false));


      /// <summary>
      /// Obtiene el detalle de la órden
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpGet("Details")]
      [ProducesResponseType(200, Type = typeof(GenericResponse<GetOrderDetailsResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse))]
      public async Task<IActionResult> GetOrderDetails([FromQuery] GetOrderDetailsRequest request)
            => Success(await Mediator.Send(request).ConfigureAwait(false));

      /// <summary>
      /// Solicitud Inicial para Grupo de ordenes
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpGet("MyOrderInitial")]
      [ProducesResponseType(200, Type = typeof(GenericResponse<GetInitialMyOrdersResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse))]
      public async Task<IActionResult> GetInitialMyOrders([FromQuery] GetInitialMyOrdersRequest request)
            => Success(await Mediator.Send(request).ConfigureAwait(false));

      /// <summary>
      /// Pago de orden por Pasarela de Pagos (Tarjeta)
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpPost("Payment/Card")]
      [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse))]
      public async Task<IActionResult> PaymentOrderByCard([FromBody] PaymentOrderByCardRequest request)
            => Success(await Mediator.Send(request).ConfigureAwait(false));

      /// <summary>
      /// Pago de orden Pasarela de Pagos (Criptomoneda)
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpPost("Payment/Crypto")]
      [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse))]
      public async Task<IActionResult> PaymentOrderByCrypto([FromBody] PaymentOrderByCryptoRequest request)
            => Success(await Mediator.Send(request).ConfigureAwait(false));

      /// <summary>
      /// Pago de orden por Link de Pago
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpPost("Payment/Link")]
      [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
      [ProducesResponseType(400, Type = typeof(GenericResponse))]
      public async Task<IActionResult> PaymentOrderByLink([FromBody] PaymentOrderByLinkRequest request)
            => Success(await Mediator.Send(request).ConfigureAwait(false));

      #endregion
}