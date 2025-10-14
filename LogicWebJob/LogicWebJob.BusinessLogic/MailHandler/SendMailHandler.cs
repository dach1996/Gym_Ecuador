using Common.Utils.Extensions;
using LogicWebJob.BusinessLogic.SendMailHandler;
using LogicWebJob.Model.Request.Mail;

namespace LogicWebJob.BusinessLogic.MailHandler;
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
    IPluginFactory pluginFactory) : MailBase<SendMailRequest, Unit>(logger, pluginFactory)
{
    public async override Task<Unit> Handle(SendMailRequest request, CancellationToken cancellationToken)
    {
        await MailNotification.SendMailAsync(
        new Common.Mail.Model.MailTemplateRequest(
            request.MailTemplateModel.Identifier.GetEnumMember(),
            request.MailTemplateModel,
            request.To
        )
       ).ConfigureAwait(false);
        return Unit.Value;
    }
}