using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicApi.Model.Common;
using LogicApi.Model.Enum;
using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.Common.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicCommon.Model.Request.File;
using PersistenceDb.Models.Core;

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
        return parameters.ToDictionary(parameter => parameter.Code, parameter => parameter.Id);
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
    /// <param name="newProcessTracking"></param>
    /// <returns></returns>
    protected async Task<List<ProcessTrackingMeasurement>> MapToMeasurementEntitiesAsync(IProcessTrackingMeasurementsInput request, ProcessTracking newProcessTracking)
    {
        var parameterIdsByCode = await GetActivePhysicalParameterIdsByCodeAsync().ConfigureAwait(false);
        var measurements = new (byte?, decimal?)[]
        {
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.Weight.GetEnumMember()), request.Weight),
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.Height.GetEnumMember()), request.Height),
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.BodyFatPercentage.GetEnumMember()), request.BodyFatPercentage),
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.MuscleMassPercentage.GetEnumMember()), request.MuscleMassPercentage),
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.ChestMeasurement.GetEnumMember()), request.ChestMeasurement),
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.WaistMeasurement.GetEnumMember()), request.WaistMeasurement),
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.HipMeasurement.GetEnumMember()), request.HipMeasurement),
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.ArmRightMeasurement.GetEnumMember()), request.ArmRightMeasurement),
                    (parameterIdsByCode.FirstValueOrDefault(PhysicalParameterCode.ThighRightMeasurement.GetEnumMember()), request.ThighRightMeasurement),
        }.Where(select => select.Item1.HasValue && select.Item2.HasValue)
        .Select(select => new ProcessTrackingMeasurement
        {
            ProcessTrackingId = newProcessTracking.Id,
            PhysicalParameterId = select.Item1.Value,
            Value = select.Item2.Value
        }).ToList();
        return measurements;
    }

    /// <summary>
    /// Mapa de códigos de parámetros físicos a tipos de diferencia
    /// </summary>
    /// <returns></returns>
    private static readonly Dictionary<PhysicalParameterCode, DifferenceValueType> PhysicalParameterCodeToDifferenceValueType = new()
    {
        { PhysicalParameterCode.Weight, DifferenceValueType.Positive },
        { PhysicalParameterCode.Height, DifferenceValueType.Positive },
        { PhysicalParameterCode.BodyFatPercentage, DifferenceValueType.Positive },
        { PhysicalParameterCode.MuscleMassPercentage, DifferenceValueType.Positive },
        { PhysicalParameterCode.ChestMeasurement, DifferenceValueType.Positive },
        { PhysicalParameterCode.WaistMeasurement, DifferenceValueType.Positive },
    };


    public record PartialMeasurement(
        int ProcessTrackingId,
        PhysicalParameterCode? Code,
        string Name,
        decimal Value,
        DifferenceValueType DifferenceValueType);
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
                select.PhysicalParameter.Name
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
                        row.Code?.TryToEnumFromMember<PhysicalParameterCode>(),
                        row.Name,
                        row.Value,
                        row.Code?.TryToEnumFromMember<PhysicalParameterCode>() != null ? PhysicalParameterCodeToDifferenceValueType.FirstValueOrDefault(row.Code.TryToEnumFromMember<PhysicalParameterCode>().Value, DifferenceValueType.Positive) : DifferenceValueType.Positive
                        ))
                        .Where(select => select.Code.HasValue)
                        .ToList();
                    var height = partialMeasurements.FirstOrDefault(select => select.Code.Value == PhysicalParameterCode.Height);
                    var weight = partialMeasurements.FirstOrDefault(select => select.Code.Value == PhysicalParameterCode.Weight);
                    var bmi = weight.Value / (height.Value * height.Value);
                    var bmiParameter = parameters.FirstOrDefault(select => select.Code == PhysicalParameterCode.Bmi.GetEnumMember());
                    partialMeasurements.Add(new PartialMeasurement(
                        group.Key,
                        PhysicalParameterCode.Bmi,
                        bmiParameter.Name,
                        bmi,
                        DifferenceValueType.Positive
                    ));
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
        var codes = partialMeasurements.Select(select => select.Code.Value).Union(partialMeasurementsToCompare.Select(select => select.Code.Value)).Distinct();
        return [.. codes.Select(code => new StatisticComparisonModel
        {
            Code = code.GetEnumMember(),
            Label = code.GetEnumMember(),
            Value = partialMeasurements.FirstOrDefault(select => select.Code.Value == code)?.Value,
            PreviousValue = partialMeasurementsToCompare.FirstOrDefault(select => select.Code.Value == code)?.Value,
            DifferenceValueType = PhysicalParameterCodeToDifferenceValueType.FirstValueOrDefault(code, DifferenceValueType.Positive)
        })];
    }

    /// <summary>
    /// Aplica las medidas parciales al detalle del seguimiento de proceso
    /// </summary>
    /// <param name="measurements"></param>
    /// <param name="partialMeasurements"></param>
    protected static void ApplyMeasurementsToDetail(IProcessTrackingMeasurement measurements, PartialMeasurement[] partialMeasurements)
    {
        var groupedMeasurements = partialMeasurements.GroupBy(m => m.Code.Value).ToDictionary(g => g.Key, g => g.First().Value);
        measurements.Weight = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.Weight);
        measurements.Height = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.Height);
        measurements.BodyFatPercentage = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.BodyFatPercentage);
        measurements.MuscleMassPercentage = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.MuscleMassPercentage);
        measurements.ChestMeasurement = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.ChestMeasurement);
        measurements.WaistMeasurement = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.WaistMeasurement);
        measurements.HipMeasurement = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.HipMeasurement);
        measurements.ArmRightMeasurement = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.ArmRightMeasurement);
        measurements.ThighRightMeasurement = groupedMeasurements.FirstValueOrDefault(PhysicalParameterCode.ThighRightMeasurement);
    }
}
