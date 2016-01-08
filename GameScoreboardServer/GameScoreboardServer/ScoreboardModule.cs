using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameScoreboardServer
{
    public class ScoreboardModule :  NancyModule 
    {
        public ScoreboardModule() : base("/api/v1")
        {
            Get["/ping"] = parameters =>
            {
                return "pong";
            }; 
        }
    }
}
