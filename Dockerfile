FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY KahootBackend.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "KahootBackend.dll", "--server.urls", "http://0.0.0.0:7161"]

EXPOSE 7161
ENV ASPNETCORE_URLS=http://0.0.0.0:7161
