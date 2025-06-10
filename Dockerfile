# 1. Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY ["Refhub_Ir/Refhub_Ir.csproj", "Refhub_Ir/"]
COPY ["DaneshkarTeam1403.1.sln", "./"]

# Restore dependencies
RUN dotnet restore "Refhub_Ir/Refhub_Ir.csproj"

# Copy the rest of the project (important!)
COPY ./Refhub_Ir ./Refhub_Ir

WORKDIR "/src/Refhub_Ir"

# Optional: copy Docker-specific config if needed
# COPY ./Refhub_Ir/appsettings.Docker.json ./Refhub_Ir/appsettings.Docker.json

RUN dotnet build "Refhub_Ir.csproj" -c Release -o /app/build

# 2. Publish the app
FROM build AS publish
RUN dotnet publish "Refhub_Ir.csproj" -c Release -o /app/publish

# 3. Use the ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Refhub_Ir.dll"]
