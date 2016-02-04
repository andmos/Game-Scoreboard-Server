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

function run-docker()
{
    sudo docker run -dt -p 5000:5000 gamescoreboardserver
}

function run-dockerAndLinkToDb()
{
    cd Database
    chmod +x setUpDockerMysqlDatabase
    ./setUpDockerMysqlDatabase
    sudo docker build -t gamescoreboardserver ..
    sudo docker run -dt --link gamescoreboarddb:gamescoreboarddb -p 5000:5000 gamescoreboardserver
    cd ..
}

function run-integration-docker()
{
   run-dockerAndLinkToDb
   mono GameScoreboardServer/packages/NUnit.Runners.2.6.4/tools/nunit-console.exe GameScoreboardServer/TestGameScoreboardServer/bin/Debug/Test*.dll
}


"$@"
