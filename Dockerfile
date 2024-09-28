FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Contato.Api/Contato.Api.csproj", "src/Contato.Api/"]
COPY ["src/Contato.Application/Contato.Application.csproj", "src/Contato.Application/"]
COPY ["src/Contato.Domain/Contato.Domain.csproj", "src/Contato.Domain/"]
COPY ["src/Contato.Infra/Contato.Infra.csproj", "src/Contato.Infra/"]
RUN dotnet restore "src/Contato.Api/Contato.Api.csproj"
COPY . .
WORKDIR "/src/src/Contato.Api"
RUN dotnet build "Contato.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Contato.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contato.Api.dll"]
