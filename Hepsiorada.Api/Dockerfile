#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Hepsiorada.Api/Hepsiorada.Api.csproj", "Hepsiorada.Api/"]
COPY ["Hepsiorada.Application/Hepsiorada.Application.csproj", "Hepsiorada.Application/"]
COPY ["Hepsiorada.Domain/Hepsiorada.Domain.csproj", "Hepsiorada.Domain/"]
COPY ["Hepsiorada.Infrastructure/Hepsiorada.Infrastructure.csproj", "Hepsiorada.Infrastructure/"]
RUN dotnet restore "Hepsiorada.Api/Hepsiorada.Api.csproj"
COPY . .
WORKDIR "/src/Hepsiorada.Api"
RUN dotnet build "Hepsiorada.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hepsiorada.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hepsiorada.Api.dll"]