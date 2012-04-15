@ECHO OFF

set MSBuildExe="%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
set GitExe="%SYSTEMDRIVE%\Program Files (x86)\Git\bin\git.exe"

IF NOT EXIST %MSBuildExe% GOTO MSBuildNotFound
IF NOT EXIST %GitExe% GOTO GitNotFound

:: Load HEAD commit in to parameter
:: --------------------------------------------------
%GitExe% rev-parse HEAD > .gitcommit
set /p Commit= < .gitcommit
del .gitcommit
echo Commit: %Commit%

:: Prompt for product version
:: --------------------------------------------------
set /p MajorVersion=Major Version: %=%
set /p MinorVersion=Minor Version: %=%
set /p RevisionNumber=Revision Number: %=%

:: Run MSBuild
:: --------------------------------------------------
cd src
%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild build.proj /target:Package /property:Configuration=Release /property:MajorVersion=%MajorVersion% /property:MinorVersion=%MinorVersion% /property:RevisionNumber=%RevisionNumber% /property:Commit=%Commit%
if errorlevel 1 pause
cd..

GOTO Exit

:MSBuildNotFound
ECHO MSBuild Not Found: %MSBuildExe%
PAUSE
GOTO Exit

:GitNotFound
ECHO GIT Not Found: %GitExe%
PAUSE
GOTO Exit

:Exit
