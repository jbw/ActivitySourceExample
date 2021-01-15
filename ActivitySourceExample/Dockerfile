#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ActivitySourceExample/ActivitySourceExample.csproj", "ActivitySourceExample/"]
RUN dotnet restore "ActivitySourceExample/ActivitySourceExample.csproj"
COPY . .
WORKDIR "/src/ActivitySourceExample"
RUN dotnet build "ActivitySourceExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ActivitySourceExample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ActivitySourceExample.dll"]
