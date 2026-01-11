using Common.Utils;
using Common.Utils.ConstansCodes;
using Common.Utils.Extensions;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.Request.Administration;
using LogicCommon.Model.Response.Administration;
using Microsoft.AspNetCore.StaticAssets;
using PersistenceDb.Models.Enums;

namespace LogicCommon.BusinessLogic.AdministrationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetInitialCataloguesHandler(
    ILogger<GetInitialCataloguesHandler> logger,
    IPluginFactory pluginFactory) : AdministrationBase<GetInitialCataloguesCommonRequest, GetInitialCataloguesResponse>(
        logger,
        pluginFactory)
{

   /// <summary>
   /// Handler
   /// </summary>
   /// <param name="request"></param>
   /// <param name="cancellationToken"></param>
   /// <returns></returns>
   public override async Task<GetInitialCataloguesResponse> Handle(GetInitialCataloguesCommonRequest request, CancellationToken cancellationToken)
       => await ExecuteHandlerAsync(request, async ()
               => await AdministratorCache.TryGetOrSetAsync(CacheCodes.GET_INITIAL_CATALOGUES, async () =>
               {
                  var listCatalogCodes = new Dictionary<string, Dictionary<string, string>>();

                  var countries = (await UnitOfWork.CountryRepository.GetGenericAsync(
                      select => new { select.Code, select.Name }
                   ).ConfigureAwait(false)).ToDictionary(select => select.Code, select => select.Name);
                  listCatalogCodes.Add($"{CatalogsTypeItemsCodes.Nationality}", countries);
                  var typeIdentifications = (await UnitOfWork.TypeIdentificationRepository.GetGenericAsync(
                      select => new { select.Code, select.Name }
                   ).ConfigureAwait(false)).ToDictionary(select => select.Code, select => select.Name);
                  listCatalogCodes.Add($"{CatalogsTypeItemsCodes.DocumentType}", typeIdentifications);
                  listCatalogCodes.Add($"{CatalogsTypeItemsCodes.AdministratorRoleScope}", Util.GetDictionaryEnums<RoleScope>().ToDictionary(select => $"{select.Key}", select => select.Value));
                  var catalogs = (await UnitOfWork.CatalogRepository.GetGenericAsync(
                      select => new
                      {
                         FatherCode = select.CatalogueFather.Code,
                         select.Code,
                         select.CatalogLanguages.FirstOrDefault().Name
                      },
                      where => ParentCodes.Contains(where.CatalogueFather.Code)
                   ).ConfigureAwait(false))
                   .GroupBy(select => select.FatherCode)
                   .ToDictionary(select => GetCatalogByFatherCode[select.Key.ToEnumFromMember<CatalogCodes>()], select => select.ToDictionary(select => select.Code, select => select.Name));
                  foreach (var catalog in catalogs)
                     listCatalogCodes.Add($"{catalog.Key}", catalog.Value);
                  return new GetInitialCataloguesResponse(listCatalogCodes);
               }).ConfigureAwait(false)
           ).ConfigureAwait(false);

   private static readonly string[] ParentCodes = [.. new[] { CatalogCodes.Gender, CatalogCodes.EquipmentTypeGym, CatalogCodes.MuscleType }.Select(select => select.GetEnumMember())];

   /// <summary>
   /// Catálogos por código de padre
   /// </summary>
   /// <returns></returns>
   private static readonly Dictionary<CatalogCodes, CatalogsTypeItemsCodes> GetCatalogByFatherCode = new()
   {
      { CatalogCodes.Gender, CatalogsTypeItemsCodes.Gender },
      { CatalogCodes.EquipmentTypeGym, CatalogsTypeItemsCodes.EquipmentTypeGym },
      { CatalogCodes.MuscleType, CatalogsTypeItemsCodes.MuscleType },
   };
}

