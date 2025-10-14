using Common.WebJob.Model;
using Common.Utils.CustomExceptions;
using Common.WebCommon.Models.Enum;
using PersistenceDb.Models.Enums;
using LogicCommon.BusinessLogic;
using PersistenceDb.Repository.Interfaces.UnitOfWork;
namespace LogicWebJob.BusinessLogic;
/// <summary>
/// Clase base para Lógica de WebJobs
/// </summary>
public abstract class BusinessLogicWebJobBase : BusinessLogicCommonBase
{
    protected readonly AppSettingsWebJob AppSettingsWebJob;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pluginFactory"></param>
    protected BusinessLogicWebJobBase(
        ILogger<BusinessLogicWebJobBase> logger,
        IPluginFactory pluginFactory)
        : base(logger, pluginFactory)
    {
        AppSettingsWebJob = PluginFactory.GetType<AppSettingsWebJob>();
        var connection = AppSettingsWebJob.CustomConnectionStrings.Find(where => where.Identifier == $"{DataBaseConnectionIdentifier.WebJob}")
                ?? throw new WebJobException($"La cadena de conexión en el Contexto está vacía");
        CommonContextRequest = new()
        {
            RequestId = Guid.NewGuid().ToString(),
            DataBaseConfiguration = connection,
            TimeZone = AppSettingsWebJob.TimeZone,
        };
        Clock.ConfigureTimeZone(AppSettingsWebJob.TimeZone);
    }

    /// <summary>
    /// Ejecuta el proceso
    /// </summary>
    /// <param name="process"></param>
    /// <param name="unitOfWorkTypes"></param>
    /// <param name="applyTransaction"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected async Task<T> ExecuteHandlerAsync<T>(
           Func<Task<T>> process)
    {

        try
        {
            //Ejecuta el proceso
            var result = await process().ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error: {@Message}", ex.Message);
            return default;
        }
    }

}