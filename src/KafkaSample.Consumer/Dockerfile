# Copyright (c) Dennis Shevtsov. All rights reserved.
# Licensed under the MIT License.
# See LICENSE in the project root for license information.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/KafkaSample.Consumer/KafkaSample.Consumer.csproj", "src/KafkaSample.Consumer/"]
RUN dotnet restore "./src/KafkaSample.Consumer/./KafkaSample.Consumer.csproj"
COPY . .
WORKDIR "/src/src/KafkaSample.Consumer"
RUN dotnet build "./KafkaSample.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KafkaSample.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaSample.Consumer.dll"]
