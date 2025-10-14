namespace Common.WebApi.Models
{
    /// <summary>
    /// Clase modelo para devolver controladores con archivos
    /// </summary>
    public class FileResultResponse
    {
        /// <summary>
        /// Contenido 
        /// </summary>
        /// <value></value>
        public byte[] Content { get; set; }

        /// <summary>
        /// Tipo de Archivo
        /// </summary>
        /// <value></value>
        public string ContentType { get; set; }

        /// <summary>
        /// Nombre de archivo descargado
        /// </summary>
        /// <value></value>
        public string FileDownloadName { get; set; }

    }
}
