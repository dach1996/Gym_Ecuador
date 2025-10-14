using System.Runtime.Serialization;

namespace Common.Blob.BlobException;

/// <summary>
/// Blob Exception
/// </summary>

public class CustomBlobException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public CustomBlobException(string message) : base(message)
    {
    }
}
