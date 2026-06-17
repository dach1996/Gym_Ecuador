namespace Common.WebCommon.Models.Constants;

/// <summary>
/// Códigos de ítems hijos en ADMINISTRACION.CATALOGO (CAT_CODIGO)
/// </summary>
public static class CatalogItemCodes
{
    #region Gender

    /// <summary>
    /// Género masculino
    /// </summary>
    public const string GenderMale = "GENERO_MASCULINO";

    /// <summary>
    /// Género femenino
    /// </summary>
    public const string GenderFemale = "GENERO_FEMENINO";

    #endregion

    #region PhysicalActivityLevel

    /// <summary>
    /// Actividad sedentaria
    /// </summary>
    public const string ActivitySedentary = "ACTIVIDAD_SEDENTARIO";

    /// <summary>
    /// Actividad ligera (1-2 días ejercicio/semana)
    /// </summary>
    public const string ActivityLight = "ACTIVIDAD_LIGERO";

    /// <summary>
    /// Actividad moderada (3-5 días ejercicio/semana)
    /// </summary>
    public const string ActivityModerate = "ACTIVIDAD_MODERADO";

    /// <summary>
    /// Actividad intensa (6-7 días ejercicio/semana)
    /// </summary>
    public const string ActivityIntense = "ACTIVIDAD_INTENSO";

    /// <summary>
    /// Actividad muy intensa (atleta / trabajo físico)
    /// </summary>
    public const string ActivityVeryIntense = "ACTIVIDAD_MUY_INTENSO";

    #endregion

    #region ProgressRate

    /// <summary>
    /// Ritmo de progreso lento
    /// </summary>
    public const string ProgressSlow = "RITMO_LENTO";

    /// <summary>
    /// Ritmo de progreso moderado
    /// </summary>
    public const string ProgressModerate = "RITMO_MODERADO";

    /// <summary>
    /// Ritmo de progreso agresivo
    /// </summary>
    public const string ProgressAggressive = "RITMO_AGRESIVO";

    #endregion
}
