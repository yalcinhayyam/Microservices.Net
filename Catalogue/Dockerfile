# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
COPY Catalogue/*.csproj Catalogue/
COPY Library/*.csproj Library/
RUN dotnet restore Catalogue/*.csproj 
COPY . .
RUN dotnet build Catalogue/*.csproj -c Release -o /app/build
RUN dotnet publish -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Catalogue.dll"]
