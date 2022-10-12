#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdjusterAssignment.Client/AdjusterAssignment.Client.csproj", "AdjusterAssignment.Client/"]
RUN dotnet restore "AdjusterAssignment.Client/AdjusterAssignment.Client.csproj"
COPY . .
WORKDIR "/src/AdjusterAssignment.Client"
RUN dotnet build "AdjusterAssignment.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdjusterAssignment.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdjusterAssignment.Client.dll"]