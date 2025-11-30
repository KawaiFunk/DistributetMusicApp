# CatalogService Dockerfile (Revised to ensure files are visible)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# --- STEP 1: Copy everything needed for dotnet restore ---
COPY . . 

# --- STEP 2: Restore dependencies from the correct location ---
# Note the 'c' typo in CatalogServicec.Api
RUN dotnet restore "CatalogService/src/Presentation/CatalogServicec.Api/CatalogServicec.Api.csproj"

# --- STEP 3: Build the project ---
WORKDIR "/src/CatalogService/src/Presentation/CatalogServicec.Api"
RUN dotnet build "CatalogServicec.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CatalogServicec.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogServicec.Api.dll"]