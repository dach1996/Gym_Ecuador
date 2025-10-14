namespace Common.Utils.Extensions;

public static class StreamExtensions
{
    /// <summary>
    /// Convierte en Array
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static async Task<byte[]> ToArrayAsync(this Stream stream)
    {
        stream.Position = 0;
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    /// <summary>
    /// Convierte en Array
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static async Task<MemoryStream> ToMemoryStreamAsync(this Stream stream)
    {
        stream.Position = 0;
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return memoryStream;
    }
}