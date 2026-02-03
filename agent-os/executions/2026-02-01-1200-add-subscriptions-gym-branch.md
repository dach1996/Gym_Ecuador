# Agregar Suscripciones a GetGymBranchByGuid

**Fecha:** 2026-02-01 12:00
**Tipo:** Response/DTO + Handler

## Descripción

Se agregó el listado de suscripciones con sus características (incluidas/excluidas) al response de obtener información de sucursal en GatewayApi. Los datos son mock por ahora ya que no existen en la base de datos.

## Cambios Realizados

| Archivo | Cambio |
|---------|--------|
| `LogicApi/LogicApi.Model/Response/GymBranch/GetGymBranchByGuidResponse.cs` | Agregados modelos `GymBranchSubscriptionItem`, `SubscriptionFeatureItem` y enum `SubscriptionFeatureType`. Agregada propiedad `Subscriptions` a `GymBranchDetail`. |
| `LogicApi/LogicApi.BusinessLogic/GymBranchHandler/GetGymBranchByGuidHandler.cs` | Agregada data mock con 3 planes de suscripción (Básico, Premium, VIP) con sus características. |

## Estándares Aplicados

- **api/response-envelope** — Respuesta mantiene estructura `GenericResponse<T>`
- **backend/response-models** — Modelos siguen patrón de clases con propiedades documentadas
- **backend/handler-structure** — Handler mantiene patrón `ExecuteHandlerAsync`

## Notas

- Los datos de suscripciones son mock y están hardcodeados en el handler
- Cuando se implemente la base de datos, se deberá crear:
  - Tabla de suscripciones (Subscription/Plan)
  - Tabla de características (SubscriptionFeature)
  - Relación muchos a muchos entre Plan y Feature con tipo (incluido/excluido)
- El enum `SubscriptionFeatureType` tiene valores `Included = 1` y `Excluded = 2`
