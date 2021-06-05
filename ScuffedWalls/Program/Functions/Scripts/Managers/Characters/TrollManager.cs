using System;
using ConsoleApp1.Scripts.Selectors;

namespace ConsoleApp1.Scripts.Managers.Characters
{
    public static class TrollManager
    {
        public static void TrollConvo()
        {
            if (Program.save.player.stat.Playercharisma >= 10)
            {
                bool Convobool = true;
                if (Program.save.progress.npc.hasSadieTroll)
                {
                    Program.save.progress.TrollConvo.Remove("You are what they heard making noise");
                    Program.save.progress.TrollConvo.Add("You are what they heard making noise");
                    Program.save.invsave();
                }

                while (Convobool)
                {
                    if (Program.save.player.inventory.storage.Contains("Meat Slab"))
                    {
                        Program.save.progress.TrollConvo.Remove("Offer the Meat Slab");
                        Program.save.progress.TrollConvo.Add("Offer the Meat Slab");
                        Program.save.invsave();
                    }

                    for (int i = 0; i < Program.save.progress.TrollConvo.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + Program.save.progress.TrollConvo[i]);
                    }

                    string answer = Console.ReadLine();
                    switch (answer.ToUpper())
                    {
                        case "HOW DID YOU GET HERE":
                            PrintTroll("The house was built over my cave. I have been here a long time");
                            bool Convobool1 = true;
                            while (Convobool1)
                            {
                                Console.WriteLine("1. Are you willing to stop making noise \n2. Leave");
                                string answer1 = Console.ReadLine();
                                if (answer1.ToUpper() == "ARE YOU WILLING TO STOP MAKING NOISE")
                                {
                                    if (Program.save.player.stat.Playercharisma >= 12)
                                    {
                                        PrintTroll("You seem trustworthy. I will do my best");
                                        Program.save.progress.npc.istrolldealt = true;
                                        Program.save.invsave();
                                        Convobool1 = false;
                                        Convobool = false;
                                    }
                                    else
                                    {
                                        PrintTroll("No, Why would I do that?");
                                    }
                                }
                                else
                                {
                                    Convobool1 = false;
                                }
                            }
                            break;
                        case "WHAT IS YOUR NAME":
                            PrintTroll("I do not remember. I have been here a long time");
                            bool Convobool2 = true;
                            while (Convobool2)
                            {
                                Console.WriteLine("1. Are you willing to stop making noise \n2. Leave");
                                string answer1 = Console.ReadLine();
                                if (answer1.ToUpper() == "ARE YOU WILLING TO STOP MAKING NOISE")
                                {
                                    PrintTroll("You showed me kindness. I will try'");
                                    Program.save.progress.npc.istrolldealt = true;
                                    Program.save.invsave();
                                    Convobool2 = false;
                                    Convobool = false;
                                }
                                else
                                {
                                    Convobool2 = false;
                                }
                            }
                            break;
                        case "LEAVE":
                            Convobool = false;
                            break;
                        case "YOU ARE WHAT THEY HEARD MAKING NOISE":
                            PrintTroll("Who?");
                            break;
                        case "OFFER THE MEAT SLAB":
                            Console.WriteLine("The troll takes the meat and eats it quickly");
                            PrintTroll("Thank you for this, I will try to be quiter if I have been loud");
                            Program.save.progress.npc.istrolldealt = true;
                            Program.save.invsave();
                            Convobool = false;
                            break;
                    }
                }
            }
            else if (Program.save.player.stat.Playercharisma < 10)
            {
                Console.WriteLine("The troll gets angry and hits you across the room");
            }

            if (Program.save.progress.npc.istrolldealt)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Congrats, you dealt with the troll. You should probably tell both of the old women who are upstairs");
                Console.ResetColor();
                Program.save.progress.npc.istrolldealt = true;
                Program.save.invsave();
            }
        }

        public static void TrollKombat()
        {
            int Trollhealth = 60;

            bool kombatbool = true;

            while (kombatbool)
            {
                while (Program.save.player.stat.Playerhealth > 0 && Trollhealth > 0)
                {
                    if (string.IsNullOrEmpty(Program.save.player.inventory.weapon))
                    {
                        Console.WriteLine("You have no weapon. The troll laughs and hits you really hard.");
                        Program.save.player.stat.Playerhealth = 0;
                    }
                    else
                    {
                        Console.WriteLine("How will you approach this battle?\n1.Attack with " + Program.save.player.inventory.weapon + "\n2.Dodge");
                        string answer = Console.ReadLine();
                        if (string.IsNullOrEmpty(Program.save.player.inventory.weapon))
                        {
                            Console.WriteLine("You have no weapon. The troll laughs and hits you really hard.");
                            Program.save.player.stat.Playerhealth = 0;
                        }
                        else if (answer.ToUpper() == "ATTACK WITH " + Program.save.player.inventory.weapon.ToUpper())
                        {
                            Console.WriteLine("You swing at the troll with your weapon, cutting him the angered troll swings his axe at you");
                            Program.save.player.stat.Playerhealth = Program.save.player.stat.Playerhealth = 20;
                            if (Program.save.player.species.ToUpper() == "DWARF")
                            {
                                Trollhealth = Trollhealth - 30;
                            }
                            if (Program.save.player.species.ToUpper() == "HUMAN" || Program.save.player.species.ToUpper() == "ELF")
                            {
                                Trollhealth = Trollhealth - 20;
                            }
                        }
                        else if (answer.ToUpper() == "DODGE")
                        {
                            Console.WriteLine("You dodge the trolls swing, he grazes your side and you take a small about of damage");
                            if (Program.save.player.species.ToUpper() == "ELF")
                            {
                                Program.save.player.stat.Playerhealth = Program.save.player.stat.Playerhealth = 5;
                            }
                            if (Program.save.player.species.ToUpper() == "HUMAN" || Program.save.player.species.ToUpper() == "DWARF")
                            {
                                Program.save.player.stat.Playerhealth = Program.save.player.stat.Playerhealth = 10;
                            }
                        }
                    }
                }
                if (Program.save.player.stat.Playerhealth <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Program.save.player.stat.PlayerDeaths++;
                    Console.WriteLine("YOU DIED, you have died" + Program.save.player.stat.PlayerDeaths.ToString() + "\nPlease try again, Press any key to restart");
                    Program.save.player.stat.Playerhealth = 80;
                    Program.save.invsave();
                    Console.ResetColor();
                    Console.ReadLine();
                    AtticSelector.intoAttic();
                }
                if (Trollhealth <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Congrats, you killed the troll. You should probably tell both of the old women who are upstairs");
                    Console.ResetColor();
                    Program.save.progress.npc.istrolldead = true;
                    Program.save.invsave();
                    kombatbool = false;
                }
            }
        }

        public static void PrintTroll(string c)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(c);
            Console.ResetColor();
        }
    }
}
