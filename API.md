GameScoreboardServer API
===

### Model
    {
        "gameName": "",
        "playerName": "",
        "score": 0,
        "recordId": 0
    }


### GET

    /api/v1/ping
    # Returns pong

    /api/v1/gameScoreBoard?gameName=[gameName]&count=[count]
    # Returns top [count] list from [gameName]

    /api/v1/countHigherScores?gameName=[gameName]&score=[score]
    # Returns number of highscores higher than [score]

    /api/v1/playerScoreBoard?playerName=[playerName]
    # Returns all highscores for [playerName]

### POST

    /api/v1/addScoreBoardData/
    # Call with model to add highscores.
