using System;

namespace ConsoleApp1.Scripts
{
    public class Menue
    {
        public tosave Menu()
        {
            Console.WriteLine(@"
     _______.  ______  __    __   _______  _______  _______  _______          ___       _______  ____    ____  _______ .__   __. .___________. __    __  .______       _______     _______.
    /       | /      ||  |  |  | |   ____||   ____||   ____||       \        /   \     |       \ \   \  /   / |   ____||  \ |  | |           ||  |  |  | |   _  \     |   ____|   /       |
   |   (----`|  ,----'|  |  |  | |  |__   |  |__   |  |__   |  .--.  |      /  ^  \    |  .--.  | \   \/   /  |  |__   |   \|  | `---|  |----`|  |  |  | |  |_)  |    |  |__     |   (----`
    \   \    |  |     |  |  |  | |   __|  |   __|  |   __|  |  |  |  |     /  /_\  \   |  |  |  |  \      /   |   __|  |  . `  |     |  |     |  |  |  | |      /     |   __|     \   \    
.----)   |   |  `----.|  `--'  | |  |     |  |     |  |____ |  '--'  |    /  _____  \  |  '--'  |   \    /    |  |____ |  |\   |     |  |     |  `--'  | |  |\  \----.|  |____.----)   |   
|_______/     \______| \______/  |__|     |__|     |_______||_______/    /__/     \__\ |_______/     \__/     |_______||__| \__|     |__|      \______/  | _| `._____||_______|_______/    
                                                                                                                                                                                           

                                                                           
           


");
            Console.WriteLine("press Enter to start");
            ConsoleKey keypressed = ConsoleKey.P;
            while (keypressed != ConsoleKey.Enter) if (Console.KeyAvailable) keypressed = Console.ReadKey(true).Key;
            tosave savey = null;
            while (savey == null)
            {
                Console.WriteLine("Do you want to chose a save?[y/n]");
                string answer = Console.ReadLine();
                if (Program.savejr != null && answer.ToUpper().Equals("Y"))
                {
                    foreach (var savsel in Program.savejr)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(savsel.savenum + 1);
                        Console.ResetColor();
                    }
                    Console.WriteLine("Please select a save");
                    var savchoice = Console.ReadLine();
                    if (int.TryParse(savchoice.ToUpper(), out int choice))
                    {
                        if(choice <= Program.savejr.Length && choice > 0)
                        {
                            savey = Program.savejr[choice - 1];
                        }
                    }
                    else
                    {
                        Console.WriteLine("That is not a save");
                    }
                }
                else if (Program.savejr != null && answer.ToUpper().Equals("N"))
                {
                    new Rainbow().PrintRainbow("NEW SAVE");
                    Console.ResetColor();
                    int saveorsome = 0;
                    if (Program.savejr != null)
                    {
                        saveorsome = Program.savejr.Length;
                    }
                    savey = new tosave(saveorsome);
                }
                else if (answer.ToUpper().Equals("N"))
                {
                    new Rainbow().PrintRainbow("NEW SAVE");
                    Console.ResetColor();
                    int saveorsome = 0;
                    if (Program.savejr != null)
                    {
                        saveorsome = Program.savejr.Length;
                    }
                    savey = new tosave(saveorsome);
                }
                else if (answer.ToUpper().Equals("Y"))
                {
                    Console.WriteLine("No save detected. Press any key to continue.");
                    Console.ReadKey();
                    new Rainbow().PrintRainbow("NEW SAVE");
                    Console.ResetColor();
                    int saveorsome = 0;
                    if (Program.savejr != null)
                    {
                        saveorsome = Program.savejr.Length;
                    }
                    savey = new tosave(saveorsome);
                }
            }
            return savey;
        }
    }
}
