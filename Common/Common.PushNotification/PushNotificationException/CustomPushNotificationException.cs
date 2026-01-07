namespace Common.PushNotification.PushNotificationException;
/// <summary>
/// Push Notification Exception
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="message"></param>
public class CustomPushNotificationException(string message) : Exception(message)
{
}


