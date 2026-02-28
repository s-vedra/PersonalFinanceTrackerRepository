FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
EXPOSE 80

COPY ["./PersonalFinanceApplicationExchangeRates-API/PersonalFinanceApplicationExchangeRates-API.csproj", "PersonalFinanceApplicationExchangeRates-API/"]
COPY ["./PersonalFinanceApplication-DtoModels/PersonalFinanceApplication-DtoModels.csproj", "PersonalFinanceApplication-DtoModels/"]
COPY ["./PFA-Services/PFA-Services.csproj", "PFA-Services/"]

WORKDIR /src/PersonalFinanceApplicationExchangeRates-API
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app/publish --verbosity normal

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PersonalFinanceApplicationExchangeRates-API.dll"]
 
