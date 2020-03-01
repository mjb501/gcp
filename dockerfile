FROM mcr.microsoft.com/dotnet/core/sdk:3.1
# Set the Working Directory
WORKDIR /app

# Configure the listening port to 8080 - GAE uses this
ENV ASPNETCORE_URLS http://*:8080
EXPOSE 80

# Copy the app
COPY . /app

ENTRYPOINT ["dotnet", "gcp.dll"]