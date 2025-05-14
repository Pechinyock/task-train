@echo off
set SCRIPT_HOME_PATH=%CD%
cd ..\..\services\_storages-showcase\docker

echo Building postgres image

docker buildx build -t postgres -f PostgreSQL.Dockerfile .

echo enter container's name:
set /p container_name=

echo enter postgres user's password:
set /p password=

echo enter port for database:
set /p port=

docker run --name %container_name% -e POSTGRES_PASSWORD=%password% -p %port%:5432 -d postgres

echo.
echo successufly run postgreSQL container with no volume!
echo any data you will record to database will be will permanently deleted on container shut down!
echo.
echo container's name: %container_name%
echo.
echo postgres user password: %password%
echo.
echo port for access from host machine: %port%
echo.

cd %SCRIPT_HOME_PATH%
pause