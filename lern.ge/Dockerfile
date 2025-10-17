# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# The USER $APP_UID line is fine, but for Render simplicity, we can sometimes omit it if $APP_UID is not defined
# USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# --- BUILD STAGE ---
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. COPY the project file (.csproj) to WORKDIR /src.
# Since Render's Root Directory is "lern.ge", we only need the project file name.
COPY ["lern.ge.csproj", "."]

# 2. Restore dependencies
RUN dotnet restore "lern.ge.csproj"

# 3. Copy the rest of the source code
COPY . .

# 4. Build the project
RUN dotnet build "lern.ge.csproj" -c $BUILD_CONFIGURATION -o /app/build

# --- PUBLISH STAGE ---
# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
# Changed from dotnet build to dotnet publish (this is the correct command for final deployment)
RUN dotnet publish "lern.ge.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- FINAL STAGE ---
# This stage is used in production or when running from VS in regular mode
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# The application entry point (make sure this is the correct DLL name)
ENTRYPOINT ["dotnet", "lern.ge.dll"]
