#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install curl -y
RUN apt-get install libreoffice -y

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DomainStorm.Project.TWCrepair.Report.Web/DomainStorm.Project.TWCrepair.Report.Web.csproj", "DomainStorm.Project.TWCrepair.Report.Web/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.Authentication/DomainStorm.Framework.Authentication.csproj", "DomainStorm.Framework/DomainStorm.Framework.Authentication/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.Caching/DomainStorm.Framework.Caching.csproj", "DomainStorm.Framework/DomainStorm.Framework.Caching/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework/DomainStorm.Framework.csproj", "DomainStorm.Framework/DomainStorm.Framework/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.BlazorComponent/DomainStorm.Framework.BlazorComponent.csproj", "DomainStorm.Framework/DomainStorm.Framework.BlazorComponent/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.Dapr/DomainStorm.Framework.Dapr.csproj", "DomainStorm.Framework/DomainStorm.Framework.Dapr/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.EventSourcing/DomainStorm.Framework.EventSourcing.csproj", "DomainStorm.Framework/DomainStorm.Framework.EventSourcing/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.LibreOffice/DomainStorm.Framework.LibreOffice.csproj", "DomainStorm.Framework/DomainStorm.Framework.LibreOffice/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.RazorEngine/DomainStorm.Framework.RazorEngine.csproj", "DomainStorm.Framework/DomainStorm.Framework.RazorEngine/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.SqlDb/DomainStorm.Framework.SqlDb.csproj", "DomainStorm.Framework/DomainStorm.Framework.SqlDb/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.WebApi/DomainStorm.Framework.WebApi.csproj", "DomainStorm.Framework/DomainStorm.Framework.WebApi/"]
COPY ["DomainStorm.Project.TWCrepair.Repository/DomainStorm.Project.TWCrepair.Repository.csproj", "DomainStorm.Project.TWCrepair.Repository/"]
COPY ["DomainStorm.Project.TWCrepair.Shared/DomainStorm.Project.TWCrepair.Shared.csproj", "DomainStorm.Project.TWCrepair.Shared/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.Client.ProcessClient/DomainStorm.Framework.Client.ProcessClient.csproj", "DomainStorm.Framework/DomainStorm.Framework.Client.ProcessClient/"]
COPY ["DomainStorm.Report.BlazorComponent/DomainStorm.Report.BlazorComponent.csproj", "DomainStorm.Report.BlazorComponent/"]
RUN dotnet restore "./DomainStorm.Project.TWCrepair.Report.Web/DomainStorm.Project.TWCrepair.Report.Web.csproj"
COPY . .
WORKDIR "/src/DomainStorm.Project.TWCrepair.Report.Web"
RUN dotnet build "./DomainStorm.Project.TWCrepair.Report.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DomainStorm.Project.TWCrepair.Report.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomainStorm.Project.TWCrepair.Report.Web.dll"]