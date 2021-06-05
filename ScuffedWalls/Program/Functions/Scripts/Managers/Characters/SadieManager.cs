using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Scripts.Selectors
{
    public static class SadieManager
    {
        public static void SadieConvo()
        {

            bool sadieconvobool = true;

            while (sadieconvobool)
            {
                if (Program.save.progress.npc.istrolldead || Program.save.progress.npc.istrolldealt)
                {
                    Program.save.progress.SadieConvo.Remove("I dealt with a troll downstairs");
                    if (!Program.save.progress.npc.isSadieok)
                    {
                        Program.save.progress.SadieConvo.Add("I dealt with a troll downstairs");
                    }
                    Program.save.invsave();
                }
                Console.WriteLine("You approach the old woman");
                for (int i = 0; i < Program.save.progress.SadieConvo.Count; i++)
                {
                    Console.WriteLine((i + 1) + "." + Program.save.progress.SadieConvo[i]);
                }
                string response = Console.ReadLine();
                switch (response.ToUpper())
                {
                    case "GREET HER":
                        if (Program.save.player.species == "Human" || Program.save.player.species == "Dwarf")
                        {
                            PrintSadie("Hello dear, I hope you enjoy your time here");
                        }
                        else if (Program.save.player.species == "Elf")
                        {
                            PrintSadie("Hello");

                        }
                        break;
                    case "ASK ABOUT THE HOUSE":
                        PrintSadie("Well, this house is very old. It was originally built after the last stellar event with only one story. As you can see we've made alot of changes since then.");
                        break;
                    case "ASK ABOUT THE UPSTAIRS":
                        PrintSadie("My parents added that when I was a little girl, Now that old hag stays up there most of the time.");
                        Program.save.progress.npc.hasSadiehag = true;
                        Program.save.invsave();
                        break;
                    case "ASK ABOUT THE DOOR DOWNSTAIRS":
                        PrintSadie("Oh that old thing, It is an old bunker made to withstand the next stellar event. I hear some loud creaking sounds down there from time to time but its probably the house setteling");
                        bool sadiedownstairsconvo = true;
                        while (sadiedownstairsconvo)
                        {
                            Console.WriteLine("1. Should the house be setteled by now \n2.Leave");
                            string response2 = Console.ReadLine();
                            if (response2.ToUpper() == "SHOULD THE HOUSE BE SETTELED BY NOW")
                            {
                                Console.Write("Sadie gives you a look... ");
                                PrintSadie("A house is a house, They all creak. Now stop being paranoid. You sound like her");
                                Program.save.progress.npc.hasSadieTroll = true;
                                Program.save.invsave();
                                sadiedownstairsconvo = false;
                            }
                            else
                            {
                                sadiedownstairsconvo = false;
                            }
                        }
                        break;
                    case "LEAVE":
                        sadieconvobool = false;
                        break;
                    case "I DEALT WITH A TROLL DOWNSTAIRS":
                        Program.save.progress.npc.isSadieok = true;
                        Program.save.invsave();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Oh dear, A troll?? ");
                        if (Program.save.progress.npc.isAnneok)
                        {
                            Console.WriteLine("Good thing you told her first... Why don't you meet us in the basement");
                        }
                        if (!Program.save.progress.npc.isAnneok)
                        {
                            Console.WriteLine("You should go tell that woman upstairs, hurry! Lets meet in the Basement.");
                        }
                        Console.ResetColor();
                        Program.save.progress.SadieConvo.Remove("I dealt a the troll in the dungeon");
                        Program.save.invsave();
                        break;
                }
            }
        }
       
        public static void  PrintSadie(string a)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(a);
            Console.ResetColor();
        }
    }
}
