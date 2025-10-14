using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Attributes;
using PersistenceDb.Models.Core;

namespace PersistenceDb.Models.Authentication;
/// <summary>
/// Tabla Usuario
/// </summary>
[Table(name: "USUARIO", Schema = "AUTENTICACION")]
public class User
{
    /// <summary>
    /// Id
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("USR_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    /// <value></value>
    [Required]
    [Column("USR_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    /// <value></value>
    [Required]
    [StringLength(100)]
    [Column("USR_USERNAME")]
    public string UserName { get; set; }

    /// <summary>
    /// Teléfono
    /// </summary>
    [StringLength(20)]
    [Column("USR_TELEFONO")]
    public string Phone { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [StringLength(100)]
    [Column("USR_CORREO")]
    public string Email { get; set; }

    /// <summary>
    /// Fecha de 1° Login
    /// </summary>
    [Column("USR_FECHA_PRIMER_LOGIN")]
    public DateTime? FirstLoginDate { get; set; }

    /// <summary>
    /// Imágen del usuario
    /// </summary>
    /// <value></value>
    [Column("ARG_ID_IMAGEN_USUARIO")]
    [ForeignKey(nameof(Image))]
    public int? ImagenId { get; set; }

    /// <summary>
    /// Idioma del usuario
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("USR_CODIGO_IDIOMA")]
    public string LanguageCode { get; set; }

    /// <summary>
    /// Intengo fallido de Logín
    /// </summary>
    [Column("USR_INTENTO_LOGIN_FALLIDO")]
    public int? TriedLoginFailed { get; set; }

    /// <summary>
    /// Fecha de último login fallido
    /// </summary>
    [Column("USR_FECHA_ULTIMO_INTENTO_LOGIN_FALLIDO")]
    public DateTime? DateTimeTriedLoginFailed { get; set; }

    /// <summary>
    /// Usuario Bloqueado
    /// </summary>
    [Column("USR_BLOQUEADO")]
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Id de Persona 
    /// </summary>
    /// <value></value>
    [ForeignKey(nameof(Person))]
    [Column("PNA_ID")]
    public int? PersonId { get; set; }

    /// <summary>
    /// Tiene registro completo
    /// </summary>
    [Column("USR_TIENE_REGISTRO_COMPLETO")]
    public bool HasCompleteRegistration { get; set; }

    /// <summary>
    /// Salt
    /// </summary>
    [Column("USR_SALT")]
    [EncryptColumn]
    public string Salt { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Column("USR_GUID")]
    [Required]
    public Guid Guid { get; set; }

    /// <summary>
    /// Forma de Registro
    /// </summary>
    /// <value></value>
    [NotMapped]
    public UserRegistrationForm ManualUserRegistrationForm
        => UserRegistrationForms.FirstOrDefault(first => first.UserTypeRegister == Enums.UserTypeRegister.Manual)
            ?? throw new DataException($"El tipo de registro: '{Enums.UserTypeRegister.Manual}' no se encontró");

    /// <summary>
    /// Forma de Registro
    /// </summary>
    /// <value></value>
    [NotMapped]
    public UserRegistrationForm GoogleUserRegistrationForm
        => UserRegistrationForms.FirstOrDefault(first => first.UserTypeRegister == Enums.UserTypeRegister.Google)
            ?? throw new DataException($"El tipo de registro: '{Enums.UserTypeRegister.Google}' no se encontró");

    /// <summary>
    /// Archivo
    /// </summary>
    public FilePersistence Image { get; set; }

    /// <summary>
    /// Lista de Usuarios relacinados con dispositivos
    /// </summary>
    /// <value></value>
    public ICollection<UserDevice> UserDevices { get; set; }

    /// <summary>
    /// Lista de compañeros
    /// </summary>
    /// <value></value>
    public ICollection<Companion> Companions { get; set; }

    /// <summary>
    /// Lista de tarjetas
    /// </summary>
    /// <value></value>
    public ICollection<Card> Cards { get; set; }

    /// <summary>
    /// Lista de Registros de Forma de Usuario
    /// </summary>
    /// <value></value>
    public ICollection<UserRegistrationForm> UserRegistrationForms { get; set; }

    /// <summary>
    /// Persona
    /// </summary>
    /// <value></value>
    public Person Person { get; set; }

    /// <summary>
    /// Lista de notificaciones enviadas al Usuario
    /// </summary>
    /// <value></value>
    public ICollection<NotificationPushUser> NotificationPushUsers { get; set; }

    /// <summary>
    /// Usuario de envío de notificación
    /// </summary>
    /// <value></value>
    public UserDevicePushToken UserDevicePushToken { get; set; }

}