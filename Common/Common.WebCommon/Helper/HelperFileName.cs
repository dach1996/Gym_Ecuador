namespace Common.WebCommon.Helper;

/// <summary>
/// Helper para nombres de archivos
/// </summary>
public static class HelperFileName
{
    /// <summary>
    /// Formato de Nombre de imagenes genéricas
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="extension"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    private static string GenericImageName(string prefix, string extension, DateTime? now = null)
    {
        var guid = $"{Guid.NewGuid():N}";
        var last5 = guid.Length >= 5 ? guid[^5..] : guid;
        return $"{prefix}_{now ?? DateTime.Now:yyyyMMddHHmmss}{last5}.{extension}";
    }

    /// <summary>
    /// Formato de Nombre de imagenes de usuario
    /// </summary>
    /// <param name="extension"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static string GetUserImageName(string extension, DateTime? now = null)
        => GenericImageName("UserImage", extension, now);

    /// <summary>
    /// Formato de Nombre de imagenes de sucursales de gimnasio
    /// </summary>
    /// <param name="extension"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static string GetGymBranchImageName(string extension, DateTime? now = null)
        => GenericImageName("GymBranch", extension, now);

    /// <summary>
    /// Formato de Nombre de imagenes de procesos de seguimiento
    /// </summary>
    /// <param name="extension"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static string GetProcessTrackingImageName(string extension, DateTime? now = null)
        => GenericImageName("ProcessTracking", extension, now);

    /// <summary>
    /// Formato de Nombre de imagenes de equipamientos
    /// </summary>
    /// <param name="extension"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static string GetEquipmentImageName(string extension, DateTime? now = null)
        => GenericImageName("Equipment", extension, now);

}