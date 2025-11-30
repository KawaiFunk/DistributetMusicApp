# ApiGateway Dockerfile (Corrected for location at ApiGateway.Api/Dockerfile)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# --- STEP 1: Copy ONLY the required CSPROJ files ---
# Copying project files relative to the SOLUTION ROOT ('.')
COPY ["ApiGateway.Api/ApiGateway.Api.csproj", "ApiGateway.Api/"]
COPY ["ApiGateway.Infrastructure/ApiGateway.Infrastructure.csproj", "ApiGateway.Infrastructure/"]
COPY ["ApiGateway.Application/ApiGateway.Application.csproj", "ApiGateway.Application/"]
COPY ["ApiGateway.Domain/ApiGateway.Domain.csproj", "ApiGateway.Domain/"]

# Restore dependencies
RUN dotnet restore "ApiGateway.Api/ApiGateway.Api.csproj"

# --- STEP 2: Copy the rest of the source code ---
COPY . .
WORKDIR "/src/ApiGateway.Api"
RUN dotnet build "./ApiGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy the Ocelot config which is needed by the ApiGateway
COPY ApiGateway.Api/ocelot.json . 
ENTRYPOINT ["dotnet", "ApiGateway.Api.dll"]