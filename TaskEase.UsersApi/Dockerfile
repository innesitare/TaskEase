FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaskEase.UsersApi/TaskEase.UsersApi.csproj", "TaskEase.UsersApi/"]
COPY ["TaskEase.Core/TaskEase.Core.csproj", "TaskEase.Core/"]
COPY ["TaskEase.Domain/TaskEase.Domain.csproj", "TaskEase.Domain/"]
COPY ["TaskEase.Infrastructure/TaskEase.Infrastructure.csproj", "TaskEase.Infrastructure/"]
RUN dotnet restore "TaskEase.UsersApi/TaskEase.UsersApi.csproj"
COPY . .
WORKDIR "/src/TaskEase.UsersApi"
RUN dotnet build "TaskEase.UsersApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskEase.UsersApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskEase.UsersApi.dll"]
