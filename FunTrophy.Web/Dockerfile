FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FunTrophy.Web/FunTrophy.Web.csproj", "FunTrophy.Web/"]
COPY ["FunTrophy.Shared/FunTrophy.Shared.csproj", "FunTrophy.Shared/"]
RUN dotnet restore "FunTrophy.Web/FunTrophy.Web.csproj"
COPY . .
WORKDIR "/src/FunTrophy.Web"
RUN dotnet build "FunTrophy.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FunTrophy.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY "FunTrophy.Web/nginx.conf" "/etc/nginx/nginx.conf"