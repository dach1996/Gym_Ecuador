using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;

namespace LogicApi.Abstractions.Interfaces.Authorization;
public interface ILoginHandler
{
    /// <summary>
    /// Formas de Logín
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<LoginResponse> Handle(LoginRequest request);
}
