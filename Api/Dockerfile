FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY Api/KahootBackend.csproj ./

RUN dotnet restore KahootBackend.csproj

COPY Api/ ./

RUN dotnet publish KahootBackend.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "KahootBackend.dll", "--server.urls", "http://0.0.0.0:7161", "--launch-profile docker"]

EXPOSE 7161
