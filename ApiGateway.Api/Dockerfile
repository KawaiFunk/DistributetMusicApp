# ApiGateway Dockerfile (Revised to ensure files are visible)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# --- STEP 1: Copy everything needed for dotnet restore ---
# The COPY command below copies your entire project structure 
# from the root (where the workflow runs) into the container's /src
COPY . . 

# --- STEP 2: Restore dependencies from the correct location ---
# We use the full path relative to /src inside the container
RUN dotnet restore "ApiGateway/src/Presentation/ApiGateway.Api/ApiGateway.Api.csproj"

# --- STEP 3: Build the project ---
WORKDIR "/src/ApiGateway/src/Presentation/ApiGateway.Api"
RUN dotnet build "ApiGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApiGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy the Ocelot config needed for the gateway to run
COPY ApiGateway/src/Presentation/ApiGateway.Api/ocelot.json . 
ENTRYPOINT ["dotnet", "ApiGateway.Api.dll"]