FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["InMa.Portal/InMa.Portal.csproj", "InMa.Portal/"]
RUN dotnet restore "InMa.Portal/InMa.Portal.csproj"
COPY . .
WORKDIR "/src/InMa.Portal"
RUN dotnet build "InMa.Portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InMa.Portal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InMa.Portal.dll"]
