using LogicApi.Model.Request.Seat;
using LogicApi.Model.Response.Seat;

namespace LogicApi.Abstractions.Interfaces.Seat;

public interface IGetSeatAvailableHandler
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetSeatAvailableResponse> Handle(GetSeatAvailableRequest request);
}