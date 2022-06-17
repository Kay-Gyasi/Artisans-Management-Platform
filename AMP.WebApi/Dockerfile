#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AMP.WebApi/AMP.WebApi.csproj", "AMP.WebApi/"]
COPY ["AMP.Persistence/AMP.Persistence.csproj", "AMP.Persistence/"]
COPY ["AMP.Shared.Domain/AMP.Shared.Domain.csproj", "AMP.Shared.Domain/"]
COPY ["AMP.Shared.Persistence/AMP.Shared.Persistence.csproj", "AMP.Shared.Persistence/"]
COPY ["AMP.Processors/AMP.Processors.csproj", "AMP.Processors/"]
COPY ["AMP.Domain/AMP.Domain.csproj", "AMP.Domain/"]
COPY ["AMP.Application/AMP.Application.csproj", "AMP.Application/"]
RUN dotnet restore "AMP.WebApi/AMP.WebApi.csproj"
COPY . .
WORKDIR "/src/AMP.WebApi"
RUN dotnet build "AMP.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AMP.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AMP.WebApi.dll"]