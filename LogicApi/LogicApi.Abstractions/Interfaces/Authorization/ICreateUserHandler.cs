using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;

namespace LogicApi.Abstractions.Interfaces.Authorization;
public interface ICreateUserHandler
{
    /// <summary>
    /// Formas de Logín
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<CreateUserResponse> Handle(CreateUserRequest request);
}
