#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["CustomerApp.WebAPI/CustomerApp.WebAPI.csproj", "CustomerApp.WebAPI/"]
COPY ["CustomerApp.Application/CustomerApp.Application.csproj", "CustomerApp.Application/"]
COPY ["CustomerApp.Domain/CustomerApp.Domain.csproj", "CustomerApp.Domain/"]
COPY ["CustomerApp.Infrastructure/CustomerApp.Infrastructure.csproj", "CustomerApp.Infrastructure/"]
COPY ["CustomerApp.Presentation/CustomerApp.Presentation.csproj", "CustomerApp.Presentation/"]
RUN dotnet restore "./CustomerApp.WebAPI/./CustomerApp.WebAPI.csproj"
COPY . .
WORKDIR "/src/CustomerApp.WebAPI"
RUN dotnet build "./CustomerApp.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CustomerApp.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerApp.WebAPI.dll"]