# STEP 1: Use the .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /src
EXPOSE 80

# Copy the solution and project files
COPY ["./PersonalFinanceApplication-API/PersonalFinanceApplication-API.csproj", "PersonalFinanceApplication-API/"]
COPY ["./PersonalFinanceApplication-DAL/PersonalFinanceApplication-DAL.csproj", "PersonalFinanceApplication-DAL/"]
COPY ["./PersonalFinanceApplication-DomainModels/PersonalFinanceApplication-DomainModels.csproj", "PersonalFinanceApplication-DomainModels/"]
COPY ["./PersonalFinanceApplication-DTO/PersonalFinanceApplication-DTO.csproj", "PersonalFinanceApplication-DTO/"]
COPY ["./PersonalFinanceApplication-Exceptions/PersonalFinanceApplication-Exceptions.csproj", "PersonalFinanceApplication-Exceptions/"]
COPY ["./PersonalFinanceApplication-Mappers/PersonalFinanceApplication-Mappers.csproj", "PersonalFinanceApplication-Mappers/"]
COPY ["./PersonalFinanceApplication-MBService/PersonalFinanceApplication-MBService.csproj", "PersonalFinanceApplication-MBService/"]
COPY ["./PersonalFinanceApplication-Servicies/PersonalFinanceApplication-Services.csproj", "PersonalFinanceApplication-Servicies/"]
COPY ["./PFA-gRPCClient/PFA-gRPCClient.csproj", "PFA-gRPCClient/"]

# Restore NuGet packages
WORKDIR /src/PersonalFinanceApplication-API
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
ENTRYPOINT ["dotnet", "PersonalFinanceApplication-API.dll"]
 
