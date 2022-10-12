#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdjusterAssignment.API/AdjusterAssignment.API.csproj", "AdjusterAssignment.API/"]
RUN dotnet restore "AdjusterAssignment.API/AdjusterAssignment.API.csproj"
COPY . .
WORKDIR "/src/AdjusterAssignment.API"
RUN dotnet build "AdjusterAssignment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdjusterAssignment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdjusterAssignment.API.dll"]
