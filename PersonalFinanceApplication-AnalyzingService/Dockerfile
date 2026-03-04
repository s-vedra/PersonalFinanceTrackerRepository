FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
EXPOSE 80

ARG GITHUB_PAT

RUN dotnet nuget add source "https://nuget.pkg.github.com/s-vedra/index.json" \
    --name GitHub \
    --username s-vedra \
    --password $GITHUB_PAT \
    --store-password-in-clear-text

COPY ["./PersonalFinanceApplication-AnalyzingService/PersonalFinanceApplication-AnalyzingService.csproj", "PersonalFinanceApplication-AnalyzingService/"]
COPY ["./PFA-DAL/PFA-DAL.csproj", "PFA-DAL/"]
COPY ["./PFA-DM/PFA-DM.csproj", "PFA-DM/"]
COPY ["./PFA-DTO/PFA-DTO.csproj", "PFA-DTO/"]
COPY ["./PFA-Exceptions/PFA-Exceptions.csproj", "PFA-Exceptions/"]
COPY ["./PFA-gRPCServer/PFA-gRPCServer.csproj", "PFA-gRPCServer/"]
COPY ["./PFA-Mappers/PFA-Mappers.csproj", "PFA-Mappers/"]
COPY ["./PFA-Services/PFA-Services.csproj", "PFA-Services/"]


WORKDIR /src/PersonalFinanceApplication-AnalyzingService
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app/publish --verbosity normal

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PersonalFinanceApplication-AnalyzingService.dll"]
 
 
