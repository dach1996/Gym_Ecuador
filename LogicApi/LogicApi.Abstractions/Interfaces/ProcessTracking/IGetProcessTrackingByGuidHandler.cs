using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Abstractions.Interfaces.ProcessTracking;

public interface IGetProcessTrackingByGuidHandler
{
    /// <summary>
    /// Obtener seguimiento de proceso por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetProcessTrackingByGuidResponse> Handle(GetProcessTrackingByGuidRequest request);
}
