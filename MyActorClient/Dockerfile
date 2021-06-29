#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyActorClient/MyActorClient.csproj", "MyActorClient/"]
RUN dotnet restore "MyActorClient/MyActorClient.csproj"
COPY . .
WORKDIR "/src/MyActorClient"
RUN dotnet build "MyActorClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyActorClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyActorClient.dll"]