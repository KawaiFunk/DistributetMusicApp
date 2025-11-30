# CatalogService Dockerfile (Final attempt at path correction)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# ... (rest of base section)

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# --- STEP 1: Copy everything needed for dotnet restore (COPY . . is fine) ---
COPY . . 

# --- STEP 2: Restore dependencies from the correct location ---
# CRITICAL FIX: Assume the project is at the flat root, like ApiGateway
RUN dotnet restore "CatalogServicec.Api/CatalogServicec.Api.csproj" 

# --- STEP 3: Build the project ---
WORKDIR "/src/CatalogServicec.Api"
RUN dotnet build "CatalogServicec.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
# ... (rest of the file remains the same)