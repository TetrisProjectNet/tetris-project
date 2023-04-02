#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tetris-backend/tetris-backend.csproj", "tetris-backend/"]
RUN dotnet restore "tetris-backend/tetris-backend.csproj"
COPY . .
WORKDIR "/src/tetris-backend"
RUN dotnet build "tetris-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tetris-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tetris-backend.dll"]