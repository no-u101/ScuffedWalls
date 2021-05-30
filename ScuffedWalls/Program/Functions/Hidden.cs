using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DiscordRPC;
using ScuffedWalls.Functions;

namespace ScuffedWalls.Functions
{
    [ScuffedFunction("Hidden")]
    class Hidden : SFunction
    {
        public override void Run()
        {
            RPC.ishidden = true;
        }
    }
}