#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PubFinderGeneral.Data.Api/PubFinderGeneral.Data.Api.csproj", "PubFinderGeneral.Data.Api/"]
RUN dotnet restore "PubFinderGeneral.Data.Api/PubFinderGeneral.Data.Api.csproj"
COPY . .
WORKDIR "/src/PubFinderGeneral.Data.Api"
RUN dotnet build "PubFinderGeneral.Data.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PubFinderGeneral.Data.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PubFinderGeneral.Data.Api.dll"]