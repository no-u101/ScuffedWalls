using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ConsoleApp1.Scripts.Managers.Characters;


namespace ConsoleApp1.Scripts.Selectors
{
    class DungeonSelect
    {
        public static void intoDungeon()
        {
            Console.Clear();

            Program.save.player.savelocation.currentlocal = "Dungeon";
            Program.save.invsave();

            bool dbool = true;

            

            while (dbool)
            {
                if (Program.save.progress.npc.istrolldead && !Program.save.progress.npc.istrolldealt)
                {
                    Console.WriteLine("Entering the Dungeon you find a the corpse of the Troll. He somehow stinks worse");
                    for (int i = 0; i < Program.save.progress.DungeonOptionsD1.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + Program.save.progress.DungeonOptionsD1[i]);
                    }
                    string answer = Console.ReadLine();
                    switch (answer.ToUpper())
                    {
                        case "SEARCH THE TROLL":
                            bool axepic = true;
                            while (axepic)
                            {
                                //Shows if player does not have dagger in inventory or equiped
                                if (!Program.save.player.inventory.storage.Contains("Axe") || !Program.save.player.inventory.weapon.Contains("Axe"))
                                {
                                    Console.WriteLine("Searching the troll you fing a large Axe. Do you wish to take it[y/n]");

                                    string answer1 = Console.ReadLine();
                                    if (answer1.ToUpper() == "Y")
                                    {
                                        bool axequ = true;
                                        while (axequ)
                                        {
                                            Console.WriteLine("Do you want to equip the Axe?[y/n]");
                                            string response1 = Console.ReadLine();
                                            if (response1.ToUpper() == "Y")
                                            {
                                                Equipable.weaponequiped("axe");
                                            }
                                            axequ = false;
                                        }
                                        axepic = false;
                                    }
                                    else if (answer.ToUpper() == "N")
                                    {
                                        Console.WriteLine("You leave the troll");
                                        Program.save.invsave();
                                        axepic = false;
                                    }
                                }
                                else if (Program.save.player.inventory.storage.Contains("Axe") || !Program.save.player.inventory.weapon.Contains("Axe")) //If the player already has the dagger then it prevents the interaction 
                                {
                                    Console.WriteLine("You already have the trolls axe. You leave the troll");
                                    Program.save.invsave();
                                    axepic = false;
                                }
                            }
                            break;
                        case "CHECK THE METAL DOOR":
                            Console.WriteLine("Running past the troll you slam into the metal door. It opens and you can see the basement");
                            Program.save.progress.DungeonOptionsD1.Remove("Check the metal door");
                            Program.save.progress.DungeonOptionsD1.Add("Leave");
                            Program.save.progress.caveoptions.Remove("Try to open the door");
                            Program.save.progress.caveoptions.Remove("Enter the dark room");
                            Program.save.progress.caveoptions.Add("Enter the dark room");
                            Program.save.invsave();
                            break;
                        case "LEAVE":
                            CaveSelect.intocave();
                            break;
                    }
                }

                else if (Program.save.progress.npc.istrolldealt && !Program.save.progress.npc.istrolldead)
                {
                    for (int i = 0; i < Program.save.progress.DungeonOptionsD2.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + Program.save.progress.DungeonOptionsD2[i]);
                    }
                    string answer = Console.ReadLine();
                    switch (answer.ToUpper())
                    {
                        case "ASK THE TROLL FOR STUFF":
                            bool axepic = true;
                            while (axepic)
                            {
                                //Shows if player does not have dagger in inventory or equiped
                                if (!Program.save.player.inventory.storage.Contains("Axe") || !Program.save.player.inventory.weapon.Contains("Axe"))
                                {
                                    Console.WriteLine("The troll shows you a large Axe. Do you wish to take it[y/n]");

                                    string answer1 = Console.ReadLine();
                                    if (answer1.ToUpper() == "Y")
                                    {
                                        bool axequ = true;
                                        while (axequ)
                                        {
                                            Console.WriteLine("Do you want to equip the Axe?[y/n]");
                                            string response1 = Console.ReadLine();
                                            if (response1.ToUpper() == "Y")
                                            {
                                                Equipable.weaponequiped("axe");
                                            }
                                            axequ = false;
                                        }
                                        axepic = false;
                                    }
                                    else if (answer.ToUpper() == "N")
                                    {
                                        Console.WriteLine("You leave the troll");
                                        Program.save.invsave();
                                        axepic = false;
                                    }
                                }
                                else if (Program.save.player.inventory.storage.Contains("Axe") || !Program.save.player.inventory.weapon.Contains("Axe")) //If the player already has the dagger then it prevents the interaction 
                                {
                                    Console.WriteLine("You already have the trolls axe. You leave the troll");
                                    Program.save.invsave();
                                    axepic = false;
                                }
                            }
                            break;
                        case "CHECK THE METAL DOOR":
                            Console.WriteLine("Running past the troll you slam into the metal door. It opens and you can see the basement");
                            Program.save.progress.DungeonOptionsD2.Remove("Check the metal door");
                            Program.save.progress.DungeonOptionsD2.Add("Leave");
                            Program.save.progress.caveoptions.Remove("Try to open the door");
                            Program.save.progress.caveoptions.Remove("Enter the dark room");
                            Program.save.progress.caveoptions.Add("Enter the dark room");
                            Program.save.invsave();
                            break;
                        case "LEAVE":
                            CaveSelect.intocave();
                            break;
                    }
                }

                else if (!Program.save.progress.npc.istrolldealt && !Program.save.progress.npc.istrolldealt)
                {
                    Console.WriteLine("Entering the Dungeon you find a mighty troll facing you, He stinks very badly but his eyes show intelligence. In the far corner you can see a metal door. ");
                    for (int i = 0; i < Program.save.progress.DungeonOptionsA.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + Program.save.progress.DungeonOptionsA[i]);
                    }
                    string answer = Console.ReadLine();
                    switch (answer.ToUpper())
                    {
                        case "[TRY]TALK TO THE TROLL":
                        case "TALK TO THE TROLL":
                            TrollManager.TrollConvo();
                            break;
                        case "ATTACK THE TROLL":
                            TrollManager.TrollKombat();
                            break;
                    }
                } else if(!Program.save.progress.npc.istrolldealt && !Program.save.progress.npc.istrolldead)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("How did you get here. Fuck you");
                    var process = new ProcessStartInfo() { FileName = "https://www.youtube.com/watch?v=xUfxnDAAxHI&t=20s", UseShellExecute = true };
                    Process.Start(process);
                    Console.ResetColor();
                }
            }
        }
    }
}
