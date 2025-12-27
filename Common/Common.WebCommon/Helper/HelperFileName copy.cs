namespace Common.WebCommon.Helper;

/// <summary>
/// Helper para paths de archivos
/// </summary>
public static class HelperPathName
{
    /// <summary>
    ///  Formato de Path de imagenes de sucursales de gimnasio
    /// </summary>
    /// <param name="basePath"></param>
    /// <param name="gymBranchId"></param>
    /// <returns></returns>
    public static string GetGymBranchPathName(string basePath, int gymBranchId) => $"{basePath}/{gymBranchId}";

}