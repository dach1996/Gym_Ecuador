# Mapa de estándares Agent OS

Índice canónico: `agent-os/standards/index.yml`

## Por capa

### API (`agent-os/standards/api/`)

| Archivo | Aplica cuando |
|---------|---------------|
| `controller-pattern.md` | Nuevo o modificado controller, acciones MediatR |
| `response-envelope.md` | Cualquier respuesta HTTP al cliente |
| `routing-conventions.md` | Rutas, versionado, nombres de acciones |

### Backend (`agent-os/standards/backend/`)

| Archivo | Aplica cuando |
|---------|---------------|
| `handler-structure.md` | Handlers, bases `{Entity}Base`, `ExecuteHandlerAsync` |
| `repository-pattern.md` | Consultas EF, paginación, updates, **prohibición de Include** |
| `request-models.md` | DTOs de entrada, `IApiBaseRequest`, contexto |
| `response-models.md` | DTOs de salida, paginación, items anidados |
| `response-pattern.md` | `UserMessage`, `ShowMessage` en handlers |
| `error-handling.md` | `CustomException`, validaciones de negocio |

### Global (`agent-os/standards/global/`)

| Archivo | Aplica cuando |
|---------|---------------|
| `tech-stack.md` | Dudas de stack, versiones, capas, MediatR/Autofac |

## Por tipo de tarea

| Tipo | Estándares (orden de lectura) |
|------|-------------------------------|
| Endpoint GET/POST/PUT/DELETE completo | controller-pattern → routing-conventions → handler-structure → request-models → response-models → response-pattern → response-envelope → repository-pattern → error-handling |
| Solo handler | handler-structure → repository-pattern → response-pattern → error-handling |
| Solo request/response | request-models → response-models → response-envelope |
| Fix en consulta | repository-pattern → handler-structure |
| Fix en controller | controller-pattern → response-envelope |
| Feature multi-API | tech-stack → todos los de backend + api relevantes |

## Producto (contexto, no convención de código)

| Archivo | Cuándo leer |
|---------|-------------|
| `agent-os/product/mission.md` | Siempre en pre-flight |
| `agent-os/product/roadmap.md` | Features nuevas o priorización |
| `agent-os/product/tech-stack.md` | Arquitectura, qué API tocar |

## Prioridad en conflictos

1. Estándar específico (`api/`, `backend/`) sobre `global/`
2. Estándar documentado sobre código legacy inconsistente
3. Código existente del **mismo dominio** sobre otro dominio distinto
