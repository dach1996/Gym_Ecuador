namespace Common.Blob.Models.Configuration.Local;
/// <summary>
/// Implementaciones
/// </summary>
public class LocalBlobConfiguration
{

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Contraseña 
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Tipo de Autenticación
    /// </summary>
    public string AuthenticationType { get; set; }

    /// <summary>
    /// Disco Origen a Ejecutar Comando
    /// </summary>
    /// <value></value>
    public string DiskOrigin { get; set; }

    /// <summary>
    /// Cmd a Ejecutar
    /// </summary>
    /// <value></value>
    public string CmdExecute { get; set; }

    /// <summary>
    /// Código Permitido
    /// </summary>
    /// <value></value>
    public string CodeAllow { get; set; }
}

