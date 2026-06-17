# Gym Ecuador

## Auto publicación (GitHub Actions → IIS)

El workflow [`.github/workflows/deploy-iis.yml`](.github/workflows/deploy-iis.yml) publica **GatewayCoreAPI** en la VM Windows vía **SCP + SSH** al hacer push a `main` (o manualmente desde **Actions → Deploy a IIS → Run workflow**).

### 1. Habilitar OpenSSH Server en la VM (Windows)

Ejecutar **PowerShell como Administrador** en el servidor:

```powershell
Add-WindowsCapability -Online -Name OpenSSH.Server~~~~0.0.1.0
Start-Service sshd
Set-Service -Name sshd -StartupType 'Automatic'
```

Comprobar que el servicio está activo y escuchando en el puerto 22:

```powershell
Get-Service sshd
Get-NetTCPConnection -LocalPort 22 -State Listen
```

### 2. Firewall (puerto 22)

**En la VM (Windows Firewall):**

Regla inbound para SSH (PowerShell como Administrador):

```powershell
New-NetFirewallRule -Name "SSH" -DisplayName "SSH" -Protocol TCP -LocalPort 22 -Action Allow -Direction Inbound
```

Si ya existe (`Cannot create a file when that file already exists`), verifica que esté activa:

```powershell
Get-NetFirewallRule -Name "SSH" | Format-Table Name, Enabled, Direction, Action
Enable-NetFirewallRule -Name "SSH"   # solo si Enabled = False
```

OpenSSH puede crear además la regla `OpenSSH-Server-In-TCP`. Comprueba ambas si hace falta:

```powershell
Get-NetFirewallRule -Name "OpenSSH-Server-In-TCP" -ErrorAction SilentlyContinue | Format-Table Name, Enabled, Direction, Action
Enable-NetFirewallRule -Name "OpenSSH-Server-In-TCP" -ErrorAction SilentlyContinue
```

**En el panel del proveedor (Contabo / VPS):** abrir también el puerto **22/TCP** en el firewall en la nube hacia Internet.  
Si solo abres el firewall de Windows pero el proveedor bloquea 22, GitHub Actions verá `dial tcp ...:22: i/o timeout`.

### 3. Permisos en IIS

El usuario de deploy (`VM_USER`) debe poder **escribir** en:

```
C:\inetpub\wwwroot\MobileApi
```

El App Pool que reinicia el pipeline es **`FitcenterMobileApi`**.

### 4. Secrets en GitHub

En el repositorio: **Settings → Secrets and variables → Actions → New repository secret**

| Secret | Descripción |
|--------|-------------|
| `VM_IP` | IP pública o hostname de la VM |
| `VM_USER` | Usuario Windows con permisos en la carpeta del sitio |
| `VM_PASSWORD` | Contraseña del usuario |

### 5. Probar conexión SSH

Desde **tu PC** (no desde la VM), usando la misma IP que en `VM_IP`:

```bash
ssh VM_USER@VM_IP
```

Si desde tu PC conecta pero GitHub Actions no, revisa reglas del proveedor que limiten por IP.

Si **nadie** desde fuera conecta, el puerto 22 no está expuesto correctamente.

### Solución de problemas

#### `dial tcp ...:22: i/o timeout`

Significa que el runner de GitHub **no llega** al puerto 22. No es error de usuario/contraseña.

Checklist en la VM:

