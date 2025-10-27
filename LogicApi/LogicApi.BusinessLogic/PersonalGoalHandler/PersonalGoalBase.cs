namespace LogicApi.BusinessLogic.PersonalGoalHandler;

/// <summary>
/// Clase base para handlers de objetivo personal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class PersonalGoalBase<TRequest, TResponse>(
    ILogger<PersonalGoalBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
