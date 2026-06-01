using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicApi.Model.Common;
using LogicApi.Model.Enum;
using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.Common.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicCommon.BusinessLogic;
using LogicCommon.Model.Request.File;
using PersistenceDb.Models.Core;
using PersistenceDb.Models.Enums;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Clase base para handlers de seguimiento de procesos
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class ProcessTrackingBase<TRequest, TResponse>(
    ILogger<ProcessTrackingBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Procesa las imágenes del seguimiento de proceso
    /// </summary>
    protected async Task ProcessProcessTrackingImageFiles(List<RequestEncodeFile> images, int processTrackingId, int registerUserId)
    {
        var fileBasePaths = await GetFileBasePathCacheInformationByPathCodeAsync(PathCode.ProcessTrackingImage).ConfigureAwait(false);
        var folderPath = HelperPathName.GetProcessTrackingPathName(fileBasePaths.FileDirectoryPath, processTrackingId);
        await ProcessImagesAsync(
                images,
                PathCode.ProcessTrackingImage,
                processCreateImagesAsync: async (images, response) =>
                    await UnitOfWork.ProcessTrackingImageRepository.AddRangeAsync([.. response.Items.Select(select => new ProcessTrackingImage
                {
                    ProcessTrackingId = processTrackingId,
                    FilePersistenceId = select.Id,
                    DateTimeRegister = Now,
                    UserIdRegister = registerUserId
                })]).ConfigureAwait(false),
                beforeDeleteImagesAsync: async (images) =>
                {
                    var processTrackingImageGuids = images.Select(select => select.Guid.Value);
                    var processTrackingImages = await UnitOfWork.ProcessTrackingImageRepository.GetGenericAsync(
                        select => new { select.ProcessTrackingId, select.FilePersistence.Guid },
                        where => processTrackingImageGuids.Contains(where.FilePersistence.Guid) && where.FilePersistence.State).ConfigureAwait(false);
                    if (processTrackingImages.Any(select => select.ProcessTrackingId != processTrackingId, out var processTrackingImageNotBelongToProcessTracking))
                        throw new CustomException((int)MessagesCodesError.SystemError, $"Las imágenes con guid {processTrackingImageNotBelongToProcessTracking.Select(select => select.Guid).Join(", ")} no pertenecen al seguimiento de proceso");
                },
                processDeleteImagesAsync: async (_, response) =>
                {
                    var responseIds = response.Items.Select(select => select.Id).ToList();
                    await UnitOfWork.ProcessTrackingImageRepository.DeleteAsync(where => responseIds.Contains(where.FilePersistenceId)).ConfigureAwait(false);
                },
                getFileExtension: (fileExtension) => HelperFileName.GetProcessTrackingImageName(fileExtension),
                folderPath: folderPath
            ).ConfigureAwait(false);
    }

    /// <summary>
    /// Carga el mapa de códigos de parámetros físicos activos a identificadores
    /// </summary>
    protected async Task<Dictionary<string, byte>> GetActivePhysicalParameterIdsByCodeAsync()
    {
        var parameters = await GetPhysicalParametersAsync().ConfigureAwait(false);
        return parameters
            .Where(parameter => parameter.IsActive)
            .ToDictionary(parameter => parameter.Code.ToUpperInvariant(), parameter => parameter.Id);
    }

    /// <summary>
    /// Obtiene el identificador de un parámetro físico por su código
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    protected async Task<PhysicalParameter[]> GetPhysicalParametersAsync()
    {
        return await AdministratorCache.TryGetOrSetAsync(CacheCodes.PHYSICAL_PARAMETERS, async () =>
          {
              return (await UnitOfWork.PhysicalParameterRepository.GetByAsync().ConfigureAwait(false)).ToArray();
          }).ConfigureAwait(false);
    }

    /// <summary>
    /// Convierte la solicitud a entidades de medición
    /// </summary>
    /// <param name="request"></param>
    /// <param name="processTracking"></param>
    /// <param name="requireWeightAndHeight"></param>
    /// <returns></returns>
    protected async Task<List<ProcessTrackingMeasurement>> MapToMeasurementEntitiesAsync(
        IProcessTrackingMeasurementsInput request,
        ProcessTracking processTracking,
        bool requireWeightAndHeight = false)
    {
        var inputs = request.Measurements ?? [];
        if (inputs.Count == 0)
            throw new CustomException((int)MessagesCodesError.SystemError, "Debe incluir al menos una medida");

        var parameterIdsByCode = await GetActivePhysicalParameterIdsByCodeAsync().ConfigureAwait(false);
        var imcCode = PhysicalParameterCode.Bmi.GetEnumMember();
        var weightCode = PhysicalParameterCode.Weight.GetEnumMember();
        var heightCode = PhysicalParameterCode.Height.GetEnumMember();
        var seenCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var measurements = new List<ProcessTrackingMeasurement>();

        foreach (var input in inputs)
        {
            var code = input.Code?.Trim().ToUpperInvariant();
            if (string.IsNullOrEmpty(code))
                throw new CustomException((int)MessagesCodesError.SystemError, "Código de medida inválido");

            if (!seenCodes.Add(code))
                throw new CustomException((int)MessagesCodesError.SystemError, $"Código de medida duplicado: {code}");

            if (code.Equals(imcCode, StringComparison.OrdinalIgnoreCase))
                throw new CustomException((int)MessagesCodesError.SystemError, "IMC es calculado y no puede enviarse");

            if (!parameterIdsByCode.TryGetValue(code, out var parameterId))
                throw new CustomException((int)MessagesCodesError.SystemError, $"Código de medida no válido: {code}");

            measurements.Add(new ProcessTrackingMeasurement
            {
                ProcessTrackingId = processTracking.Id,
                PhysicalParameterId = parameterId,
                Value = input.Value
            });
        }

        if (requireWeightAndHeight)
        {
            if (!seenCodes.Contains(weightCode))
                throw new CustomException((int)MessagesCodesError.SystemError, "PESO es obligatorio");
            if (!seenCodes.Contains(heightCode))
                throw new CustomException((int)MessagesCodesError.SystemError, "ALTURA es obligatoria");
        }

        return measurements;
    }

    public record PartialMeasurement(
        int ProcessTrackingId,
        string Code,
        string Name,
        decimal Value,
        DifferenceValueType DifferenceValueType,
        PhysicalParameterUnit MeasurementUnit,
        string IconCode);

    /// <summary>
    /// Obtiene valores de medidas agrupados por identificador de seguimiento de proceso
    /// </summary>
    protected async Task<Dictionary<int, PartialMeasurement[]>> GetMeasurementValuesByProcessTrackingIdsAsync(IEnumerable<int> processTrackingIds)
    {
        processTrackingIds ??= [];
        var rows = await UnitOfWork.ProcessTrackingMeasurementRepository.GetGenericAsync(
            select => new
            {
                select.ProcessTrackingId,
                select.PhysicalParameter.Code,
                select.Value,
                select.PhysicalParameter.Name,
                select.PhysicalParameter.DifferenceValueType,
                select.PhysicalParameter.MeasurementUnit,
                select.PhysicalParameter.IconCode,
            },
            where => processTrackingIds.Contains(where.ProcessTrackingId)).ConfigureAwait(false);
        var parameters = await GetPhysicalParametersAsync().ConfigureAwait(false);
        return rows
            .GroupBy(row => row.ProcessTrackingId)
            .ToDictionary(
                group => group.Key,
                group =>
                {
                    var partialMeasurements = group.Select(row => new PartialMeasurement(
                        row.ProcessTrackingId,
                        row.Code,
                        row.Name,
                        row.Value,
                        (DifferenceValueType)row.DifferenceValueType,
                        row.MeasurementUnit,
                        row.IconCode)).ToList();

                    var weightCode = PhysicalParameterCode.Weight.GetEnumMember();
                    var heightCode = PhysicalParameterCode.Height.GetEnumMember();
                    var height = partialMeasurements.FirstOrDefault(select => select.Code.Equals(heightCode, StringComparison.OrdinalIgnoreCase));
                    var weight = partialMeasurements.FirstOrDefault(select => select.Code.Equals(weightCode, StringComparison.OrdinalIgnoreCase));
                    if (height != null && weight != null)
                    {
                        var bmi = BusinessLogicUtils.CalculateBmi(weight.Value, height.Value);
                        var bmiParameter = parameters.First(select => select.Code == PhysicalParameterCode.Bmi.GetEnumMember());
                        partialMeasurements.Add(new PartialMeasurement(
                            group.Key,
                            bmiParameter.Code,
                            bmiParameter.Name,
                            bmi,
                            (DifferenceValueType)bmiParameter.DifferenceValueType,
                            bmiParameter.MeasurementUnit,
                            bmiParameter.IconCode));
                    }
                    return partialMeasurements.ToArray();
                });
    }

    /// <summary>
    /// Obtiene la diferencia entre dos listas de medidas parciales
    /// </summary>
    /// <param name="partialMeasurements"></param>
    /// <param name="partialMeasurementsToCompare"></param>
    /// <returns></returns>
    public List<StatisticComparisonModel> CalculatePartialMeasurementsDifference(
        IEnumerable<PartialMeasurement> partialMeasurements,
        IEnumerable<PartialMeasurement> partialMeasurementsToCompare)
    {
        var codes = partialMeasurements.Select(select => select.Code)
            .Union(partialMeasurementsToCompare.Select(select => select.Code))
            .Distinct(StringComparer.OrdinalIgnoreCase);
        return [.. codes.Select(code =>
        {
            var partialMeasurement = partialMeasurements.FirstOrDefault(select => select.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            var partialMeasurementToCompare = partialMeasurementsToCompare.FirstOrDefault(select => select.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            return new StatisticComparisonModel
            {
                Code = code,
                Label = partialMeasurement?.Name ?? partialMeasurementToCompare?.Name ?? "-",
                Unit = partialMeasurement?.MeasurementUnit.GetEnumMember() ?? partialMeasurementToCompare?.MeasurementUnit.GetEnumMember() ?? "-",
                Value = partialMeasurement?.Value.Round(2),
                PreviousValue = partialMeasurementToCompare?.Value.Round(2),
                DifferenceValueType = partialMeasurement?.DifferenceValueType ?? partialMeasurementToCompare?.DifferenceValueType ?? DifferenceValueType.Positive,
                Icon = partialMeasurement?.IconCode ?? partialMeasurementToCompare?.IconCode
            };
        })];
    }

    /// <summary>
    /// Obtiene la diferencia entre dos listas de medidas parciales
    /// </summary>
    /// <param name="processTrackingIds"></param>
    /// <returns></returns>
    protected async Task<List<StatisticComparisonModel>> GetMeasurementsDifferenceAsync(List<int> processTrackingIds)
    {
        var measurements = await GetMeasurementValuesByProcessTrackingIdsAsync(processTrackingIds).ConfigureAwait(false);
        var current = measurements[processTrackingIds[0]];
        var previous = measurements.Count > 1 ? measurements[processTrackingIds[1]] : [];
        return CalculatePartialMeasurementsDifference(current, previous);
    }
}
