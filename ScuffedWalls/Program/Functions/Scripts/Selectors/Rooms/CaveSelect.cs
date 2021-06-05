using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1.Scripts.Selectors
{
    public static class CaveSelect
    {
        public static void intocave()
        {
            Console.Clear();

            if (Program.save.progress.npc.isAnneok && Program.save.progress.npc.isSadieok)
            {
                Program.save.invsave();
                Program.save.progress.npc.isgameover = true;
                Program.save.invsave();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have finished my game, Thank you for playing");
                Console.ResetColor();
                Thread.Sleep(1000);
                var process = new ProcessStartInfo() { FileName = "https://www.youtube.com/watch?v=xvFZjo5PgG0", UseShellExecute = true };
                Process.Start(process);
                Environment.Exit(1);
            }

            //Sets the current player location
            Program.save.player.savelocation.currentlocal = "Cave";
            Program.save.invsave();

            //Bool to control the text loop of intocave()
            bool intocave = true;

            //This sets the cave options for player species elf
            if (Program.save.player.species == "Elf")
            {
                Program.save.progress.caveoptions.Remove("Dig in the dirt pile");
                Program.save.progress.caveoptions.Remove("Check the loose board");
                Program.save.progress.caveoptions.Add("Dig in the dirt pile");
                Program.save.progress.caveoptions.Add("Check the loose board");
                Program.save.invsave();
            }

            //Main text loop
            while (intocave) 
            {
                //Custom text based on player species
                if (Program.save.player.species == "Human" || Program.save.player.species == "Dwarf") 
                {
                    Console.WriteLine("You are in a dark basement, Looking around you see a stairway with a light at the top and a smooth metal door inlaid into the cave wall");
                }
                else if (Program.save.player.species == "Elf")
                {
                    Program.save.progress.caveoptions.Remove("Feel around the room to find more stuff");
                    Console.WriteLine("You are in a dark basement, thankfully you are an Elf who are known for exceptional vision in dark spaces. Looking around you see a stairway with a light at the top and a smooth metal door inlaid into the cave wall, along with a pile of dirt in a corner and what looks like a loose board underneath the stair supports.");
                }

                //Prints all save options
                for (int i = 0; i < Program.save.progress.caveoptions.Count; i++)
                {
                    Console.WriteLine((i + 1) + "." + Program.save.progress.caveoptions[i]);
                }

                string response = Console.ReadLine();
                switch (response.ToUpper())
                {
                    case "GO UPSTAIRS":
                        //Takes player to Living Room
                        LRSelect.intoLR();
                        break;

                    case "TRY TO OPEN THE DOOR":
                        //Response based on species
                        Console.WriteLine("You try to open the door but it has no handle and is very heavy. You quickly give up");
                        break;

                    case "FEEL AROUND THE ROOM TO FIND MORE STUFF":
                        Program.save.progress.caveoptions.Remove("Feel around the room to find more stuff");
                        Console.WriteLine("Feeling around the room you find a small dirt pile");
                        Program.save.progress.caveoptions.Add("Dig in the dirt pile");
                        Program.save.invsave();
                        break;

                    case "ENTER THE DARK ROOM":
                        DungeonSelect.intoDungeon();
                        break;

                    case "DIG IN THE DIRT PILE":
                    case "VISIT THE DAGGER HOLE":
                        Program.save.progress.caveoptions.Remove("Dig in the dirt pile");
                        Program.save.progress.caveoptions.Remove("Visit the Dagger hole");
                        //Allows player to visit the dirt pile/dagger hole,
                        //bool for dagger loop
                        bool dagpic = true;
                        while (dagpic)
                        {
                            //Shows if player does not have dagger in inventory or equiped
                            if (!Program.save.player.inventory.storage.Contains("Dagger") || !Program.save.player.inventory.weapon.Contains("Dagger"))
                            {
                                Console.WriteLine("Digging in the hole you find a small dagger. Do you wish to take it?[y/n]");

                                string answer = Console.ReadLine();
                                if (answer.ToUpper() == "Y")
                                {
                                    bool dagequ = true;
                                    while (dagequ)
                                    {
                                        Console.WriteLine("Do you want to equip the Dagger?[y/n]");
                                        string response1 = Console.ReadLine();
                                        if (response1.ToUpper() == "Y")
                                        {
                                            Equipable.weaponequiped("Dagger");
                                        }
                                        Program.save.progress.caveoptions.Remove("Dig in the dirt pile");
                                        Program.save.progress.caveoptions.Remove("Visit the Dagger hole");
                                        dagequ = false;
                                    }
                                    dagpic = false;
                                }
                                else if (answer.ToUpper() == "N")
                                {
                                    Console.WriteLine("You leave the dagger");
                                    Program.save.progress.caveoptions.Remove("Dig in the dirt pile");
                                    Program.save.progress.caveoptions.Remove("Visit the Dagger hole");
                                    Program.save.progress.caveoptions.Add("Visit the Dagger hole");
                                    Program.save.invsave();
                                    dagpic = false;
                                }
                            }
                            else if (Program.save.player.inventory.storage.Contains("Dagger") || !Program.save.player.inventory.weapon.Contains("Dagger")) //If the player already has the dagger then it prevents the interaction 
                            {
                                Program.save.progress.caveoptions.Remove("Dig in the dirt pile");
                                Program.save.progress.caveoptions.Remove("Visit the Dagger hole");
                                Console.WriteLine("You already have a Dagger in your inventory. You stop looking at the hole");
                                Program.save.progress.caveoptions.Add("Visit the Dagger hole");
                                Program.save.invsave();
                                dagpic = false;
                            }

                        }
                        break;

                    case "CHECK THE LOOSE BOARD":
                        //bool for the medalion picup loop
                        bool medalpic = true;
                        while (medalpic)
                        {
                            Console.Write("You pry off the board and look inside");
                            if (!Program.save.player.inventory.storage.Contains("Silver Medalion")) //If the player does not have the medalion in inventory then they can interact
                            {
                                Console.WriteLine(", you find a small silver medalion. Do you wish to keep it[y/n]");
                                string answer = Console.ReadLine();
                                if (answer.ToUpper() == "Y")
                                {
                                    Equipable.addinven("Silver Medalion");
                                    medalpic = false;
                                }
                                else if (answer.ToUpper() == "N")
                                {
                                    Console.WriteLine("You leave the Silver Medalion");
                                    medalpic = false;
                                }
                            }  else
                            {
                                Console.WriteLine(" You already have the medalion");
                                medalpic = false;
                            }
                        }
                        break;
                }
            }
        }
    }
}
