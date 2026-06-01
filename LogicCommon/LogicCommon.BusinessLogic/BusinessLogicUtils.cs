namespace LogicCommon.BusinessLogic;
public static class BusinessLogicUtils
{
    /// <summary>
    /// Calcula el índice de masa corporal
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static decimal CalculateBmi(decimal weight, decimal height)
    {
        if (height <= 0)
            return 0;
        if (weight <= 0)
            return 0;
        height /= 100;
        return Math.Round(weight / (height * height), 2);
    }
}