using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameScoreboardServer.Models
{
    public class ScoreRecord
    {
		public string GameName { get; set; }
		public string PlayerName { get; set; }
        public int Score { get; set;  }
		public int recordId { get; set; }
    }
}
