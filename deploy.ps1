# deploy.ps1
# Ejecutar: .\deploy.cmd
# O en PowerShell: powershell -ExecutionPolicy Bypass -File .\deploy.ps1
$serverIp = "86.48.26.214"
$remotePath = "C:\inetpub\wwwroot\MobileApi"
$remoteUnc = "\\$serverIp\c$\inetpub\wwwroot\MobileApi"

dotnet publish -c Release -o ./publish

# Copiar archivos
Copy-Item -Path "./publish/*" -Destination $remoteUnc -Recurse -Force

# Reiniciar IIS (o solo el sitio)
$cred = Get-Credential  # o hardcodearlo si es entorno privado
Invoke-Command -ComputerName $serverIp -Credential $cred -ScriptBlock {
    iisreset /restart
    # ó solo el app pool:
    # Restart-WebAppPool "MobileApi"
}

Write-Host "Deploy completado -> $remotePath en $serverIp"
