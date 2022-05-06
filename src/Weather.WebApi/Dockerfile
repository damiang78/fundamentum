FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5000;https://+:5001

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["src/Weather.WebApi/Weather.WebApi.csproj", "src/Weather.WebApi/"]
RUN dotnet restore "src/Weather.WebApi/Weather.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Weather.WebApi"
RUN dotnet build "Weather.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Weather.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weather.WebApi.dll"]
