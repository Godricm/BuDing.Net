FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BuDing.Consul/BuDing.Consul.csproj", "BuDing.Consul/"]
RUN dotnet restore "BuDing.Consul/BuDing.Consul.csproj"
COPY . .
WORKDIR "/src/BuDing.Consul"
RUN dotnet build "BuDing.Consul.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BuDing.Consul.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BuDing.Consul.dll"]