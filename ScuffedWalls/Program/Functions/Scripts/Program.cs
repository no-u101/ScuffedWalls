using ConsoleApp1.Scripts;
using ConsoleApp1.Scripts.Selectors;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ScuffedWalls.Functions;



namespace ConsoleApp1
{
    public static class Program
    {
        //Array of the saves that are read from the save file location
        public static tosave[] savejr;

        //Gets the file directory and sets it to a string
        public static string savdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\ScuffedAdventures";

        //This Section makes instances for other scripts
        public static Menue menu = new Menue();
        public static tosave save;
        public static PlayerManager playerManager = new PlayerManager();

        //bool for player's abiliity to interact with the envoirnment, currently serves no purpose
        public static bool playerinteractable = true;

        public static void MainSA()
        {
            //Console size and settings
            Console.WindowWidth = Console.LargestWindowWidth; 
            Console.WindowHeight = Console.LargestWindowHeight;     
            Console.Title = "A Scuffed Adventure";

            //Makes the save file directory if it doesn't exsist. Cheesy I know
            if (!Directory.Exists(savdir))
            {
                Directory.CreateDirectory(savdir);
            }
            bool gamebool = true;
            while (gamebool)
            {
                //Does player setup
                savejr = tosave.inread();
                save = menu.Menu();
                save.player ??= Characterselctor.charselect();
                save.player.stat ??= playerManager.specstatman();
                save.player.inventory ??= new invenmanager();
                save.player.savelocation ??= new Savelocation();
                save.progress ??= new Progress();
                save.progress.npc ??= new NPC();
                save.invsave();

                if (save.progress.npc.isgameover)
                {
                    Console.WriteLine("This save is complete pls make a new one");
                }
                else if (!save.progress.npc.isgameover)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Save Selected");
                    Console.ResetColor();
                    Console.WriteLine("Begin?");
                    Console.ReadLine();

                    Savemanager.gotopoint();
                    gamebool = false;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("How the actual fuck did you get here. Its literelly impossible, fuck you.");
            Console.ResetColor();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(@"
__________████████_____██████
_________█░░░░░░░░██_██░░░░░░█
________█░░░░░░░░░░░█░░░░░░░░░█
_______█░░░░░░░███░░░█░░░░░░░░░█
_______█░░░░███░░░███░█░░░████░█
______█░░░██░░░░░░░░███░██░░░░██
_____█░░░░░░░░░░░░░░░░░█░░░░░░░░███
____█░░░░░░░░░░░░░██████░░░░░████░░█
____█░░░░░░░░░█████░░░████░░██░░██░░█
___██░░░░░░░███░░░░░░░░░░█░░░░░░░░███
__█░░░░░░░░░░░░░░█████████░░█████████
_█░░░░░░░░░░█████_████___████_█████___█
_█░░░░░░░░░░█______█_███__█_____███_█___█
█░░░░░░░░░░░░█___████_████____██_██████
░░░░░░░░░░░░░█████████░░░████████░░░█
░░░░░░░░░░░░░░░░█░░░░░█░░░░░░░░░░░░█
░░░░░░░░░░░░░░░░░░░░██░░░░█░░░░░░██
░░░░░░░░░░░░░░░░░░██░░░░░░░███████
░░░░░░░░░░░░░░░░██░░░░░░░░░░█░░░░░█
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█
░░░░░░░░░░░█████████░░░░░░░░░░░░░░██
░░░░░░░░░░█▒▒▒▒▒▒▒▒███████████████▒▒█
░░░░░░░░░█▒▒███████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█
░░░░░░░░░█▒▒▒▒▒▒▒▒▒█████████████████
░░░░░░░░░░████████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█
░░░░░░░░░░░░░░░░░░██████████████████
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█
██░░░░░░░░░░░░░░░░░░░░░░░░░░░██
▓██░░░░░░░░░░░░░░░░░░░░░░░░██
▓▓▓███░░░░░░░░░░░░░░░░░░░░█
▓▓▓▓▓▓███░░░░░░░░░░░░░░░██
▓▓▓▓▓▓▓▓▓███████████████▓▓█
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█");
            Console.ReadKey();
        }
    }
}
