FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContentManager.Worker/ContentManager.Worker.csproj", "ContentManager.Worker/"]
COPY ["ContentManager.Domain/ContentManager.Domain.csproj", "ContentManager.Domain/"]
COPY ["ContentManager.Data/ContentManager.Data.csproj", "ContentManager.Data/"]
RUN dotnet restore "./ContentManager.Worker/./ContentManager.Worker.csproj"
COPY . .
WORKDIR "/src/ContentManager.Worker"
RUN dotnet build "./ContentManager.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ContentManager.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentManager.Worker.dll"]