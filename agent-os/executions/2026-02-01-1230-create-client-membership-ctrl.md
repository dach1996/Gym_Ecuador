# Crear Controlador de Membresías de Cliente

**Fecha:** 2026-02-01 12:30
**Tipo:** API (Controller + Handler + Request + Response)

## Descripción

Se creó un nuevo controlador de membresías en GatewayCoreAPI que permite obtener el historial de membresías del usuario agrupado por sucursal de gimnasio, incluyendo la membresía activa actual de cada sucursal.

## Cambios Realizados

| Archivo | Cambio |
|---------|--------|
| `LogicApi/LogicApi.Model/Response/ClientMembership/GetMyMembershipsResponse.cs` | Creado modelo de respuesta con `GymBranchMembershipGroup`, `MembershipHistoryItem` y enum `MembershipStatus` |
| `LogicApi/LogicApi.Model/Request/ClientMembership/GetMyMembershipsRequest.cs` | Creado modelo de request |
| `LogicApi/LogicApi.BusinessLogic/ClientMembershipHandler/ClientMembershipBase.cs` | Creada clase base para handlers de membresía |
| `LogicApi/LogicApi.BusinessLogic/ClientMembershipHandler/GetMyMembershipsHandler.cs` | Creado handler que consulta membresías por PersonId |
| `GatewayCoreAPI/Controllers/V1/ClientMembershipController.cs` | Creado controlador con endpoint `GetMyMemberships` |
| `Common/Common.WebApi/Models/Enum/OperationApiName.cs` | Agregado `GetMyMemberships` al enum |

## Estándares Aplicados

- **api/controller-pattern** — Controller hereda de `SecurityControllerBase`, usa `Mediator.Send()` y `Success()`
- **api/response-envelope** — Response se envuelve automáticamente en `GenericResponse<T>`
- **backend/handler-structure** — Handler hereda de base, usa `ExecuteHandlerAsync`
- **backend/request-models** — Request implementa `IApiBaseRequest<TResponse>` con `CommonContextRequest`
- **backend/response-models** — Response implementa `IApiBaseResponse` con `UserMessage` y `ShowMessage`

## Estructura del Response

```json
{
  "gymBranchMemberships": [
    {
      "gymBranchGuid": "guid",
      "gymBranchName": "Sucursal Centro",
      "gymBranchImageUrl": "https://...",
      "activeMembership": {
        "membershipGuid": "guid",
        "planName": "Plan Premium",
        "planDescription": "Acceso completo",
        "planPrice": 49.99,
        "startDate": "2026-01-01",
        "endDate": "2026-02-01",
        "status": "Active"
      },
      "membershipHistory": [...]
    }
  ]
}
```

## Notas

- El endpoint está autenticado (hereda de `SecurityControllerBase`)
- Filtra por `PersonId` del contexto de usuario autenticado
- El estado de la membresía se calcula dinámicamente:
  - `Active`: si `IsActive = true`
  - `Expired`: si `IsActive = false` y `EndDate < DateTime.UtcNow`
  - `Cancelled`: si `IsActive = false` y no está expirado
- El historial está ordenado por fecha de inicio descendente (más reciente primero)
