FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY WebAppWithAuthenticationApi.csproj ./
RUN dotnet restore WebAppWithAuthenticationApi.csproj

COPY . ./
RUN dotnet publish WebAppWithAuthenticationApi.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish ./

ENV ASPNETCORE_ENVIRONMENT=Production

# Render provides PORT at runtime; 10000 is a local fallback.
CMD ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-10000} dotnet WebAppWithAuthenticationApi.dll"]

