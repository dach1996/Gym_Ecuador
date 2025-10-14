using System.Runtime.Serialization;

namespace Common.Queue.QueueException;

/// <summary>
/// Custom Exception
/// </summary>

public class CustomQueueException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public CustomQueueException(string message) : base(message)
    {
    }
}