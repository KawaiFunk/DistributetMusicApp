# CatalogService Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# --- STEP 1: Copy ONLY the required CSPROJ files ---
# Copying project files relative to the SOLUTION ROOT ('.')
COPY ["CatalogService.Api/CatalogService.Api.csproj", "CatalogService.Api/"]
COPY ["CatalogService.Infrastructure/CatalogService.Infrastructure.csproj", "CatalogService.Infrastructure/"]
COPY ["CatalogService.Application/CatalogService.Application.csproj", "CatalogService.Application/"]
COPY ["CatalogService.Domain/CatalogService.Domain.csproj", "CatalogService.Domain/"]

# Restore dependencies
RUN dotnet restore "CatalogService.Api/CatalogService.Api.csproj"

# --- STEP 2: Copy the rest of the source code ---
COPY . .
WORKDIR "/src/CatalogService.Api"
RUN dotnet build "./CatalogService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CatalogService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogService.Api.dll"]
