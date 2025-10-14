
using Microsoft.AspNetCore.Http;
namespace Common.Utils.Extensions;
public static class IFormFileExtension
{
    /// <summary>
    /// Obtiene el Stream de un IFormFile
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns></returns>
    public static Stream GetStream(this IFormFile formFile)
    {
        var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);

        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }

    /// <summary>
    /// Obtiene la extensión del archivo
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns></returns>
    public static string GetExtension(this IFormFile formFile, bool addDot = true)
    {
        var extension = Path.GetExtension(formFile.FileName);
        if (!addDot)
            extension = extension.Replace(".", "");
        return extension;
    }

    /// <summary>
    /// Obtiene la extensión del archivo
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns></returns>
    public static async Task<string> GetContentBase64Async(this IFormFile formFile)
    {
        var array = await formFile.OpenReadStream().ToArrayAsync().ConfigureAwait(false);
        return array.ToBase64();
    }

    /// <summary>
    /// Obtiene los bytes de un file
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns></returns>
    public static async Task<byte[]> GetContentByteArrayAsync(this IFormFile formFile)
        => await formFile.OpenReadStream().ToArrayAsync().ConfigureAwait(false);
}
