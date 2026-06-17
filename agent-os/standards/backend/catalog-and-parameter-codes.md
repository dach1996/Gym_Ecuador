# Códigos de catálogo, ítems y parámetros

Nunca hardcodear códigos de BD (`"GENERO"`, `"RITMO_LENTO"`, `"MAX_COUNT_REFRESH_TOKEN"`) en handlers, controllers ni clases base de negocio. Usar las clases centralizadas del proyecto.

## Catálogos padre (CATALOGO raíz)

**Ubicación:** [`Common/Common.WebCommon/Models/Enum/CatalogCodes.cs`](Common/Common.WebCommon/Models/Enum/CatalogCodes.cs)

- Enum con `[EnumMember(Value = "CODIGO_BD")]`
- Un valor por catálogo padre (`GENERO`, `NIVEL_ACTIVIDAD_FISICA`, etc.)
- Obtener el string con `.GetEnumMember()` (extensión en `Common.Utils.Extensions`)

```csharp
CatalogCodes.PhysicalActivityLevel.GetEnumMember() // "NIVEL_ACTIVIDAD_FISICA"
```

Al agregar un catálogo padre nuevo:

1. Insertar en BD (`Script.Catalogs.sql` o script dedicado)
2. Agregar entrada en `CatalogCodes`
3. Si aplica en bundle inicial, actualizar `GetInitialCataloguesHandler`

## Ítems de catálogo (CATALOGO hijo)

**Ubicación:** [`Common/Common.WebCommon/Models/Constants/CatalogItemCodes.cs`](Common/Common.WebCommon/Models/Constants/CatalogItemCodes.cs)

- Clase estática con `public const string`
- **Agrupar por `#region`** (un region por catálogo padre: Gender, PhysicalActivityLevel, ProgressRate, etc.)
- Nombre de constante descriptivo en inglés; valor = `CAT_CODIGO` exacto de BD

```csharp
#region ProgressRate

public const string ProgressSlow = "RITMO_LENTO";

#endregion
```

Uso en código:

```csharp
person.GenderCatalog.Code.Equals(CatalogItemCodes.GenderMale, StringComparison.OrdinalIgnoreCase)
```

Al agregar ítems hijos:

1. Insertar en BD con `PROC_INSERT_OR_UPDATE_CATALOG`
2. Agregar constantes en el `#region` correspondiente de `CatalogItemCodes`

## Parámetros de aplicación (ADMINISTRACION.PARAMETRO)

**Ubicación:** [`Common/Common.Utils/ConstansCodes/ParameterCodes.cs`](Common/Common.Utils/ConstansCodes/ParameterCodes.cs) (`ParametersCodes`)

- Clase estática `ParametersCodes` con `public const string`
- Agrupar por `#region` si hay muchos parámetros del mismo dominio
- Valor = `PAR_CODIGO` de BD

```csharp
await GetIntParameterAsync(ParametersCodes.MaxAttemptsLoginFailed);
```

## Reglas

| Tipo | Clase | Prohibido en handlers |
|------|--------|------------------------|
| Catálogo padre | `CatalogCodes` + `GetEnumMember()` | `"NIVEL_ACTIVIDAD_FISICA"` inline |
| Ítem de catálogo | `CatalogItemCodes` | `"RITMO_LENTO"` inline |
| Parámetro BD | `ParametersCodes` | `"MAX_COUNT_REFRESH_TOKEN"` inline |

## Ejemplo correcto (handler)

```csharp
var activityFactor = await GetCatalogNumericValueAsync(
    physicalActivityCatalogId,
    CatalogCodes.PhysicalActivityLevel.GetEnumMember()).ConfigureAwait(false);

var isMale = person.GenderCatalog.Code.Equals(
    CatalogItemCodes.GenderMale,
    StringComparison.OrdinalIgnoreCase);
```

## Ejemplo incorrecto

```csharp
// NO hacer esto en ProfileBase, handlers, etc.
protected const string ProgressRateCatalogParentCode = "RITMO_PROGRESO";
if (person.GenderCatalog.Code == "GENERO_MASCULINO")
```
