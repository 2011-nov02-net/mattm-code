#base image + dependencies
FROM mcr.microsoft.com/dotnet/runtime:5.0

#copy in published stuff
COPY publish/ /app

#configure command to start containers
CMD dotnet /app/DockerConsole.dll

# To Run:
# 1. dotnet publish -o publish
# 2. docker build -t docker-console
# 3. docker run