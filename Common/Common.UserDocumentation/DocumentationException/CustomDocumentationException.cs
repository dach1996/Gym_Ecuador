using System.Runtime.Serialization;

namespace Common.UserDocumentation.DocumentationException;

/// <summary>
/// Blob Exception
/// </summary>

public class CustomDocumentationException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public CustomDocumentationException(string message) : base(message)
    {
    }
}
