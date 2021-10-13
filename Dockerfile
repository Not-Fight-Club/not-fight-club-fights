FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FightsApi/FightsApi.csproj", "FightsApi/"]
COPY ["FightsApi_Test/FightsApi_Test.csproj", "FightsApi_Test/"]
RUN dotnet restore "FightsApi\FightsApi.csproj"
RUN dotnet restore "FightsApi_Test\FightsApi_Test.csproj"
COPY . .
WORKDIR "/src/FightsApi"
RUN dotnet build "FightsApi.csproj" -c Release -o /app/build
RUN dotnet build "./FightsApi_Test/FightsApi_Test.csproj" -c Release -o /app/build

RUN dotnet test "./FightsApi_Test/FightsApi_Test.csproj" --logger "trx;LogFileName=FightsApi_Test.trx" 

FROM build AS publish
RUN dotnet publish "FightsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FightsApi.dll"]
