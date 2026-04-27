@echo off
setlocal

set ROOT=%~dp0..
set SLN=%ROOT%\Source\CoffeePot.API\CoffeePot.sln
set SBOM_OUT=%ROOT%\Bin\CoffeePot.API\SBOM

if not exist "%SBOM_OUT%" (
    echo Creating SBOM output directory...
    mkdir "%SBOM_OUT%"
)

echo Generating SBOM...
dotnet-CycloneDX "%SLN%" -o "%SBOM_OUT%" -fn bom.json

if %ERRORLEVEL% EQU 0 (
    echo.
    echo SBOM successfully generated: %SBOM_OUT%\bom.json
) else (
    echo.
    echo ERROR: SBOM generation failed with exit code %ERRORLEVEL%.
    exit /b %ERRORLEVEL%
)

endlocal
