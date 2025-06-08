FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EventService/EventService.csproj", "EventService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "EventService/EventService.csproj"
COPY . .
WORKDIR "/src/EventService"
RUN dotnet build "EventService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "EventService.dll"] 