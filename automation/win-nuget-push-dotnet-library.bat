@echo off
echo Go to the dotnet shared directory
set SCRIPT_HOME_PATH=%CD%
cd ..\services\_shared\dotnet\TaskTrainLibrary
for /d %%d in (%CD%\TaskTrain*) do (
    echo processing: %%d
    dotnet pack -c Release -o _pack
)
cd _pack

set "nuget-source-file-path=%SCRIPT_HOME_PATH%\_nuget-address"
set /p nuget-source=<"%nuget-source-file-path%"
echo %nuget-source%

for %%f in (%CD%\*.nupkg) do (
    echo push package %%~nxf
    dotnet nuget push -s %nuget-source% %%f
)

cd %SCRIPT_HOME_PATH%

PAUSE