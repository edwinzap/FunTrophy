#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FunTrophy.API/FunTrophy.API.csproj", "FunTrophy.API/"]
COPY ["FunTrophy.Infrastructure/FunTrophy.Infrastructure.csproj", "FunTrophy.Infrastructure/"]
COPY ["FunTrophy.Shared/FunTrophy.Shared.csproj", "FunTrophy.Shared/"]

RUN dotnet restore "FunTrophy.API/FunTrophy.API.csproj"
COPY . .
WORKDIR "/src/FunTrophy.API"
RUN dotnet build "FunTrophy.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FunTrophy.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FunTrophy.API.dll"]