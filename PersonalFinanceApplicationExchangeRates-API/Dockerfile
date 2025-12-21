# STEP 1: Use the .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /src
EXPOSE 80

# Copy the solution and project files
COPY ["./PersonalFinanceApplicationExchangeRates-API/PersonalFinanceApplicationExchangeRates-API.csproj", "PersonalFinanceApplicationExchangeRates-API/"]
COPY ["./PersonalFinanceApplication-DtoModels/PersonalFinanceApplication-DtoModels.csproj", "PersonalFinanceApplication-DtoModels/"]
COPY ["./PFA-Services/PFA-Services.csproj", "PFA-Services/"]

# Restore NuGet packages
WORKDIR /src/PersonalFinanceApplicationExchangeRates-API
RUN dotnet restore

# Copy all other files from the current directory to the container's /src directory
COPY . .

# Publish the application to the /app/publish folder inside the container
RUN dotnet publish -c Release -o /app/publish --verbosity normal

# STEP 2: Use the .NET runtime for running the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Set the working directory inside the container
WORKDIR /app

# Copy the published files from the build stage to the runtime stage
COPY --from=build /app/publish .

# Define the entry point to run the application
ENTRYPOINT ["dotnet", "PersonalFinanceApplicationExchangeRates-API.dll"]
 
