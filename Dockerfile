# Use the standard ASP.NET Core runtime as the base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["FinanceTrack.csproj", "./"]
RUN dotnet restore "./FinanceTrack.csproj"

# Copy everything else and build the app
COPY . .
WORKDIR "/src/."
RUN dotnet build "FinanceTrack.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "FinanceTrack.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Ensure the data directory exists for the SQLite volume mount
RUN mkdir -p /app/data
ENTRYPOINT ["dotnet", "FinanceTrack.dll"]
