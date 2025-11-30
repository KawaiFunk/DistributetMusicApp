FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# ... (rest of the base section)

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# --- Fix 1: Copy ALL required projects into their own folders in /src ---
COPY CatalogService/src/Presentation/CatalogServicec.Api/*.csproj CatalogService/src/Presentation/CatalogServicec.Api/
COPY CatalogService/src/Infrastructure/CatalogService.Infrastructure/*.csproj CatalogService/src/Infrastructure/CatalogService.Infrastructure/
COPY CatalogService/src/Core/CatalogService.Application/*.csproj CatalogService/src/Core/CatalogService.Application/
COPY CatalogService/src/Core/CatalogService.Domain/*.csproj CatalogService/src/Core/CatalogService.Domain/
# You might also need the solution file if the project references are complex
COPY *.sln .

RUN dotnet restore "CatalogService/src/Presentation/CatalogServicec.Api/CatalogServicec.Api.csproj"

# --- Fix 2: Copy the rest of the source code ---
COPY . .
WORKDIR "/src/CatalogService/src/Presentation/CatalogServicec.Api"

# ... (rest of the build, publish, and final sections remain the same)