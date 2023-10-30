#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MQTTServer/MQTTServer.csproj", "MQTTServer/"]
COPY ["Communication/Communication.csproj", "Communication/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "MQTTServer/MQTTServer.csproj"
COPY . .
WORKDIR "/src/MQTTServer"
RUN dotnet build "MQTTServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MQTTServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MQTTServer.dll"]