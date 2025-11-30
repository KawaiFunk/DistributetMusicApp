# ApiGateway Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# COPY using the paths seen in your screenshot
# Format: COPY ["Path/To/Project.csproj", "Destination/"]
COPY ["ApiGateway/src/Presentation/ApiGateway.Api/ApiGateway.Api.csproj", "ApiGateway/src/Presentation/ApiGateway.Api/"]
COPY ["ApiGateway/src/Infrastructure/ApiGateway.Infrastructure/ApiGateway.Infrastructure.csproj", "ApiGateway/src/Infrastructure/ApiGateway.Infrastructure/"]
# Assuming Application and Domain exist in Core based on your image
COPY ["ApiGateway/src/Core/ApiGateway.Application/ApiGateway.Application.csproj", "ApiGateway/src/Core/ApiGateway.Application/"]
COPY ["ApiGateway/src/Core/ApiGateway.Domain/ApiGateway.Domain.csproj", "ApiGateway/src/Core/ApiGateway.Domain/"]

RUN dotnet restore "ApiGateway/src/Presentation/ApiGateway.Api/ApiGateway.Api.csproj"
COPY . .
WORKDIR "/src/ApiGateway/src/Presentation/ApiGateway.Api"
RUN dotnet build "ApiGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApiGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/ApiGateway/src/Presentation/ApiGateway.Api/ocelot.json . 
ENTRYPOINT ["dotnet", "ApiGateway.Api.dll"]