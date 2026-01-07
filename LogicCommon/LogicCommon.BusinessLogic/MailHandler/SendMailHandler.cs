using Common.Utils.Extensions;
using LogicCommon.Model.Request.Mail;
using LogicCommon.Model.Response.Mail;

namespace LogicCommon.BusinessLogic.MailHandler;
/// <summary>
/// Envía el Correo
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class SendMailHandler(
    ILogger<SendMailHandler> logger,
    IPluginFactory pluginFactory) : MailBase<SendMailRequest, SendMailResponse>(logger, pluginFactory)
{
    public async override Task<SendMailResponse> Handle(SendMailRequest request, CancellationToken cancellationToken)
    {
        var template = MailTemplate.GetTemplate(
            AppSettings.MailNotificationConfiguration?.CurrentImplementation,
            request.MailTemplateModel);
        var result = await MailNotification.SendMailAsync(
        new Common.Mail.Model.MailTemplateRequest(
            template.TemplateId,
            request.MailTemplateModel,
            request.To
        )
       ).ConfigureAwait(false);

        return result
            ? SendMailResponse.SuccessResponse()
            : SendMailResponse.FailResponse();
    }
}

