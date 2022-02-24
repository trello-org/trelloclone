FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TrelloClone.csproj", "/src"]
RUN dotnet restore "NETToDoDemo/NETToDoDemo.csproj"
COPY . .
WORKDIR "/src/NETToDoDemo"
RUN dotnet build "NETToDoDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NETToDoDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet NETToDoDemo.dll
