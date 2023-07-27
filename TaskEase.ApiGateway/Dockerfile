FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaskEase.ApiGateway/TaskEase.ApiGateway.csproj", "TaskEase.ApiGateway/"]
RUN dotnet restore "TaskEase.ApiGateway/TaskEase.ApiGateway.csproj"
COPY . .
WORKDIR "/src/TaskEase.ApiGateway"
RUN dotnet build "TaskEase.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskEase.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskEase.ApiGateway.dll"]
