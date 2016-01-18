FROM mono:latest
ADD GameScoreboardServer GameScoreboardServer 

RUN nuget restore /GameScoreboardServer/GameScoreboardServer.sln
RUN xbuild /GameScoreboardServer/GameScoreboardServer.sln 


expose 5000 

CMD mono /GameScoreboardServer/GameScoreboardServer/bin/Debug/GameScoreboardServer.exe
