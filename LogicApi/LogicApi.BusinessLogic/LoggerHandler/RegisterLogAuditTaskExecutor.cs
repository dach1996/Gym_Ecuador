using Common.Tasks;
using Common.WebCommon.Json;
using LogicApi.Model.Request.Logger;
using PersistenceDb.Models.Administration;

namespace LogicApi.BusinessLogic.LoggerHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="loggerFactory"></param>
/// <param name="administrationUnitOfWork"></param>
/// <param name="clock"></param>
public class RegisterLogAuditTaskExecutor(
    ILoggerFactory loggerFactory,
    IAdministrationUnitOfWork administrationUnitOfWork,
    IClock clock) : ITaskExecutor
{
    private readonly ILogger<RegisterLogAuditTaskExecutor> _logger = loggerFactory.CreateLogger<RegisterLogAuditTaskExecutor>();
    private readonly IAdministrationUnitOfWork _administrationUnitOfWork = administrationUnitOfWork;
    private readonly IClock _clock = clock;

    public async Task Execute(IExecutorModel model)
    {
        var request = model as RegisterLogAuditExecutorModel;
        using (_administrationUnitOfWork)
        {
            try
            {
                var context = request.ContextRequest;
                if (context?.DataBaseConfiguration is null)
                    throw new CustomException((int)MessagesCodesError.SystemError, $"La cadena de conexión en el Contexto está vacía");
                if (context.TimeZone.IsNullOrEmpty())
                    throw new CustomException((int)MessagesCodesError.SystemError, $"La zona horaria en el Contexto está vacía");
                await _administrationUnitOfWork.SetDataBaseConfigurationAsync(context.DataBaseConfiguration.ToJson().ToObject<PersistenceDb.Models.Configuration.DatabaseConfiguration>()).ConfigureAwait(false);
                _clock.ConfigureTimeZone(context.TimeZone);
                //Registra el Log
                await _administrationUnitOfWork.AuditLogRepository.AddAsync(new AuditLog
                {
                    DateTime = _clock.Now(),
                    UserId = request.ContextRequest.CustomClaims.UserId,
                    UserName = request.ContextRequest.CustomClaims.UserName ?? string.Empty,
                    OriginIp = context.IpOrigin ?? string.Empty,
                    Operation = request.OperationName.GetEnumMember(),
                    Result = request.OperationResult,
                    RequestData = request.RequestData.ToJsonIgnore(),
                    ResponseData = request.ResponseData.ToJsonIgnore(),
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error el guadar log de Auditoría: {@Message}", ex.Message);
            }
        }
    }
}