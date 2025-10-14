
namespace Common.Cache.CacheExcpetion;

/// <summary>
/// Blob Exception
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="message"></param>
public class CustomCacheException(string message) : Exception(message)
{
}