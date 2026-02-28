FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
EXPOSE 80

ARG GITHUB_PAT

RUN dotnet nuget add source "https://nuget.pkg.github.com/s-vedra/index.json" \
    --name GitHub \
    --username s-vedra \
    --password $GITHUB_PAT \
    --store-password-in-clear-text

COPY ["./PersonalFinanceApplication-API/PersonalFinanceApplication-API.csproj", "PersonalFinanceApplication-API/"]
COPY ["./PersonalFinanceApplication-DAL/PersonalFinanceApplication-DAL.csproj", "PersonalFinanceApplication-DAL/"]
COPY ["./PersonalFinanceApplication-DomainModels/PersonalFinanceApplication-DomainModels.csproj", "PersonalFinanceApplication-DomainModels/"]
COPY ["./PersonalFinanceApplication-DTO/PersonalFinanceApplication-DTO.csproj", "PersonalFinanceApplication-DTO/"]
COPY ["./PersonalFinanceApplication-Exceptions/PersonalFinanceApplication-Exceptions.csproj", "PersonalFinanceApplication-Exceptions/"]
COPY ["./PersonalFinanceApplication-Mappers/PersonalFinanceApplication-Mappers.csproj", "PersonalFinanceApplication-Mappers/"]
COPY ["./PersonalFinanceApplication-MBService/PersonalFinanceApplication-MBService.csproj", "PersonalFinanceApplication-MBService/"]
COPY ["./PersonalFinanceApplication-Servicies/PersonalFinanceApplication-Services.csproj", "PersonalFinanceApplication-Servicies/"]
COPY ["./PFA-gRPCClient/PFA-gRPCClient.csproj", "PFA-gRPCClient/"]

WORKDIR /src/PersonalFinanceApplication-API
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app/publish --verbosity normal

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PersonalFinanceApplication-API.dll"]