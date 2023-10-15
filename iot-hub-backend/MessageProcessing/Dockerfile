#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app



FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MessageProcessing/MessageProcessing.csproj", "MessageProcessing/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Communication/Communication.csproj", "Communication/"]
RUN dotnet restore "MessageProcessing/MessageProcessing.csproj"
COPY . .
WORKDIR "/src/MessageProcessing"
RUN dotnet build "MessageProcessing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageProcessing.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageProcessing.dll"]