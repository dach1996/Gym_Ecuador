using System.Runtime.Serialization;

namespace Common.EventHub.EventHubException;

/// <summary>
/// Custom Exception
/// </summary>

public class CustomEventHubException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public CustomEventHubException(string message) : base(message)
    {
    }

}
