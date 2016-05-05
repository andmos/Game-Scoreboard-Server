# Game-Scoreboard-Server
A simple server to store game highscores

Try it out with [Vagrant](https://www.vagrantup.com/):

    vagrant up
    vagrant ssh
    cd /vagrant/
    chmod +x buildServer.sh
    ./buildServer.sh run-dockerAndLinkToDb
    curl localhost:5000/v1/api/ping

[API](doc/API.md)

[![Build Status](https://travis-ci.org/andmos/Game-Scoreboard-Server.svg?branch=master)](https://travis-ci.org/andmos/Game-Scoreboard-Server)
