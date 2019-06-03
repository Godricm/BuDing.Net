FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BuDing.Chaos/BuDing.Chaos.csproj", "BuDing.Chaos/"]
RUN dotnet restore "BuDing.Chaos/BuDing.Chaos.csproj"
COPY . .
WORKDIR "/src/BuDing.Chaos"
RUN dotnet build "BuDing.Chaos.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BuDing.Chaos.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BuDing.Chaos.dll"]