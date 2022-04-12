#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal as build
Workdir /source
Copy . .
Run dotnet restore "./CloudCustomers.API/CloudCustomers.API.csproj" --disable-parallel
Run dotnet publish "./CloudCustomers.API/CloudCustomers.API.csproj" -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
Workdir /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "CloudCustomers.API.dll"]




