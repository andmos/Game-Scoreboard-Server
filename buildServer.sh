#! /bin/bash 

# I guess I will scrap this later.

function build-local()
{
    nuget restore GameScoreboardServer/GameScoreboardServer.sln
    xbuild GameScoreboardServer/GameScoreboardServer.sln
}

function build-docker()
{
    sudo docker build -t gamescoreboardserver .
}


"$@"
