FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/KafkaSample.Web/KafkaSample.Web.csproj", "src/KafkaSample.Web/"]
RUN dotnet restore "./src/KafkaSample.Web/./KafkaSample.Web.csproj"
COPY . .
WORKDIR "/src/src/KafkaSample.Web"
RUN dotnet build "./KafkaSample.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KafkaSample.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaSample.Web.dll"]
