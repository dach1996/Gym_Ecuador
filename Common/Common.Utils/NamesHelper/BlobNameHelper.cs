namespace Common.Utils.NamesHelper;
public static class BlobNameHelper
{
    /// <summary>
    /// Formato de Nombre de imagenes de usuario
    /// </summary>
    /// <param name="extension"></param>
    /// <param name="userId"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static string GetUserImageName(string userId, string extension,DateTime? now = null)
        => $"UserImage_{userId}_{now ?? DateTime.Now:ddMMyyyyHHmmss}.{extension}";
}
