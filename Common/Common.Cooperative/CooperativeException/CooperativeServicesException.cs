using System.Runtime.Serialization;

namespace Common.Cooperative.CardException;

/// <summary>
/// Cooperative Service
/// </summary>

public class CooperativeServicesException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public CooperativeServicesException(string message) : base(message)
    {
    }
   
}
