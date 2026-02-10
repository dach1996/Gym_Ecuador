# Quick Create (Agent OS)

Crea algo nuevo (servicio, handler, endpoint) siguiendo estándares Agent OS **sin** shape-spec ni planeamiento. Carga los estándares necesarios y muestra el checklist; luego implementa.

## Uso

```
/agent-os:quick-create <tipo> [descripción]
```

**Tipos:** `endpoint` | `handler` | `servicio` | `request` | `response` | `consulta`

**Ejemplos:**
```
/agent-os:quick-create handler "GetPlansByBranchGuid"
/agent-os:quick-create endpoint "GET planes por sucursal"
/agent-os:quick-create servicio "enviar recordatorios de pago"
/agent-os:quick-create request "CreatePlanRequest con validaciones"
```

Si no se pasa descripción, preguntar brevemente: *"¿Qué quieres crear exactamente? (ej. nombre del handler o propósito del servicio)"*.

## Proceso

### 1. Determinar tipo y estándares

Según `<tipo>`:

| tipo       | Estándares a cargar |
|-----------|----------------------|
| endpoint  | api/controller-pattern, api/response-envelope, backend/handler-structure, backend/response-pattern |
| handler   | backend/handler-structure, backend/repository-pattern, backend/response-pattern, backend/error-handling |
| servicio  | (igual que handler en este proyecto: lógica en handlers) → tratar como handler |
| request   | backend/request-models |
| response  | backend/response-models, api/response-envelope, backend/response-pattern |
| consulta  | backend/repository-pattern |

Para `servicio`: aclarar que en este proyecto la lógica va en **handlers**; si el usuario quiere “un servicio” se interpreta como nuevo handler (y opcionalmente endpoint). Cargar estándares de handler (+ endpoint si va a exponerse por API).

### 2. Cargar estándares

Leer de `agent-os/standards/` los archivos indicados (por ejemplo `agent-os/standards/api/controller-pattern.md`) e incluirlos en el contexto. Indicar en una línea:

```
Estándares aplicados: [lista de nombres]
```

### 3. Mostrar checklist mínimo

Mostrar solo los ítems del checklist que aplican al tipo (usar la misma lista que en `.cursor/rules/agent-os-quick.mdc`):

- **endpoint:** controller base, Mediator.Send, Success(), ConfigureAwait.
- **handler:** herencia Base, ExecuteHandlerAsync, UnitOfWork, sin Include (proyección), UserMessage/ShowMessage, ConfigureAwait.
- **request/response:** interfaces base, UserMessage/ShowMessage si aplica, validaciones.
- **consulta:** GetGenericAsync / GetFirstOrDefaultGenericAsync con proyección, sin Include, ConfigureAwait.

### 4. Implementar

- Buscar en el código ejemplos similares (mismo tipo: otro handler, otro controller, otro request).
- Generar los archivos/cambios necesarios siguiendo los estándares y el checklist.
- No pedir confirmación paso a paso; implementar de una vez salvo que falte información crítica (ej. nombre de entidad o de operación).

### 5. Resumen final

- Listar archivos creados o modificados.
- Opcional: indicar que la regla `@agent-os-quick` se puede usar en otros chats para el mismo flujo.

## Reglas

- **No** entrar en plan mode ni hacer shaping largo.
- **No** preguntar por cosas que se puedan inferir (ej. nombre del proyecto o carpeta de handlers si ya existe convención).
- **Sí** preguntar si falta el nombre de la entidad, operación o contrato (ej. “¿Qué entidad es? ¿Routine o BranchPlan?”).
- Si la descripción es ambigua, elegir la interpretación más simple y seguir (ej. “crear servicio X” → un handler + opcionalmente endpoint).

## Relación con otros comandos

| Comando          | Uso |
|------------------|-----|
| `/agent-os:quick-create` | Crear algo nuevo rápido con estándares (este comando). |
| `/agent-os:quick-impl`   | Implementar un cambio o funcionalidad ya descrita (ej. “agregar campo X al response Y”). |
| `/agent-os:shape-spec`   | Diseñar una funcionalidad compleja con spec y decisiones de diseño. |
| `/agent-os:inject-standards` | Solo inyectar estándares en el contexto, sin implementar. |

Cuando el usuario pida “crear un nuevo servicio con algún motivo”, usar este comando (`quick-create`) o la regla `@agent-os-quick` para aplicar estándares sin tanto planeamiento.
