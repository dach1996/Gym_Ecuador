using LogicApi.Model.Request.Logger;
namespace LogicApi.BusinessLogic.LoggerHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class RegisterLogHandler(
    ILogger<RegisterLogHandler> logger,
    IPluginFactory pluginFactory) : LoggerBase<RegisterLogRequest, HandlerResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(RegisterLogRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.RegisterLog, request, async () =>
        {
            //Verifica que el Log tenga datos
            if (request.LogJson.IsNullOrEmpty())
                throw new CustomException((int)MessagesCodesError.SystemError, $"El contenido enviado está vacío.");
            //Almacena la información de Log
            Logger.LogInformation("Log recibido de Tipo: {@LogType}\nDesde la Plataforma: {@Platform}\n Contenido:{@Content}"
                , request.LogType
                , request.ContextRequest.Headers.Platform,
                request.LogJson);
            //Verifica si el Log fue solicitado por el Servidor
            return await Task.FromResult(HandlerResponse.Complete()).ConfigureAwait(false);
        });
}
