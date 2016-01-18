FROM mono:latest
ADD GameScoreboardServer GameScoreboardServer 

expose 5000 

CMD mono /GameScoreboardServer/GameScoreboardServer/bin/Debug/GameScoreboardServer.exe
