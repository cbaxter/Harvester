@ECHO OFF

set MSBuildExe="%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"

IF NOT EXIST %MSBuildExe% GOTO MSBuildNotFound

:: Prompt for product version
:: --------------------------------------------------
set /p Configuration=Configuration: %=%
set /p MajorVersion=Major Version: %=%
set /p MinorVersion=Minor Version: %=%
set /p RevisionNumber=Revision Number: %=%

:: Run MSBuild
:: --------------------------------------------------
cd src
%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild build.proj /target:Package /property:Configuration=%Configuration% /property:MajorVersion=%MajorVersion% /property:MinorVersion=%MinorVersion% /property:RevisionNumber=%RevisionNumber% /property:Commit=%Commit%
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
