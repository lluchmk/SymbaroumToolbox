FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["Abilities/Abilities.API/Abilities.API.csproj", "Abilities/Abilities.API/"]
COPY ["Abilities/Abilities.Persistence/Abilities.Persistence.csproj", "Abilities/Abilities.Persistence/"]
COPY ["Abilities/Abilities.Domain/Abilities.Domain.csproj", "Abilities/Abilities.Domain/"]
COPY ["Abilities/Abilities.Application/Abilities.Application.csproj", "Abilities/Abilities.Application/"]
RUN dotnet restore "Abilities/Abilities.API/Abilities.API.csproj"
COPY . .
WORKDIR "/src/Abilities/Abilities.API"
RUN dotnet build "Abilities.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Abilities.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Abilities.API.dll"]