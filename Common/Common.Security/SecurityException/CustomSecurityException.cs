namespace Common.Security.BlobException;
/// <summary>
/// Blob Exception
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="message"></param>
public class CustomSecurityException(string message) : Exception(message)
{
}
