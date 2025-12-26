using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Common.Utils.ImageTools;
/// <summary>
///  Administrador de Imagenes
/// </summary>
public static class ImageManagement
{
    /// <summary>
    /// Optimizar Imagen
    /// </summary>
    /// <param name="imageData"></param>
    /// <param name="maxWidth"></param>
    /// <param name="quality"></param>
    /// <returns></returns>
    public static async Task<byte[]> OptimizeImageAsync(
        byte[] imageData,
        int maxWidth = 1200,
        int quality = 85)
    {
        using var msImage = new MemoryStream(imageData);
        using var image = await Image.LoadAsync(msImage).ConfigureAwait(false);

        // Redimensionar si es necesario
        if (image.Width > maxWidth)
        {
            var options = new ResizeOptions
            {
                Size = new Size(maxWidth, 0),
                Mode = ResizeMode.Max,
                
            };
            image.Mutate(x => x.Resize(options));
        }

        // Optimizar según formato
        using var outputStream = new MemoryStream();

        if (image.Metadata.DecodedImageFormat?.Name == "PNG")
        {
            var pngEncoder = new PngEncoder
            {
                CompressionLevel = PngCompressionLevel.BestCompression,
                ColorType = PngColorType.RgbWithAlpha
            };
            await image.SaveAsync(outputStream, pngEncoder);
        }
        else // JPEG por defecto
        {
            var jpegEncoder = new JpegEncoder
            {
                Quality = quality,
                ColorType = JpegEncodingColor.YCbCrRatio420
            };
            await image.SaveAsync(outputStream, jpegEncoder);
        }

        return outputStream.ToArray();
    }

    /// <summary>
    /// Optimizar Imagen
    /// </summary>
    /// <param name="base64Image"></param>
    /// <param name="maxWidth"></param>
    /// <param name="quality"></param>
    /// <returns></returns>
    public static async Task<byte[]> OptimizeImageAsync(
        string base64Image,
        int maxWidth = 1200,
        int quality = 85)
    {
        var imageData = Convert.FromBase64String(base64Image);
        return await OptimizeImageAsync(imageData, maxWidth, quality).ConfigureAwait(false);
    }
}
