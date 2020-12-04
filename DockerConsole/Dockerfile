#base image + dependencies
FROM mcr.microsoft.com/dotnet/sdk:5.0
#can use source code with the sdk instead of runtime

WORKDIR /app
#copy in published stuff
COPY . ./
# copy entire directory as source code, see above

#run runs during 'docker build'
RUN dotnet publish -o publish

#configure command to start containers, this runs after build, start of 'docker run'
CMD dotnet /app/publish/DockerConsole.dll





# To Run:
# 1. dotnet publish -o publish
# 2. docker build -t docker-console .
# 3. docker run