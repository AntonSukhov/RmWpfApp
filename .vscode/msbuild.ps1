param(
    [Parameter(Mandatory)][string]$Target,
    [string]$Configuration = "Debug"
)

$vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"

if (-not (Test-Path $vswhere)) {
    Write-Error "vswhere.exe не найден по пути: $vswhere"
    exit 1
}

$msbuild = & $vswhere -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1

if (-not $msbuild) {
    Write-Error "MSBuild не найден. Убедитесь, что Visual Studio установлена с компонентом .NET desktop development."
    exit 1
}

$solution = Join-Path $PSScriptRoot "..\RM.App.sln"

& $msbuild $solution /t:$Target /p:Configuration=$Configuration /v:minimal
exit $LASTEXITCODE
