FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContentManager.Api/ContentManager.Api.csproj", "ContentManager.Api/"]
COPY ["ContentManager.Domain/ContentManager.Domain.csproj", "ContentManager.Domain/"]
COPY ["ContentManager.Data/ContentManager.Data.csproj", "ContentManager.Data/"]
RUN dotnet restore "./ContentManager.Api/./ContentManager.Api.csproj"
COPY . .
WORKDIR "/src/ContentManager.Api"
RUN dotnet build "./ContentManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ContentManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentManager.Api.dll"]