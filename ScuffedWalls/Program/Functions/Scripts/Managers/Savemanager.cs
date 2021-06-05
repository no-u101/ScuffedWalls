using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1.Scripts;
using ConsoleApp1.Scripts.Selectors;

namespace ConsoleApp1.Scripts
{
    public static class Savemanager
    {
        public static void gotopoint()
        {
            Console.ResetColor();
            Console.Clear();
            if (string.IsNullOrEmpty(Program.save.player.savelocation.currentlocal))
            {
                CaveSelect.intocave();
            }
            switch (Program.save.player.savelocation.currentlocal.ToUpper())
            {
                case "CAVE":
                    CaveSelect.intocave();
                    break;
                case "LIVINGROOM":
                    LRSelect.intoLR();
                    break;
                case "DUNGEON":
                    DungeonSelect.intoDungeon();
                    break;
                case "ATTIC":
                    AtticSelector.intoAttic();
                    break;
            }
        }
    }
}
