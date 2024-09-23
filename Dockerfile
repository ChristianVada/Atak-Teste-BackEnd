FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Atak2.csproj", "./"] 
RUN dotnet restore "./Atak2.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Atak2.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM build AS publish
RUN dotnet publish "Atak2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atak2.dll"]
