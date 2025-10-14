using System.Runtime.Serialization;

namespace Common.Mail.MailException;

/// <summary>
/// Custom Exception
/// </summary>

public class CustomMailException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public CustomMailException(string message) : base(message)
    {
    }

}