using System.Runtime.Serialization;

namespace Common.Card.CardException;

/// <summary>
/// Blob Exception
/// </summary>

public class CustomCardServicesException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public CustomCardServicesException(string message) : base(message)
    {
    }

}
