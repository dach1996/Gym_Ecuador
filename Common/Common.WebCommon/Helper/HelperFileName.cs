namespace Common.WebCommon.Helper
{
    /// <summary>
    /// Helper para nombres de archivos
    /// </summary>
    public static class HelperFileName
    {
        /// <summary>
        /// Formato de Nombre de imagenes de usuario
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static string GetUserImageName(string extension, DateTime? now = null)
        {
            var guid = $"{Guid.NewGuid():N}";
            var last5 = guid.Length >= 5 ? guid[^5..] : guid;
            return $"UserImage_{now ?? DateTime.Now:yyyyMMddHHmmss}{last5}.{extension}";
        }

    }
}