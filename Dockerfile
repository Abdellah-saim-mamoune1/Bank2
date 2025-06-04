# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY *.sln .
COPY bankApI/*.csproj ./bankApI/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the application
COPY bankApI/. ./bankApI/

# Publish the application
WORKDIR /app/bankApI
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files
COPY --from=build /app/publish .

# Railway uses PORT environment variable — default to 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "bankApI.dll"]
