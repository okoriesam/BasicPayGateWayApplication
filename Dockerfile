FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj file and restore
COPY BasicPaymentGateway.csproj ./
RUN dotnet restore BasicPaymentGateway.csproj

# Copy the rest of the app and publish
COPY . .
RUN dotnet publish BasicPaymentGateway.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BasicPaymentGateway.dll"]
