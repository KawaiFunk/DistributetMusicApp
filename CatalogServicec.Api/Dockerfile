# CatalogService Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Note the extra 'c' in CatalogServicec.Api matching your screenshot
COPY ["CatalogService/src/Presentation/CatalogServicec.Api/CatalogServicec.Api.csproj", "CatalogService/src/Presentation/CatalogServicec.Api/"]
COPY ["CatalogService/src/Infrastructure/CatalogService.Infrastructure/CatalogService.Infrastructure.csproj", "CatalogService/src/Infrastructure/CatalogService.Infrastructure/"]
COPY ["CatalogService/src/Core/CatalogService.Application/CatalogService.Application.csproj", "CatalogService/src/Core/CatalogService.Application/"]
COPY ["CatalogService/src/Core/CatalogService.Domain/CatalogService.Domain.csproj", "CatalogService/src/Core/CatalogService.Domain/"]

RUN dotnet restore "CatalogService/src/Presentation/CatalogServicec.Api/CatalogServicec.Api.csproj"
COPY . .
WORKDIR "/src/CatalogService/src/Presentation/CatalogServicec.Api"
RUN dotnet build "CatalogServicec.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CatalogServicec.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogServicec.Api.dll"]