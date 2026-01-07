using MediatR;

namespace LogicCommon.BusinessLogic.PersonHandler;

/// <summary>
/// Clase base para handlers de persona
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class PersonBase<TRequest, TResponse>(
    ILogger<PersonBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicCommonBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

