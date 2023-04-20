REM Set function variables
SET SCRIPT_DIR=%~dp0
SET SOLUTON_DIR=%SCRIPT_DIR%..

REM Validate required environment variables
IF "%PPM_PFX_PATH%"=="" (
	echo "PPM_PFX_PATH wasn't set"
	exit 2
)

IF "%PPM_PFX_PASSWORD%"=="" (
	echo "PPM_PFX_PASSWORD wasn't set"
	exit 5
)

REM Sign the Installer
signtool.exe sign ^
	/debug^
	/f "%PPM_PFX_PATH%"^
	/fd SHA256^
	/t http://timestamp.sectigo.com^
	/p "%PPM_PFX_PASSWORD%"^
	"%SOLUTON_DIR%\PpmWixInstaller\bin\Release\Paratext Plugin Manager Installer.msi"
