FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["BuDing.JwtBearer/BuDing.JwtBearer.csproj", "BuDing.JwtBearer/"]
RUN dotnet restore "BuDing.JwtBearer/BuDing.JwtBearer.csproj"
COPY . .
WORKDIR "/src/BuDing.JwtBearer"
RUN dotnet build "BuDing.JwtBearer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BuDing.JwtBearer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BuDing.JwtBearer.dll"]