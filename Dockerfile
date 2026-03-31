FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY MarketPrices.sln .
COPY MarketPrices.API/MarketPrices.API.csproj MarketPrices.API/
COPY MarketPrices.Application/MarketPrices.Application.csproj MarketPrices.Application/
COPY MarketPrices.Domain/MarketPrices.Domain.csproj MarketPrices.Domain/
COPY MarketPrices.Infrastructure/MarketPrices.Infrastructure.csproj MarketPrices.Infrastructure/
COPY MarketPrices.Presentation/MarketPrices.Presentation.csproj MarketPrices.Presentation/

RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS result
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
ENTRYPOINT [ "dotnet", "MarketPrices.API.dll" ]