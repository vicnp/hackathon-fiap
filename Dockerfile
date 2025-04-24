FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY ["./src/Hackathon.Fiap.API/Hackathon.Fiap.API.csproj", "Hackathon.Fiap.API/"]
COPY . .
WORKDIR "/app/src/Hackathon.Fiap.API"


RUN dotnet build "./Hackathon.Fiap.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Hackathon.Fiap.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hackathon.Fiap.API.dll"]