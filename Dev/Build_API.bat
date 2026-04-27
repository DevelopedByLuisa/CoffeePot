@echo off
setlocal

set ROOT=%~dp0..
set SLN=%ROOT%\Source\CoffeePot.Api\CoffeePot.sln
set NUGET_CONFIG=%ROOT%\nuget.config

echo Restoring NuGet packages...
nuget restore "%SLN%" -ConfigFile "%NUGET_CONFIG%"

if %ERRORLEVEL% NEQ 0 (
  echo.
  echo ERROR: NuGet restore failed with exit code %ERRORLEVEL%.
  exit /b %ERRORLEVEL%
)
echo NuGet restore completed successfully.

echo.
echo Searching for MSBuild.exe...

set VSWHERE="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"

if not exist %VSWHERE% (
  echo ERROR: vswhere.exe not found. Please ensure Visual Studio 2017 or later is installed.
  exit /b 1
)

for /f "usebackq tokens=*" %%i in (`%VSWHERE% -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do (
  set MSBUILD=%%i
)

if not defined MSBUILD (
  echo ERROR: MSBuild.exe could not be found.
  exit /b 1
)

echo Found MSBuild: %MSBUILD%

echo.
echo Building solution...
"%MSBUILD%" "%SLN%" /v:minimal /m

if %ERRORLEVEL% EQU 0 (
  echo.
  echo Solution built successfully.
) else (
  echo.
  echo ERROR: Build failed with exit code %ERRORLEVEL%.
  exit /b %ERRORLEVEL%
)

endlocal
