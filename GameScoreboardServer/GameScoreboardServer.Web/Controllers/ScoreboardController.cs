using System.Collections.Generic;
using GameScoreboardServer.Web.Models;
using GameScoreboardServer.Web.Services;
using Microsoft.AspNetCore.Mvc;
namespace GameScoreboardServer.Web.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ScoreboardController : ControllerBase
    {
        private readonly IScoreBoardRepository _scoreBoardRepository;

        public ScoreboardController(IScoreBoardRepository scoreBoardRepository)
        {
            _scoreBoardRepository = scoreBoardRepository;
        }
        [HttpGet("ping")]
        public string Ping()
        {
            return "pong";
        }

        [HttpPost("addScoreBoardData")]
        public IActionResult PostScoreBoardData([FromBody] ScoreRecord scoreRecord)
        {
            var createdId = _scoreBoardRepository.AddScoreRecordToStorage(scoreRecord);

            return Created($"api/v1/scoreRecord/{scoreRecord.PlayerName}/{createdId}", new {createdId});
        }

        [HttpGet("scoreRecord/{playerName}/{recordId:int}")]
        public ScoreRecord GetScoreRecord(string playerName, int recordId)
        {
            return _scoreBoardRepository.GetScoreRecordForUsername(recordId, playerName);
        }

        [HttpGet("playerScoreBoard/{playerName}")]
        public IEnumerable<ScoreRecord> GetPlayerScoreBoard(string playerName)
        {
            return _scoreBoardRepository.GetAllScoresForUsername(playerName);
        }
        
        [HttpGet("gameScoreBoard/{gameName}/{count:int})")]
        public IEnumerable<ScoreRecord> GetGameScoreBoard(string gameName, int count)
        {
            return _scoreBoardRepository.GetScoresForGame(gameName, count);
        }
        
        [HttpGet("gameNames")]
        public IEnumerable<string> GetGameNames()
        {
            return _scoreBoardRepository.GetAllGameNames();
        }
    }
}
