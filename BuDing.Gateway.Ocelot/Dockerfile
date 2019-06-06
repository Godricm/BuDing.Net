FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BuDing.Gateway.Ocelot/BuDing.Gateway.Ocelot.csproj", "BuDing.Gateway.Ocelot/"]
RUN dotnet restore "BuDing.Gateway.Ocelot/BuDing.Gateway.Ocelot.csproj"
COPY . .
WORKDIR "/src/BuDing.Gateway.Ocelot"
RUN dotnet build "BuDing.Gateway.Ocelot.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BuDing.Gateway.Ocelot.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BuDing.Gateway.Ocelot.dll"]