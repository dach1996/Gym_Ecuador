---
name: agent-os
description: Orquesta planificación e implementación en el proyecto Gym usando agent-os. Verifica estándares, producto y patrones existentes antes de planear o ejecutar. Usar cuando el usuario invoque agent-os, pida implementar siguiendo estándares del proyecto, crear endpoints/handlers/servicios con convenciones, planear features, o mencione agent-os, estándares, shape-spec o quick-create.
---

# Agent OS — Planificador y Ejecutor

Skill principal del proyecto. **Nunca planifiques ni escribas código sin completar primero el pre-flight.**

Responde en **español**. Commits en **inglés** (`tipo: descripción`).

## Modos de operación

Detecta la intención del usuario y elige un modo:

| Modo | Cuándo | Entregable |
|------|--------|------------|
| **PLAN** | Feature nueva, alcance ambiguo, varias capas, o el usuario pide plan/spec | Plan estructurado + lista de archivos + estándares aplicables. **No escribir código** salvo que lo pida explícitamente |
| **EXECUTE** | Cambio acotado, endpoint/handler concreto, bug fix, o el usuario pide implementar | Código siguiendo estándares + build si aplica |
| **QUICK** | "Rápido", "solo el handler", cambio de 1–2 archivos | Igual que EXECUTE pero sin spec larga; usar checklist mínimo de `@agent-os-quick` |

Si no está claro: pregunta **PLAN o EXECUTE** en una línea.

---

## Pre-flight obligatorio (siempre)

Ejecutar **antes** de planear o codificar. No omitir pasos.

```
Pre-flight Agent OS:
- [ ] 1. Leer agent-os/standards/index.yml
- [ ] 2. Identificar estándares aplicables (ver standards-map.md)
- [ ] 3. Leer cada estándar aplicable completo
- [ ] 4. Leer agent-os/product/mission.md (contexto de negocio)
- [ ] 5. Consultar agent-os/product/roadmap.md si la feature es nueva
- [ ] 6. Consultar agent-os/product/tech-stack.md si hay dudas de stack/API
- [ ] 7. Buscar código similar existente (handler/controller del mismo dominio)
- [ ] 8. Verificar agent-os/executions/ por trabajos previos relacionados
```

**Regla:** Si un estándar contradice código legacy, **priorizar el estándar** salvo que el usuario indique mantener consistencia con legacy.

---

## Mapeo rápido tarea → estándares

Detalle en [standards-map.md](standards-map.md). Resumen:

| Tarea | Estándares mínimos |
|-------|-------------------|
| Endpoint nuevo | `api/controller-pattern`, `api/response-envelope`, `api/routing-conventions`, `backend/handler-structure`, `backend/response-pattern` |
| Handler nuevo | `backend/handler-structure`, `backend/repository-pattern`, `backend/error-handling`, `backend/response-pattern` |
| Request / Response | `backend/request-models`, `backend/response-models`, `api/response-envelope` |
| Consultas / repo | `backend/repository-pattern` |
| Errores de negocio | `backend/error-handling` |
| Cualquier cambio | `global/tech-stack` |

---

## Flujo PLAN

1. Completar pre-flight.
2. Definir alcance: API objetivo (`GatewayCoreAPI` vs `AdministratorApi`), entidad, operación CRUD/consulta.
3. Listar artefactos a crear/modificar:
   - Request, Response, Handler, Base (si no existe), Controller, `OperationApiName` / `OperationAdministratorName`, repositorio si aplica.
4. Señalar riesgos: auth, paginación, filtros, relaciones EF (proyección vs Include).
5. Entregar plan en este formato:

```markdown
## Plan Agent OS — [nombre feature]

### Contexto
[1–2 frases desde mission/roadmap]

### Estándares aplicados
- [lista desde index.yml]

### Archivos
| Acción | Ruta |
|--------|------|

### Endpoints
| Método | Ruta | Handler |

### Checklist de implementación
- [ ] ...

### Fuera de alcance
- ...
```

