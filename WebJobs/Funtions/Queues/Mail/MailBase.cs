using Common.Mail.Interface;
using Common.Mail.Model.Templates;
using Common.PluginFactory.Interface;
using Common.Queue.Model.Template.Mails;
using Common.Utils.Extensions;
using LogicWebJob.Model.Request.Mail;
using SQLitePCL;

namespace WebJobs.Funtions.Queues.Mail;

public abstract class MailBase(
    ILogger<MailBase> logger,
    IPluginFactory pluginFactory) : QueueBase(logger, pluginFactory)
{
    protected readonly IMailNotification MailNotification;

    /// <summary>
    /// Envía el Correo
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="TMailQueueTemplate"></typeparam>
    /// <typeparam name="TMailTemplateModel"></typeparam>
    /// <returns></returns>
    protected async Task ExecuteQueueMailAsync<TMailQueueTemplate, TMailTemplateModel>(
        string message
        ) where TMailQueueTemplate : QueueMailBase
        where TMailTemplateModel : IMailTemplateModel
     => await ExecuteFunctionAsync(message, async () =>
         {
             var mailQueueTemplate = message.ToObject<TMailQueueTemplate>();
             var mailTemplateModel = message.ToObject<TMailTemplateModel>();
             if (AppSettingsWebJob.CustomLog?.QueueMessage ?? false)
                 logger.LogInformation("Mensaje: {@Message}", message);
             var request = new SendMailRequest
             {
                 MailTemplateModel = mailTemplateModel,
                 To = mailQueueTemplate.To,
                 ToCco = mailQueueTemplate.ToCco
             };
             logger.LogInformation("Request: {@Request}", request.ToJson());
             await Mediator.Send(request).ConfigureAwait(false);
         });
}