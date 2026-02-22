# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY ApplyVault.Api/ApplyVault.Api.csproj ApplyVault.Api/
COPY ApplyVault.Application/ApplyVault.Application.csproj ApplyVault.Application/
COPY ApplyVault.Domain/ApplyVault.Domain.csproj ApplyVault.Domain/
COPY ApplyVault.Infrastructure/ApplyVault.Infrastructure.csproj ApplyVault.Infrastructure/

RUN dotnet restore "ApplyVault.Api/ApplyVault.Api.csproj"

# copy everything else and publish
COPY . .
RUN dotnet publish "ApplyVault.Api/ApplyVault.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Render expects 0.0.0.0 and uses PORT (default 10000)
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
EXPOSE 10000

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ApplyVault.Api.dll"]