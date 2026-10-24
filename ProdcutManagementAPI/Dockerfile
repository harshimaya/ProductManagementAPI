#build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src
COPY ["ProdcutManagementAPI.csproj", "."]
RUN dotnet restore "ProdcutManagementAPI.csproj"

COPY . .
RUN dotnet build "ProdcutManagementAPI.csproj" -c Release -o /app/build --no-restore
RUN dotnet publish "ProdcutManagementAPI.csproj" -c Release -o /app/publish  /p:UseAppHost=false --no-restore

#runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8081
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProdcutManagementAPI.dll"]
