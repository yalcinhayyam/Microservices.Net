# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
ARG BUILD=Catalogue
WORKDIR /src
COPY ${BUILD}/*.csproj ${BUILD}/
COPY Library/*.csproj Library/
RUN dotnet restore ${BUILD}/*.csproj 

COPY ${BUILD}/ ${BUILD}/
COPY Library/ Library/
RUN dotnet build ${BUILD}/${BUILD}.csproj -c Release -o /app/build
RUN dotnet publish ${BUILD}/ -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Catalogue.dll"]

