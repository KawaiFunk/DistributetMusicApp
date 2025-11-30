FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# ... (rest of the base section)

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# --- Fix 1: Copy ALL required projects into their own folders in /src ---
# Copy ApiGateway project files (Presentation, Infrastructure, Core/Application, Core/Domain)
COPY ApiGateway/src/Presentation/ApiGateway.Api/*.csproj ApiGateway/src/Presentation/ApiGateway.Api/
COPY ApiGateway/src/Infrastructure/ApiGateway.Infrastructure/*.csproj ApiGateway/src/Infrastructure/ApiGateway.Infrastructure/
COPY ApiGateway/src/Core/ApiGateway.Application/*.csproj ApiGateway/src/Core/ApiGateway.Application/
COPY ApiGateway/src/Core/ApiGateway.Domain/*.csproj ApiGateway/src/Core/ApiGateway.Domain/
# You might also need the solution file if the project references are complex
COPY *.sln .

RUN dotnet restore "ApiGateway/src/Presentation/ApiGateway.Api/ApiGateway.Api.csproj"

# --- Fix 2: Copy the rest of the source code ---
COPY . . 
WORKDIR "/src/ApiGateway/src/Presentation/ApiGateway.Api"

# ... (rest of the build, publish, and final sections remain the same)