#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Zamat.Sample.Cli/Zamat.Sample.Cli.csproj"
COPY . .
WORKDIR "/src/src/Zamat.Sample.Cli"
RUN dotnet build "Zamat.Sample.Cli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zamat.Sample.Cli.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zamat.Sample.Cli.dll", "initialize"]