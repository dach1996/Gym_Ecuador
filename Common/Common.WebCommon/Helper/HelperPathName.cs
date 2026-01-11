
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

    /// <summary>
    ///  Formato de Path de imagenes de procesos de seguimiento
    /// </summary>
    /// <param name="fileDirectoryPath"></param>
    /// <param name="processTrackingId"></param>
    /// <returns></returns>
    public static string GetProcessTrackingPathName(string fileDirectoryPath, int processTrackingId) => $"{fileDirectoryPath}/{processTrackingId}";

    /// <summary>
    ///  Formato de Path de imagenes de equipamientos
    /// </summary>
    /// <param name="basePath"></param>
    /// <param name="equipmentId"></param>
    /// <returns></returns>
    public static string GetEquipmentPathName(string basePath, int equipmentId) => $"{basePath}/{equipmentId}";

    /// <summary>
    ///  Formato de Path de imagenes de ejercicios
    /// </summary>
    /// <param name="basePath"></param>
    /// <param name="exerciseId"></param>
    /// <returns></returns>
    public static string GetExercisePathName(string basePath, int exerciseId) => $"{basePath}/{exerciseId}";
}