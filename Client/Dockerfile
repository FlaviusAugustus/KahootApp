FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app
COPY Client ./
RUN dotnet restore KahootFrontend.csproj
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "KahootFrontend.dll", "--server.urls", "http://0.0.0.0:5161"]

EXPOSE 5161