namespace Common.Utils.Extensions;
/// <summary>
/// Extensión
/// </summary>
public static class DecimalExtension
{
    /// <summary>
    /// Calcula el porcentaje de diferencia entre dos valores
    /// </summary>
    /// <param name="value1">Valor 1</param>
    /// <param name="value2">Valor 2</param>
    /// <returns>Porcentaje de diferencia</returns>
    public static decimal? CalculatePercentageDifference(this decimal? value1, decimal? value2, short round = 2)
    {
        if (!value1.HasValue || !value2.HasValue)
            return null;
        return value2 == 0 ? 0 : Math.Round((value1.Value - value2.Value) / value2.Value * 100, round);
    }

    /// <summary>       
    /// Calcula el porcentaje de diferencia entre dos valores
    /// </summary>
    /// <param name="value1">Valor 1</param>
    /// <param name="value2">Valor 2</param>
    /// <returns>Porcentaje de diferencia</returns>
    public static decimal CalculatePercentageDifference(this decimal value1, decimal value2, short round = 2)
    {
        if (value2 == 0)
            return 0;
        return Math.Round((value1 - value2) / value2 * 100, round);
    }

    /// <summary>
    /// Redondea un decimal
    /// </summary>
    /// <param name="value">Valor</param>
    /// <param name="round">Decimales</param>
    /// <returns>Decimal redondeado</returns>
    public static decimal? Round(this decimal? value, short round = 2)
     => value.HasValue ? Math.Round(value.Value, round) : null;

    /// <summary>
    /// Redondea un decimal
    /// </summary>
    /// <param name="value">Valor</param>
    /// <param name="round">Decimales</param>
    /// <returns>Decimal redondeado</returns>
    public static decimal Round(this decimal value, short round = 2)
    => Math.Round(value, round);
}