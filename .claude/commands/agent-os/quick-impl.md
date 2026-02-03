# Quick Impl

Ejecuta implementaciones rápidas de funcionalidades o cambios pequeños, aplicando estándares automáticamente y documentando el resultado.

## Uso

```
/quick-impl <descripción de la funcionalidad>
```

Ejemplos:
```
/quick-impl agregar campo email al response de GetUser
/quick-impl crear endpoint para listar membresías por sucursal
/quick-impl agregar validación de fecha en CreateReservation
```

## Important Guidelines

- **Ejecución directa** — Este comando NO requiere plan mode, ejecuta inmediatamente
- **Estándares automáticos** — Detecta y aplica estándares relevantes sin preguntar
- **Documentación ligera** — Genera un archivo de resultado breve pero útil
- **Para cambios pequeños** — Usa `/shape-spec` para funcionalidades complejas

## Process

### Step 1: Parse Input

Si se proporcionó una descripción como argumento, úsala directamente.

Si NO se proporcionó argumento, usa AskUserQuestion:

```
¿Qué funcionalidad o cambio necesitas implementar?

(Describe brevemente: "agregar campo X al response Y", "crear endpoint para Z", etc.)
```

### Step 2: Classify Work Type

Analiza la descripción para clasificar el tipo de trabajo:

| Palabras clave | Tipo de trabajo | Estándares a cargar |
|----------------|-----------------|---------------------|
| endpoint, api, controller, route | API | api/*, backend/handler-structure |
| response, dto, model | Response/DTO | backend/response-models, api/response-envelope |
| request, validación, input | Request | backend/request-models |
| handler, command, query | Handler | backend/handler-structure, backend/response-pattern |
| repository, database, query | Data Access | backend/repository-pattern |
| error, exception | Error Handling | backend/error-handling |

Si no se puede clasificar claramente, carga los estándares más comunes:
- `api/response-envelope`
- `backend/handler-structure`
- `backend/response-pattern`

### Step 3: Load Standards Silently

Lee `agent-os/standards/index.yml` y carga los archivos de estándares relevantes según la clasificación del Step 2.

**NO preguntes al usuario** — carga los estándares automáticamente y menciónalos brevemente:

```
Aplicando estándares: api/response-envelope, backend/handler-structure
```

Lee el contenido completo de cada estándar para tenerlo en contexto.

### Step 4: Quick Context Scan

Haz una exploración rápida del código relacionado:

1. Si la descripción menciona un archivo/clase específica → léelo directamente
2. Si menciona un feature existente → busca archivos relacionados con Glob/Grep
3. Si es algo nuevo → busca implementaciones similares como referencia

Limita esta exploración a máximo 3-5 archivos relevantes. No sobre-investigues.

### Step 5: Execute Implementation

Implementa la funcionalidad directamente siguiendo los estándares cargados.

Durante la implementación:
- Sigue los patrones de los estándares
- Mantén consistencia con el código existente
- Haz commits atómicos si el usuario lo solicita

### Step 6: Generate Execution Record

Al terminar, crea un archivo de registro en `agent-os/executions/`.

**Nombre del archivo:**
```
YYYY-MM-DD-HHMM-{slug}.md
```

Donde:
- Fecha/hora es el timestamp actual
- Slug deriva de la descripción (lowercase, guiones, max 30 chars)

Ejemplo: `2026-02-01-1430-add-membership-plans-response.md`

**Contenido del archivo:**

```markdown
# {Título breve de la funcionalidad}

**Fecha:** {YYYY-MM-DD HH:MM}
**Tipo:** {API | Response | Handler | Repository | etc.}

## Descripción

{Qué se implementó, en 1-2 oraciones}

## Cambios Realizados

| Archivo | Cambio |
|---------|--------|
| `path/to/file.cs` | {Descripción breve del cambio} |
| `path/to/other.cs` | {Descripción breve del cambio} |

## Estándares Aplicados

- **{standard-name}** — {cómo se aplicó}

## Notas

{Cualquier decisión importante o consideración para el futuro}
```

### Step 7: Summary

Presenta un resumen al usuario:

```
Implementación completada.

**Cambios:**
- {lista de archivos modificados}

**Registro guardado en:** agent-os/executions/{filename}.md

¿Necesitas ajustes o hay algo más que implementar?
```

## Output Structure

```
agent-os/executions/
└── YYYY-MM-DD-HHMM-{slug}.md
```

Si la carpeta `agent-os/executions/` no existe, créala.

## Diferencias con /shape-spec

| Aspecto | /quick-impl | /shape-spec |
|---------|-------------|-------------|
| Requiere plan mode | No | Sí |
| Preguntas al usuario | Mínimas | Varias (shaping) |
| Estándares | Auto-detectados | Confirmados por usuario |
| Documentación | Registro post-ejecución | Spec pre-ejecución |
| Ideal para | Cambios pequeños, features simples | Features complejas, decisiones de diseño |

## Tips

- **Usa descripciones claras** — Mientras más específica la descripción, mejor el resultado
- **Menciona archivos si los conoces** — "agregar campo X en UserResponse.cs" es mejor que "agregar campo X"
- **Para trabajo complejo** — Usa `/shape-spec` si necesitas planificar o hay decisiones de diseño importantes
- **Revisa el registro** — Los archivos en `executions/` son útiles para tracking y auditoría
