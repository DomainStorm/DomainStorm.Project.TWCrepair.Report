#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install curl -y
RUN apt-get install libreoffice -y

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DomainStorm.Project.TWC.Report.Web/DomainStorm.Project.TWC.Report.Web.csproj", "DomainStorm.Project.TWC.Report.Web/"]
COPY ["DomainStorm.Framework/DomainStorm.Framework.RazorEngine/DomainStorm.Framework.RazorEngine.csproj", "DomainStorm.Framework/DomainStorm.Framework.RazorEngine/"]
COPY ["DomainStorm.Report.BlazorComponent/DomainStorm.Report.BlazorComponent.csproj", "DomainStorm.Report.BlazorComponent/"]
RUN dotnet restore "DomainStorm.Project.TWC.Report.Web/DomainStorm.Project.TWC.Report.Web.csproj"
COPY . .
WORKDIR "/src/DomainStorm.Project.TWC.Report.Web"
RUN dotnet build "DomainStorm.Project.TWC.Report.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DomainStorm.Project.TWC.Report.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomainStorm.Project.TWC.Report.Web.dll"]