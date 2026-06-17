using System.Globalization;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Enums;

namespace LogicApi.BusinessLogic.ProfileHandler;

/// <summary>
/// Clase base para handlers de perfil
/// </summary>
public abstract class ProfileBase<TRequest, TResponse>(
    ILogger<ProfileBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Resultado del cálculo de macros
    /// </summary>
    protected sealed record ProfileMacroResult(short Protein, short Carbohydrates, short Fats);

    /// <summary>
    /// Valida campos comunes del perfil
    /// </summary>
    protected static void ValidateProfileFields(
        string name,
        ProfileType type,
        decimal height,
        byte estimatedWeeks,
        int physicalActivityCatalogId,
        int progressRateCatalogId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new CustomException((int)MessagesCodesError.SystemError, "El nombre del perfil es obligatorio");

        if (name.Length > 50)
            throw new CustomException((int)MessagesCodesError.SystemError, "El nombre del perfil no puede exceder 50 caracteres");

        if (!Enum.IsDefined(type))
            throw new CustomException((int)MessagesCodesError.SystemError, "El tipo de perfil no es válido");

        if (height <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "La altura debe ser mayor a cero");

        if (estimatedWeeks <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "Las semanas estimadas deben ser mayor a cero");

        if (physicalActivityCatalogId <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "El nivel de actividad física es obligatorio");

        if (progressRateCatalogId <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "El ritmo de progreso es obligatorio");
    }

    /// <summary>
    /// Valida macros nutricionales
    /// </summary>
    protected static void ValidateMacros(short protein, short carbohydrates, short fats)
    {
        if (protein <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "La proteína debe ser mayor a cero");

        if (carbohydrates <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "Los carbohidratos deben ser mayor a cero");

        if (fats <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "Las grasas deben ser mayor a cero");
    }

    /// <summary>
    /// Calcula macros nutricionales según datos del perfil y peso
    /// </summary>
    protected async Task<ProfileMacroResult> CalculateMacrosAsync(
        ProfileType type,
        decimal weight,
        decimal height,
        int physicalActivityCatalogId,
        int progressRateCatalogId)
    {
        if (weight <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "El peso debe ser mayor a cero");

        var person = await UnitOfWork.PersonRepository
            .GetByFirstOrDefaultAsync(
                where => where.Id == PersonId,
                includes => includes.GenderCatalog)
            .ConfigureAwait(false);

        if (person?.BirthDate == null)
            throw new CustomException((int)MessagesCodesError.SystemError, "La persona no tiene fecha de nacimiento registrada");

        if (person.GenderCatalog == null)
            throw new CustomException((int)MessagesCodesError.SystemError, "La persona no tiene género registrado");

        var activityFactor = await GetCatalogNumericValueAsync(
            physicalActivityCatalogId,
            CatalogCodes.PhysicalActivityLevel.GetEnumMember()).ConfigureAwait(false);
        var progressAdjustment = await GetCatalogNumericValueAsync(
            progressRateCatalogId,
            CatalogCodes.ProgressRate.GetEnumMember()).ConfigureAwait(false);

        var age = CalculateAge(person.BirthDate.Value);
        var isMale = person.GenderCatalog.Code.Equals(CatalogItemCodes.GenderMale, StringComparison.OrdinalIgnoreCase);
        var isFemale = person.GenderCatalog.Code.Equals(CatalogItemCodes.GenderFemale, StringComparison.OrdinalIgnoreCase);

        if (!isMale && !isFemale)
            throw new CustomException((int)MessagesCodesError.SystemError, "El género de la persona no es compatible para el cálculo");

        var bmr = CalculateBmr(weight, height, age, isMale);
        var tdee = bmr * activityFactor;
        var targetCalories = type == ProfileType.LoseWeight
            ? tdee - progressAdjustment
            : tdee + progressAdjustment;

        if (targetCalories <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "No fue posible calcular calorías objetivo con los datos ingresados");

        var protein = (short)Math.Round(weight * 2m, MidpointRounding.AwayFromZero);
        var fats = (short)Math.Round(targetCalories * 0.25m / 9m, MidpointRounding.AwayFromZero);
        var remainingCalories = targetCalories - (protein * 4m) - (fats * 9m);
        var carbohydrates = (short)Math.Max(0, (int)Math.Round(remainingCalories / 4m, MidpointRounding.AwayFromZero));

        if (protein <= 0 || carbohydrates <= 0 || fats <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "No fue posible calcular macros válidos con los datos ingresados");

        return new ProfileMacroResult(protein, carbohydrates, fats);
    }

    /// <summary>
    /// Desactiva perfiles activos previos de la persona
    /// </summary>
    protected async Task DeactivateActiveProfilesAsync()
    {
        var activeProfiles = await UnitOfWork.ProfileRepository
            .GetByAsync(where => where.PersonId == PersonId && where.IsActive)
            .ConfigureAwait(false);

        if (activeProfiles.Count == 0)
            return;

        foreach (var profile in activeProfiles)
            profile.IsActive = false;

        await UnitOfWork.ProfileRepository.UpdateRangeAsync(activeProfiles).ConfigureAwait(false);
    }

    /// <summary>
    /// Valida que los catálogos existan y pertenezcan al padre esperado
    /// </summary>
    protected Task ValidateCatalogReferencesAsync(int physicalActivityCatalogId, int progressRateCatalogId)
        => Task.WhenAll(
            GetCatalogNumericValueAsync(physicalActivityCatalogId, CatalogCodes.PhysicalActivityLevel.GetEnumMember()),
            GetCatalogNumericValueAsync(progressRateCatalogId, CatalogCodes.ProgressRate.GetEnumMember()));

    private async Task<decimal> GetCatalogNumericValueAsync(int catalogId, string parentCode)
    {
        var catalog = await UnitOfWork.CatalogRepository
            .GetByFirstOrDefaultAsync(
                where => where.Id == catalogId && where.Status,
                includes => includes.CatalogueFather)
            .ConfigureAwait(false);

        ValidateCatalog(catalog, parentCode);

        if (!decimal.TryParse(catalog.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out var numericValue) || numericValue <= 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "El catálogo seleccionado no tiene un valor numérico válido");

        return numericValue;
    }

    private static void ValidateCatalog(Catalog catalog, string parentCode)
    {
        if (catalog?.CatalogueFather == null
            || !catalog.CatalogueFather.Code.Equals(parentCode, StringComparison.OrdinalIgnoreCase))
            throw new CustomException((int)MessagesCodesError.SystemError, "El catálogo seleccionado no es válido");
    }

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.UtcNow.Date;
        var age = today.Year - birthDate.Date.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    private static decimal CalculateBmr(decimal weight, decimal height, int age, bool isMale)
    {
        var bmr = 10m * weight + 6.25m * height - 5m * age;
        return isMale ? bmr + 5m : bmr - 161m;
    }
}