1. `sshd` en ejecución: `Get-Service sshd` → **Running**
2. Escucha en 22: `Get-NetTCPConnection -LocalPort 22 -State Listen`
3. Regla Windows Firewall inbound TCP 22 **Allow**
4. Firewall del **hosting/VPS** (Hetzner, OVH, Azure NSG, etc.): permitir **22/TCP** desde `0.0.0.0/0` o desde [rangos IP de GitHub Actions](https://api.github.com/meta) (`actions`)
5. Secret `VM_IP` = IP **pública** correcta (no la IP privada `10.x` / `192.168.x`)

Verificación rápida desde internet (desde tu PC):

```powershell
Test-NetConnection -ComputerName VM_IP -Port 22
```

`TcpTestSucceeded : True` → el puerto es alcanzable. `False` → sigue bloqueado a nivel red.

#### Contabo (firewall en la nube)

Contabo trae un **firewall externo** aparte del de Windows. **Por defecto bloquea todo el tráfico entrante** hasta que creas reglas *Accept* y asignas el firewall al VPS.

1. Entra en [Contabo Customer Control Panel](https://my.contabo.com/) → **VPS** → tu servidor (`86.48.26.214`).
2. Abre **Firewall** (o **Security** / **Network** según la vista).
3. Crea un firewall nuevo o edita el existente.
4. Añade regla **Accept**:
   - Tipo: **SSH** (o manual: **TCP**, puerto **22**)
   - Origen: `0.0.0.0/0` (todo internet; necesario para GitHub Actions)
5. **Asigna** ese firewall al VPS (Assign / Apply to instance).
6. Espera 1–2 minutos y prueba desde tu PC:

```powershell
Test-NetConnection -ComputerName 86.48.26.214 -Port 22
```

Documentación: [Contabo Firewall](https://help.contabo.com/en/support/solutions/articles/103000390430-firewall-what-is-it-and-how-does-it-protect-my-vps-vds-)

Si el sitio IIS ya responde por HTTP/HTTPS, probablemente ya tienes reglas para **80** y **443**; solo falta añadir **22** para el deploy.

**Importante:** Si solo permites el puerto **22** y luego *Block all traffic*, al asignar el firewall se **bloquean RDP (3389), HTTP (80) y HTTPS (443)**. No se arregla desde Windows por dentro: el firewall de Contabo actúa **antes** de llegar al servidor.

Reglas recomendadas **antes** de asignar al VPS (orden: Accept arriba, Drop al final):

| Regla | Puerto | Para qué |
|-------|--------|----------|
| Accept TCP | 3389 | Escritorio remoto (RDP) |
| Accept TCP | 80 | IIS HTTP |
| Accept TCP | 443 | IIS HTTPS |
| Accept TCP | 22 | SSH (deploy GitHub Actions) |
| Drop all | * | Bloquear el resto |

**Si ya perdiste RDP/conexión:** entra a [my.contabo.com](https://my.contabo.com/) → **Firewall** → desactiva el firewall (icono **X**) o quita el VPS de *VPS/VDS activos* → guarda. Recuperas acceso sin entrar al servidor. Alternativa: **VNC** en el VPS (Manage → VNC Information) para consola directa.

#### `tar: Option --overwrite is not supported` (Windows)

El `tar` de Windows **no soporta** `--overwrite`. En el workflow **no** uses `overwrite: true`; usa ruta estilo Unix `/c/inetpub/wwwroot/MobileApi` y `tar_dereference: true`. El extract sobrescribe archivos existentes sin esa flag.

Si fallan otros pasos del `tar`, en la VM instala [Git for Windows](https://git-scm.com/download/win) y configura OpenSSH para usar Bash:

```powershell
New-ItemProperty -Path "HKLM:\SOFTWARE\OpenSSH" -Name DefaultShell -Value "$env:ProgramFiles\Git\bin\bash.exe" -PropertyType String -Force
```

#### Alternativas si no puedes abrir el 22 a Internet

- **Self-hosted runner** en la VM o en la misma red (el deploy copia por localhost/red interna).
- **VPN** entre GitHub y la VM.
- Volver a deploy manual con `deploy.ps1` (WinRM/red interna).

### Comportamiento del deploy

- **Configuración de publish:** `Staging`
- **Destino:** `/c/inetpub/wwwroot/MobileApi` (`C:\inetpub\wwwroot\MobileApi`)
- **Archivos:** solo **sobrescribe** lo publicado; **no borra** la carpeta destino (`rm: false`)
