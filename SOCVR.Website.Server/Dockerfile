FROM microsoft/dotnet:1.1.1-runtime
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "SOCVR.Website.Server.dll"]

RUN apt-get update && apt-get install -y git

COPY publish/ /app
