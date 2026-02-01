@echo off
setlocal
set /p "originalName=Please enter the package name (ex:"Lua", "Common"): "

if "originalName"=="" (
    echo Please provide a name as the first argument.
    pause
    exit /b 1
)

set "firstLetter=%originalName:~0,1%"
set "remainingLetters=%originalName:~1%"
set "myName=%firstLetter%%remainingLetters%"

for /f "delims=" %%i in ('powershell -command "(\"%originalName%\".ToLower())"') do set "myName=%%i"


set "folderPath=ITI.DesignPatterns.%originalName%"
if not exist "%folderPath%" (
    mkdir "%folderPath%"
)

set "runtimeFolderPath=%folderPath%/Runtime"
if not exist "%runtimeFolderPath%" (
    mkdir "%runtimeFolderPath%"
)

set "editorFolderPath=%folderPath%/Editor"
if not exist "%editorFolderPath%" (
    mkdir "%editorFolderPath%"
)

(
echo {
echo     "name": "com.iti.designpatterns.%myName%",
echo     "version": "0.1.0",
echo     "displayName": "ITI.DesignPatterns.%originalName%",
echo     "description": "Package description",
echo     "unity": "2022.2",
echo     "unityRelease": "0b8",
echo     "hideInEditor": false
echo }
) > "%folderPath%"\package.json

(
echo {
echo     "name": "ITI.DesignPatterns.%originalName%.Runtime",
echo     "rootNamespace": "ITI.DesignPatterns.%originalName%.Runtime",
echo     "references": [],
echo     "includePlatforms": [],
echo     "excludePlatforms": [],
echo     "allowUnsafeCode": false,
echo     "overrideReferences": false,
echo     "precompiledReferences": [],
echo     "autoReferenced": true,
echo     "defineConstraints": [],
echo     "versionDefines": [],
echo    "noEngineReferences": false
echo }
) > "%runtimeFolderPath%"\ITI.DesignPatterns.%originalName%.Runtime.asmdef

(
echo {
echo     "name": "ITI.DesignPatterns.%originalName%.Editor",
echo     "rootNamespace": "ITI.DesignPatterns.%originalName%.Editor",
echo     "references": ["ITI.DesignPatterns.%originalName%.Runtime"],
echo     "includePlatforms": ["Editor"],
echo     "excludePlatforms": [],
echo     "allowUnsafeCode": false,
echo     "overrideReferences": false,
echo     "precompiledReferences": [],
echo     "autoReferenced": true,
echo     "defineConstraints": [],
echo     "versionDefines": [],
echo    "noEngineReferences": false
echo }
) > "%editorFolderPath%"\ITI.DesignPatterns.%originalName%.Editor.asmdef

echo package.json has been created successfully.
endlocal
pause