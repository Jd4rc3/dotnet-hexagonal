# Reto backend .net

Para ejecutar el proyecto primero se debe crear un container

``` bash
podman run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Arc33456$" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest
```
Para ejecutar el proyecto utiliza un IDE de tu preferencia o el CLI de dotnet estando en la raíz de la solución

``` shell
dotnet run --project ./Api
```