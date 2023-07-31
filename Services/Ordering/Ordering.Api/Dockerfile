# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
# ARG BUILD=Ordering
WORKDIR /src
COPY Services/Ordering/Ordering.Api/*.csproj Services/Ordering/Ordering.Api/
COPY Services/Ordering/Ordering.Persistence/*.csproj Services/Ordering/Ordering.Persistence/
COPY Core/*.csproj Core/
COPY Contracts/*.csproj Contracts/
COPY Shared/*.csproj Shared/

RUN dotnet restore Services/Ordering/Ordering.Api/*.csproj 

COPY Services/Ordering/Ordering.Api/ Services/Ordering/Ordering.Api/
COPY Services/Ordering/Ordering.Persistence/ Services/Ordering/Ordering.Persistence/
COPY Core/ Core/
COPY Contracts/ Contracts/
COPY Shared/ Shared/

RUN dotnet build Services/Ordering/Ordering.Api/Ordering.Api.csproj -c Release -o /app/build
RUN dotnet publish Services/Ordering/Ordering.Api/ -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]

