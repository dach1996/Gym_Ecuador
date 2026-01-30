# Sync Messages

Analiza un handler o clase y sincroniza los códigos de error usados con el archivo JSON de mensajes.

## Uso

```
/sync-messages <ruta-del-handler>
```

### Ejemplos

```
/sync-messages LogicApi/LogicApi.BusinessLogic/AuthorizationHandler/Login/LoginHandler.cs
/sync-messages LogicApi/LogicApi.BusinessLogic/PersonHandler/VerifyDocumentWithAIHandler.cs
```

## Proceso

### Step 1: Validar el Argumento

Si no se proporciona argumento, mostrar ayuda:

```
Uso: /sync-messages <ruta-del-handler>

Ejemplos:
  /sync-messages LogicApi/LogicApi.BusinessLogic/AuthorizationHandler/Login/LoginHandler.cs
  /sync-messages LogicApi/LogicApi.BusinessLogic/PersonHandler/VerifyDocumentWithAIHandler.cs

Este comando analiza un handler, detecta los códigos de error usados en CustomException
y los agrega al archivo JSON de mensajes si no existen.
```

### Step 2: Leer y Analizar el Handler

Leer el archivo del handler y buscar todos los usos de `CustomException`. Patrones a detectar:

```csharp
// Patrón 1: Usando el enum directamente
throw new CustomException((int)MessagesCodesError.GymNotFound, "mensaje");

// Patrón 2: Usando el enum sin cast
throw new CustomException(MessagesCodesError.GymNotFound, "mensaje");

// Patrón 3: Variable con el código
var code = MessagesCodesError.GymNotFound;
throw new CustomException((int)code, "mensaje");
```

Extraer:
- El nombre del código del enum (ej: `GymNotFound`, `InvalidDocumentImage`)
- El mensaje usado en el throw (si está disponible)

### Step 3: Resolver los Códigos del Enum

Leer el archivo `Common/Common.Messages/MessagesCodesError.cs` para obtener:
- El valor numérico de cada código encontrado
- La descripción del `<summary>` XML

Ejemplo:
```csharp
/// <summary>
/// Gimnasio no encontrado
/// </summary>
GymNotFound = 136,
```

Resultado: `GymNotFound` → Valor: `136`, Descripción: "Gimnasio no encontrado"

### Step 4: Leer el Archivo JSON de Mensajes

Leer `Common/Common.Messages/MessagesJson/UserMessageError.json` y extraer los códigos existentes.

### Step 5: Identificar Códigos Faltantes

Comparar los códigos usados en el handler con los que existen en el JSON.

Si todos los códigos ya existen:

```
✓ Todos los códigos de error del handler ya están en UserMessageError.json

Handler: LoginHandler.cs
Códigos analizados: 3
  - 63: InfoUserNotFound ✓
  - 105: IncorrectPassword ✓
  - 66: UserBlocked ✓

No hay cambios necesarios.
```

### Step 6: Mostrar Códigos Faltantes y Confirmar

Si hay códigos faltantes:

```
=== Sincronización de Mensajes ===

Handler: VerifyDocumentWithAIHandler.cs
JSON: Common/Common.Messages/MessagesJson/UserMessageError.json

Códigos encontrados en el handler:
  ✓ 143: InvalidDocumentImage (ya existe en JSON)
  ✗ 144: InvalidSelfieImage (FALTANTE)
  ✗ 145: DocumentVerificationError (FALTANTE)
  ✗ 146: DocumentNumberMismatch (FALTANTE)

Códigos a agregar (3):

  Code: 144
  Spanish: "Imagen selfie inválida"
  English: "Invalid selfie image"

  Code: 145
  Spanish: "Error en verificación de documento"
  English: "Document verification error"

  Code: 146
  Spanish: "El número de documento no coincide con el número de documento de la persona"
  English: "Document number does not match the person's document number"

¿Proceder con la actualización?
```

Usar AskUserQuestion para confirmar con opciones:
1. **Sí, agregar todos** - Agrega todos los códigos faltantes
2. **Revisar traducciones primero** - Permite editar las traducciones antes de agregar
3. **No, cancelar** - No hace cambios

### Step 7: Actualizar el Archivo JSON

Si el usuario confirma:

1. Leer el JSON actual
2. Agregar las nuevas entradas al array `Messages`
3. Ordenar el array por `Code` (ascendente)
4. Escribir el archivo JSON con formato correcto (indentación de 2 espacios, UTF-8 con BOM)

Formato de cada entrada nueva:
```json
{
  "Code": 144,
  "Message": {
    "Spanish": "Imagen selfie inválida",
    "English": "Invalid selfie image"
  }
}
```

### Step 8: Informar Resultado

```
✓ Sincronización completada

Archivo actualizado: Common/Common.Messages/MessagesJson/UserMessageError.json

Códigos agregados: 3
  - 144: InvalidSelfieImage
  - 145: DocumentVerificationError
  - 146: DocumentNumberMismatch

El handler VerifyDocumentWithAIHandler.cs ahora tiene todos sus códigos de error sincronizados.
```

## Generación de Traducciones

Para generar el mensaje en inglés:

1. **Usar el summary del enum** como base en español
2. **Traducir al inglés** de forma clara y concisa
3. Mantener el mismo tono y estilo de los mensajes existentes

Ejemplos de traducciones:
| Spanish | English |
|---------|---------|
| "Gimnasio no encontrado" | "Gym not found" |
| "Usuario Bloqueado" | "User blocked" |
| "Imagen del documento inválida" | "Invalid document image" |
| "El número de documento no coincide" | "Document number does not match" |

## Manejo de Errores

### Handler no encontrado

```
✗ No se encontró el archivo: <ruta>

Verifica que la ruta sea correcta. Ejemplo:
  /sync-messages LogicApi/LogicApi.BusinessLogic/AuthorizationHandler/Login/LoginHandler.cs
```

### No se encontraron CustomException

```
✓ No se encontraron usos de CustomException en el handler.

Handler: <nombre>

No hay códigos de error que sincronizar.
```

### Código no existe en el enum

```
⚠ Advertencia: El código "CodigoNoExistente" no existe en MessagesCodesError.

El handler usa un código que no está definido en el enum.
Primero agrega el código al enum antes de sincronizar los mensajes.

Archivo del enum: Common/Common.Messages/MessagesCodesError.cs
```

## Notas

- Solo analiza archivos `.cs` (handlers, servicios, etc.)
- Detecta códigos de `MessagesCodesError` (errores)
- Usa la descripción del `<summary>` XML como mensaje base en español
- Genera traducciones automáticas al inglés basadas en el contexto
- Siempre pide confirmación antes de modificar archivos
- Mantiene el orden numérico de códigos en el JSON

## Estándar Relacionado

Ver: `agent-os/standards/backend/error-handling.md` - Sección "Sincronización de Mensajes JSON"
