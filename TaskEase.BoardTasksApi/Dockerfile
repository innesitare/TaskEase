FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaskEase.BoardTasksApi/TaskEase.BoardTasksApi.csproj", "TaskEase.BoardTasksApi/"]
COPY ["TaskEase.Core/TaskEase.Core.csproj", "TaskEase.Core/"]
COPY ["TaskEase.Domain/TaskEase.Domain.csproj", "TaskEase.Domain/"]
COPY ["TaskEase.Infrastructure/TaskEase.Infrastructure.csproj", "TaskEase.Infrastructure/"]
RUN dotnet restore "TaskEase.BoardTasksApi/TaskEase.BoardTasksApi.csproj"
COPY . .
WORKDIR "/src/TaskEase.BoardTasksApi"
RUN dotnet build "TaskEase.BoardTasksApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskEase.BoardTasksApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskEase.BoardTasksApi.dll"]
