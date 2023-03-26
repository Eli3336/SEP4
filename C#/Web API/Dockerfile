FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Web API/Web API.csproj", "Web API/"]
RUN dotnet restore "Web API/Web API.csproj"
COPY . .
WORKDIR "/src/Web API"
RUN dotnet build "Web API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web API.dll"]
