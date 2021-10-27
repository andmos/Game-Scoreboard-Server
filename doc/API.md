GameScoreboardServer API
===

### Model
    {
        "gameName": "",
        "playerName": "",
        "score": 0,
    }

### GET

    /api/v1/ping
    # Returns pong

    /api/v1/gameScoreBoard/{gameName}/{count}
    # Returns top [count] list from [gameName]

    /api/v1/playerScoreBoard/{playerName}
    # Returns all highscores for [playerName]

    /api/v1/gameNames
    # Returns list of all the unique game names

    /api/v1/scoreRecord/{playerName}/{recordId}
    # Returns single score record for player

### POST

    /api/v1/addScoreBoardData/
    # Call with model to add highscores.
