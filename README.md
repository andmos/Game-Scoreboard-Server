# Game-Scoreboard-Server

A simple server to store game highscores

## Run the server from `dotnet` / VSStudio / Rider With reverse proxy, aka development mode

The `docker-compose` file contains an `nginx` config that can be used alongside `mkcert` to get and reverse-proxy with trusted HTTPS certificate:

* Install [mkcert](https://github.com/FiloSottile/mkcert)
* `$ cd revserse-proxy/ && mkcert install && mkcert localhost`
* `$ cd ..`
* `$ dotnet run --project GameScoreboardServer/GameScoreboardServer.Web/GameScoreboardServer.Web.csproj` or fire up the project in VS, Rider, etc.
* In another terminal: `$ docker-compose up nginx`

The solution is now reachable on `https://localhost:5000/swagger/index.html`

## Run the entire server with `docker-compose`

* Install [mkcert](https://github.com/FiloSottile/mkcert)
* `$ cd revserse-proxy/ && mkcert install && mkcert localhost`
* `$ cd ..`
* `$ docker-compose build && docker-compose up`

The solution is now reachable on `https://localhost:5000/swagger/index.html`