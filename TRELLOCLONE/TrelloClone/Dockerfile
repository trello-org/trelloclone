FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./TrelloClone/TrelloClone.csproj",
      "./DomainLayer/Domain.csproj",
      "./Repository/Repository.csproj",
      "./Application/Application.csproj",
      "./TrelloCloneTests/TrelloCloneTests.csproj",
      "ProjectFolder/"]
RUN dotnet restore "./*/*.csproj"
COPY . .
WORKDIR "/src/ProjectFolder"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet **.dll
