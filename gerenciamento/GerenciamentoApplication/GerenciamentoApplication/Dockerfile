
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


# Use the ASP.NET runtime image as the base for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project files for all projects in the solution
COPY ["GerenciamentoApplication/GerenciamentoApplication.csproj", "GerenciamentoApplication/"]
COPY ["Gerenciamento.Application/Gerenciamento.Application.csproj", "Gerenciamento.Application/"]
COPY ["Gerenciamento.Domain/Gerenciamento.Domain.csproj", "Gerenciamento.Domain/"]
COPY ["Gerenciamento.DbAdapter/Gerenciamento.DbAdapter.csproj", "Gerenciamento.DbAdapter/"]

# Restore dependencies for all projects
RUN dotnet restore "./GerenciamentoApplication/GerenciamentoApplication.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/GerenciamentoApplication"
RUN dotnet build "./GerenciamentoApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./GerenciamentoApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "GerenciamentoApplication.dll"]

