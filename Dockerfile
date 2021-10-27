FROM mcr.microsoft.com/dotnet/sdk:5.0 as builder
WORKDIR app

ADD GameScoreboardServer GameScoreboardServer 

RUN dotnet restore GameScoreboardServer/GameScoreboardServer.Web/GameScoreboardServer.Web.csproj

RUN dotnet publish GameScoreboardServer/GameScoreboardServer.Web/GameScoreboardServer.Web.csproj -c Release -o publish 

FROM mcr.microsoft.com/dotnet/aspnet:5.0.10-alpine3.13

ENV ASPNETCORE_ENVIRONMENT Production
WORKDIR /app

COPY --from=builder /app/publish/ .

EXPOSE 8080

ENTRYPOINT ["dotnet", "GameScoreboardServer.Web.dll", "--urls", "http://*:8080"]
