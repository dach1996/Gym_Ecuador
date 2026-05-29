param(
    [string]$Root = $PSScriptRoot
)

$Root = (Resolve-Path (Join-Path $Root "..\..")).Path
$removed = 0
$failed = 0

Get-ChildItem -Path $Root -Filter "*.csproj" -Recurse -File | ForEach-Object {
    foreach ($folder in @("bin", "obj")) {
        $path = Join-Path $_.DirectoryName $folder
        if (-not (Test-Path $path)) {
            continue
        }

        try {
            Remove-Item -Path $path -Recurse -Force -ErrorAction Stop
            Write-Host "Eliminado: $path"
            $removed++
        }
        catch {
            Write-Warning "No se pudo eliminar (archivo en uso?): $path"
            $failed++
        }
    }
}

Write-Host ""
Write-Host "Listo. Carpetas eliminadas: $removed | Fallidas: $failed | Raiz: $Root"
if ($failed -gt 0) {
    exit 1
}
