using Common.PluginFactory.Interface;
using Common.Queue.Model.Template;
using Common.Utils.Extensions;
using Common.WebJob.Model;
using MediatR;
using PersistenceDb.Repository.Interfaces.UnitOfWork;

namespace WebJobs.Funtions;

/// <summary>
/// Clase base para las funciones
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class FunctionBase(
    ILogger<FunctionBase> logger,
    IPluginFactory pluginFactory)
{
    protected abstract string FunctionName { get; }
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= pluginFactory.GetType<IMediator>();
    protected readonly AppSettingsWebJob AppSettingsWebJob = pluginFactory.GetType<AppSettingsWebJob>();

    /// <summary>
    /// Ejecución de Funciones
    /// </summary>
    /// <param name="functionName"></param>
    /// <param name="message"></param>
    /// <param name="process"></param>
    /// <returns></returns>
    protected async Task ExecuteFunctionAsync(string message, Func<Task> process)
    {
        var templateBase = message.ToObject<QueueTemplateBase>();
        try
        {
            using (pluginFactory.GetType<ICoreUnitOfWork>())
            using (pluginFactory.GetType<IAuthenticationUnitOfWork>())
            using (pluginFactory.GetType<IAdministrationUnitOfWork>())
            {
                using (logger.BeginScope(new Dictionary<string, object>   {
                    { "RequestId", templateBase.RequestId } ,
                    { "CorrelationId", templateBase.RequestId },
                    { "QueueIdentifier", templateBase.InternalIdentifier }   }))
                {
                    logger.LogInformation("** {@FunctionName} - {@Identifier} Inicia **", FunctionName, templateBase.InternalIdentifier);
                    await process().ConfigureAwait(false);
                    logger.LogInformation("** {@FunctionName} - {@Identifier} Termina **", FunctionName, templateBase.InternalIdentifier);

                }
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "** Error en {@FunctionName} - {@Identifier} - {@Message} **", FunctionName, templateBase.InternalIdentifier, ex.Message);
        }
    }
}