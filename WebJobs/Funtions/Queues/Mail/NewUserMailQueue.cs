using Common.PluginFactory.Interface;
using Common.Queue.Model.Template;
using Microsoft.Azure.WebJobs;

namespace WebJobs.Funtions.Queues.Mail;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class NewUserMailQueue(
    ILogger<NewUserMailQueue> logger,
    IPluginFactory pluginFactory) : MailBase(logger, pluginFactory)
{
    protected override string FunctionName => nameof(NewUserMailQueue);


}