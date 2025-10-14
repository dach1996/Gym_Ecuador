using System.Runtime.Serialization;
namespace Common.PushNotification.PushNotificationException;
/// <summary>
/// Push Notification Exception
/// </summary>

public class CustomPushNotificationException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public CustomPushNotificationException(string message) : base(message)
    {
    }
}
