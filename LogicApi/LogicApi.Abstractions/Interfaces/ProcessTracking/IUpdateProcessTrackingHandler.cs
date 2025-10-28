using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Abstractions.Interfaces.ProcessTracking;

public interface IUpdateProcessTrackingHandler
{
    /// <summary>
    /// Actualizar seguimiento de proceso
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<UpdateProcessTrackingResponse> Handle(UpdateProcessTrackingRequest request);
}
