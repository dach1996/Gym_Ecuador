using Common.Mail.Model.Enum;

namespace Common.Mail.Model.Templates;
/// <summary>
/// Modelo de Template
/// </summary>
public interface IMailTemplateModel
{
    MailTemplateCodes Identifier { get; }
}