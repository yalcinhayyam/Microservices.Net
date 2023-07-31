# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
# ARG BUILD=Catalogue
WORKDIR /src
COPY Services/Catalogue/Catalogue.Api/*.csproj Services/Catalogue/Catalogue.Api/
COPY Services/Catalogue/Catalogue.Domain/*.csproj Services/Catalogue/Catalogue.Domain/
COPY Services/Catalogue/Catalogue.Application/*.csproj Services/Catalogue/Catalogue.Application/
COPY Services/Catalogue/Catalogue.Infrastructure/*.csproj Services/Catalogue/Catalogue.Infrastructure/
COPY Services/Catalogue/Catalogue.Persistence/*.csproj Services/Catalogue/Catalogue.Persistence/
COPY Core/*.csproj Core/
COPY Contracts/*.csproj Contracts/
COPY Shared/*.csproj Shared/

RUN dotnet restore Services/Catalogue/Catalogue.Api/*.csproj 

COPY Services/Catalogue/Catalogue.Api/ Services/Catalogue/Catalogue.Api/
COPY Services/Catalogue/Catalogue.Domain/ Services/Catalogue/Catalogue.Domain/
COPY Services/Catalogue/Catalogue.Application/ Services/Catalogue/Catalogue.Application/
COPY Services/Catalogue/Catalogue.Infrastructure/ Services/Catalogue/Catalogue.Infrastructure/
COPY Services/Catalogue/Catalogue.Persistence/ Services/Catalogue/Catalogue.Persistence/
COPY Core/ Core/
COPY Contracts/ Contracts/
COPY Shared/ Shared/

RUN dotnet build Services/Catalogue/Catalogue.Api/Catalogue.Api.csproj -c Release -o /app/build
RUN dotnet publish Services/Catalogue/Catalogue.Api/ -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Catalogue.Api.dll"]

