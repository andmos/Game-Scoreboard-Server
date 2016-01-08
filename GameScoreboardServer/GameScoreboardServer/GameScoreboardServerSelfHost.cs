using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;
using System.Configuration;
using System.Diagnostics;

namespace GameScoreboardServer
{
    public class GameScoreboardServerSelfHost
    {

        public bool Start()
        {
            return true; 
        }
        public bool Stop()
        {
            return false; 
        }
       
    }
}
