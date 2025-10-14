using Common.Templates.Interface.Types;

namespace Common.Templates.Interface;
/// <summary>
/// Template Factory
/// </summary>
public interface ITemplateFactory
{
    INotificationTemplate NotificationTemplate { get; }
}