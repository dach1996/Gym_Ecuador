using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Abstractions.Interfaces.ProcessTracking;

public interface IGetProcessTrackingsPaginatedHandler
{
    /// <summary>
    /// Obtener seguimientos de proceso paginados
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetProcessTrackingsPaginatedResponse> Handle(GetProcessTrackingsPaginatedRequest request);
}
