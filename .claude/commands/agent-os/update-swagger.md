# Update Swagger

Actualiza la documentación del API Swagger desde el servidor backend local.

## Descripción

Este comando ejecuta el script `update-swagger.ps1` que descarga la especificación OpenAPI/Swagger desde el servidor backend en `https://localhost:44326/swagger/apicore/swagger.json` y la guarda en `agent-os/standards/backend/api-swagger.json`.

## Requisitos

- El servidor backend debe estar corriendo en `https://localhost:44326`
- El endpoint `/swagger/apicore/swagger.json` debe estar disponible

## Proceso

### Step 1: Verificar si el servidor está disponible

Antes de ejecutar el script, verifica que el servidor backend esté corriendo:

Use Shell tool to check if the server is responding:

```powershell
try {
    $response = Invoke-WebRequest -Uri "https://localhost:44326/swagger/apicore/swagger.json" -Method Head -TimeoutSec 5 -ErrorAction Stop
    Write-Host "✓ Servidor backend disponible" -ForegroundColor Green
} catch {
    Write-Host "✗ Servidor backend no disponible. Asegúrate de que esté corriendo en https://localhost:44326" -ForegroundColor Red
    exit 1
}
```

If the server is not available, stop here and inform the user that they need to start the backend server first.

### Step 2: Ejecutar el script de actualización

Execute the update-swagger.ps1 script:

```powershell
cd c:\Users\wwwda\Documents\Repos\Gym_Mobile\gym_mobile_app
.\update-swagger.ps1
```

### Step 3: Verificar el resultado

Check if the file was updated successfully and show information:

```powershell
if (Test-Path "agent-os\standards\backend\api-swagger.json") {
    $fileInfo = Get-Item "agent-os\standards\backend\api-swagger.json"
    Write-Host "`n=== Swagger actualizado ===" -ForegroundColor Cyan
    Write-Host "Archivo: agent-os\standards\backend\api-swagger.json" -ForegroundColor Gray
    Write-Host "Tamaño: $([math]::Round($fileInfo.Length / 1KB, 2)) KB" -ForegroundColor Gray
    Write-Host "Última actualización: $($fileInfo.LastWriteTime)" -ForegroundColor Gray
    Write-Host "`n✓ Listo para usar en desarrollo" -ForegroundColor Green
} else {
    Write-Host "✗ Error: No se pudo crear el archivo api-swagger.json" -ForegroundColor Red
}
```

### Step 4: Informar al usuario

Output to user:

```
✓ Swagger actualizado exitosamente.

El archivo api-swagger.json ahora contiene la especificación más reciente del API.

Puedes:
- Importarlo en Postman/Insomnia para probar endpoints
- Referenciarlo cuando implementes servicios de networking
- Usar /inject-standards para incluir contexto del API en tu trabajo

Ubicación: agent-os/standards/backend/api-swagger.json
```

## Manejo de Errores

Si el script falla por cualquier razón (servidor no disponible, problemas de red, etc.), informa al usuario con instrucciones alternativas:

```
✗ No se pudo descargar el swagger automáticamente.

Alternativas:

1. Verifica que el servidor backend esté corriendo:
   https://localhost:44326

2. Descarga manualmente desde el navegador:
   - Abre: https://localhost:44326/swagger/index.html
   - Descarga el JSON
   - Guárdalo en: agent-os\standards\backend\api-swagger.json

3. O copia desde tu carpeta de Downloads si ya tienes una versión:
   Copy-Item "c:\Users\wwwda\Downloads\swagger.json" -Destination "agent-os\standards\backend\api-swagger.json"
```

## Notas

- El script maneja automáticamente certificados SSL autofirmados
- Compatible con PowerShell 5.1 y PowerShell 7+
- El archivo puede ser grande (600+ KB), esto es normal
- No es necesario ejecutar este comando cada vez, solo cuando el backend tenga cambios significativos

run
