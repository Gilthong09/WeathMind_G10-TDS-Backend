FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WealthMind/WealthMind.csproj", "WealthMind/"]
COPY ["WealthMind.Core.Application/WealthMind.Core.Application.csproj", "WealthMind.Core.Application/"]
COPY ["WealthMind.Core.Domain/WealthMind.Core.Domain.csproj", "WealthMind.Core.Domain/"]
COPY ["WealthMind.Infrastructure.Identity/WealthMind.Infrastructure.Identity.csproj", "WealthMind.Infrastructure.Identity/"]
COPY ["WealthMind.Infrastructure.Persistence/WealthMind.Infrastructure.Persistence.csproj", "WealthMind.Infrastructure.Persistence/"]
COPY ["WealthMind.Infrastructure.Shared/WealthMind.Infrastructure.Shared.csproj", "WealthMind.Infrastructure.Shared/"]
RUN dotnet restore "WealthMind/WealthMind.csproj"
COPY . .
WORKDIR "/src/WealthMind"
RUN dotnet build "WealthMind.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WealthMind.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "WealthMind.dll"]