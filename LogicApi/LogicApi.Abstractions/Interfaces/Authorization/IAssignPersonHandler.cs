using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
namespace LogicApi.Abstractions.Interfaces.Authorization;
public interface IAssignPersonHandler
{
    /// <summary>
    /// Asigna persona
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<LoginResponse> Handle(AssignPersonRequest request);
}
