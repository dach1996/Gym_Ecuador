using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Abstractions.Interfaces.ProcessTracking;

public interface IGetProcessTrackingsHandler
{
    /// <summary>
    /// Obtener seguimientos de procesos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetProcessTrackingsResponse> Handle(GetProcessTrackingsRequest request);
}
