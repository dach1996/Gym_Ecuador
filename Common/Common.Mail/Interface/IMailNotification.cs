using Common.Mail.Model;
namespace Common.Mail.Interface;
public interface IMailNotification
{
 
    /// <summary>
    /// Send a mail with templates
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> SendMailAsync(MailTemplateRequest request);
}