6. Esperar confirmación del usuario antes de EXECUTE (salvo que pidió implementar de inmediato).

---

## Flujo EXECUTE / QUICK

1. Completar pre-flight.
2. Aplicar checklist según tipo (ver abajo).
3. Implementar con **diff mínimo**; reutilizar bases y handlers del dominio.
4. Registrar `OperationApiName` / enum correspondiente si es endpoint nuevo.
5. **MediatR:** el handler debe existir en el ensamblado escaneado (`AddMediatrTypes`); no basta con Request/Controller.
6. Compilar el proyecto afectado (`dotnet build`).
7. Post-flight (ver abajo).

### Checklist endpoint + handler

- [ ] Controller: `SecurityControllerBase` (o base correcta), `Mediator.Send`, `Success()`
- [ ] Handler: `{Entity}Base<TRequest,TResponse>`, `ExecuteHandlerAsync(OperationName, ...)`
- [ ] Repositorio: solo `UnitOfWork.{Entity}Repository`
- [ ] Lecturas: `GetGenericAsync` / `GetPaginatorGenericAsync` con proyección — **sin `.Include()`**
- [ ] Updates: `GetByFirstOrDefaultAsync(where)` + mutación + respuesta
- [ ] Response: `UserMessage`, `ShowMessage` (true en CUD, false en consultas)
- [ ] `.ConfigureAwait(false)` en todo async
- [ ] XML docs en tipos públicos si el archivo vecino los usa

### Ubicaciones del proyecto

| Artefacto | Ruta |
|-----------|------|
| Controller móvil/core | `GatewayCoreAPI/Controllers/V1/` |
| Controller admin | `AdministratorApi/Controllers/` |
| Handler LogicApi | `LogicApi/LogicApi.BusinessLogic/{Entity}Handler/` |
| Handler Admin | `LogicAdministratorApi/LogicAdministratorApi.BusinessLogic/` |
| Request | `*/Model/Request/{Entity}/` |
| Response | `*/Model/Response/{Entity}/` |
| Repositorio | `Persistence/PersistenceDb/` |

---

## Post-flight (después de EXECUTE)

```
Post-flight Agent OS:
- [ ] Build sin errores
- [ ] Estándares del checklist cumplidos
- [ ] No archivos markdown/docs no solicitados
- [ ] (Opcional) Crear agent-os/executions/YYYY-MM-DD-HHmm-[slug].md
```

Plantilla de execution log:

```markdown
# [Título]

**Fecha:** YYYY-MM-DD HH:mm
**Tipo:** [API | Handler | Fix | ...]

## Descripción
[Qué y por qué]

## Cambios
| Archivo | Cambio |

## Estándares aplicados
- [ids desde index.yml]

## Notas
- ...
```

---

## Reglas de oro (no negociables)

1. **Estándares antes que código** — pre-flight siempre.
2. **Sin `.Include()`** — proyección en selectores del repositorio.
3. **Handlers registrados por MediatR** — clase concreta en el ensamblado correcto.
4. **Consistencia** — copiar estilo del handler/controller más cercano del mismo dominio.
5. **Alcance mínimo** — no refactorizar ni documentar de más.
6. **Estándar faltante** — proponer archivo nuevo en `agent-os/standards/` e actualizar `index.yml`.

---

## Relación con otras reglas del repo

- Regla global: `.cursor/rules/cursor.mdc`
- Implementación rápida: `.cursor/rules/agent-os-quick.mdc` (checklist condensado; **este skill tiene prioridad** en pre-flight completo)
- Ejemplos de ejecuciones previas: `agent-os/executions/*.md`

---

## Invocación

El usuario puede decir:

- `@agent-os planear [feature]`
- `@agent-os implementar [descripción]`
- `@agent-os` + tarea concreta

Siempre iniciar leyendo este skill y ejecutando **pre-flight** antes de responder con plan o código.
