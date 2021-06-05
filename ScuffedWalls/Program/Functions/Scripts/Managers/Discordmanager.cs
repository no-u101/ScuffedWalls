using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using DiscordRPC;

namespace ConsoleApp1.Scripts
{
    
    class Discordmanager
    {
        static DiscordRpcClient client;
        
        public Discordmanager()
        {
            if(client == null)
            {
                client = new DiscordRpcClient("834134177853997096");
                client.OnError += (object sender, DiscordRPC.Message.ErrorMessage args) => { Console.WriteLine($"RPC Error: {args.Message}"); };
                client.Initialize();
                client.SetPresence(new RichPresence()
                {
                    Details = "inbetween",
                    State = "?????",
                    Timestamps = Timestamps.Now,

                });
            }
        }

        public async Task autoupdateRPC()
        {
            while (true)
            {
                if (string.IsNullOrEmpty(Program.save.player.savelocation.currentlocal))
                {
                    client.UpdateDetails("inbetween");
                    await Task.Delay(1000);
                }
                switch (Program.save.player.savelocation.currentlocal.ToUpper())
                {
                    case "CAVE":
                        client.UpdateDetails("In the cave");
                        break;
                    case "LIVINGROOM":
                        client.UpdateDetails("In the Living Room");
                        break;
                    case "DUNGEON":
                        client.UpdateDetails("In the Dungeon");
                        break;
                    case "ATTIC":
                        client.UpdateDetails("In the Attic");
                        break;

                }
                client.UpdateState(Program.save.player.stat.Playerhealth.ToString() + " Hp");
                await Task.Delay(4269);
            }
        }
    }
}
