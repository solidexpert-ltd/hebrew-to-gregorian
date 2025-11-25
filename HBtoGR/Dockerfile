FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HBtoGR/HBtoGR.csproj", "HBtoGR/"]
RUN dotnet restore "HBtoGR/HBtoGR.csproj"
COPY . .
WORKDIR "/src/HBtoGR"
RUN dotnet build "HBtoGR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HBtoGR.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HBtoGR.dll"]
