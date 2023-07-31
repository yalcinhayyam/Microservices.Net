# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
COPY Subscriber/*.csproj Subscriber/
COPY Core/Core.EventBus/*.csproj Core/Core.EventBus/
RUN dotnet restore Subscriber/*.csproj 

COPY Subscriber/ Subscriber/
COPY Core/Core.EventBus/ Core/Core.EventBus/
RUN dotnet build Subscriber/Subscriber.csproj -c Release -o /app/build
RUN dotnet publish Subscriber/ -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Subscriber.dll"]

