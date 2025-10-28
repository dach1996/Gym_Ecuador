using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Abstractions.Interfaces.ProcessTracking;

public interface ICreateProcessTrackingHandler
{
    /// <summary>
    /// Crear seguimiento de proceso
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<CreateProcessTrackingResponse> Handle(CreateProcessTrackingRequest request);
}
