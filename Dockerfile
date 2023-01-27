# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .

RUN dotnet restore "./Task4/BlazorApp/BlazorApp.csproj" --disable-parallel
RUN dotnet publish "./Task4/BlazorApp/BlazorApp.csproj" -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "BlazorApp.dll"]