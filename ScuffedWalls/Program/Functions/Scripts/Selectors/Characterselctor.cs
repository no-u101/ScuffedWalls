using System;
using System.Linq;

namespace ConsoleApp1.Scripts
{
    public static class Characterselctor
    {
        public static Character charselect()
        {
            Progress specdes = new Progress();
            Character khar = new Character();
            bool charloop = true;
            while (charloop)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please select a Species");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Human");
                Console.WriteLine("2. Dwarf");
                Console.WriteLine("3. Elf");
                Console.WriteLine("4. Info on Species");
                string Species = Console.ReadLine();
                switch (Species.ToUpper())
                {
                    case "1":
                    case "HUMAN":
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("You have Chosen to join the race of men. Are you sure?[y/n]");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string y = Console.ReadLine();
                        switch (y.ToUpper())
                        {
                            case "Y":
                                khar.species = "Human";
                                while (string.IsNullOrEmpty(khar.name))
                                {
                                    Console.WriteLine("Please input a name");
                                    khar.name = Console.ReadLine();
                                }
                                Console.WriteLine("hmm...");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(khar.name.ToUpper());
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("Thats a name I havn't heard before.");
                                Console.Write("Well ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(khar.name.ToUpper());
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine(" I have some questions for you");
                                Console.WriteLine("If you had to chose a nice outfit, which one would you chose");
                                khar.outfit = outselect();
                                charloop = false;
                                break;
                        }

                        break;
                    case "2":
                    case "DWARF":
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("You have Chosen to join the race of Dwarves. Are you sure?[y/n]");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        y = Console.ReadLine();
                        switch (y.ToUpper())
                        {
                            case "Y":
                                khar.species = "Dwarf";
                                while (string.IsNullOrEmpty(khar.name))
                                {
                                    Console.WriteLine("Please input a name");
                                    khar.name = Console.ReadLine();
                                }
                                Console.WriteLine("hmm...");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(khar.name.ToUpper());
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("Thats a name I havn't heard before.");
                                Console.Write("Well ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(khar.name.ToUpper());
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine(" I have some questions for you");
                                Console.WriteLine("If you had to chose a nice outfit, which one would you chose");
                                khar.outfit = outselect();
                                charloop = false;
                                break;
                        }
                        break;
                    case "3":
                    case "ELF":
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("You have Chosen to join the race of Elves. Are you sure?[y/n]");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        y = Console.ReadLine();
                        switch (y.ToUpper())
                        {
                            case "Y":
                                khar.species = "Elf";
                                while (string.IsNullOrEmpty(khar.name))
                                {
                                    Console.WriteLine("Please input a name");
                                    khar.name = Console.ReadLine();
                                }
                                Console.WriteLine("hmm...");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(khar.name.ToUpper());
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("Thats a name I havn't heard before.");
                                Console.Write("Well ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(khar.name.ToUpper());
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine(" I have some questions for you");
                                Console.WriteLine("If you had to chose a nice outfit, which one would you chose");
                                khar.outfit = outselect();
                                charloop = false;
                                break;
                        }
                        break;
                    case "4":
                    case "INFO ON SPECIES":
                        Program.playerManager.speciesdescriptor();
                        break;
                    case "OWO":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Are you a cat?[y/n]: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        y = Console.ReadLine();
                        switch (y.ToUpper())
                        {
                            case "Y":
                                khar.species = "cat";
                                Console.WriteLine("POGGERS");
                                System.Threading.Thread.Sleep(1000);
                                Console.WriteLine("Say kitty, What is your name?");
                                khar.name = Console.ReadLine();
                                Console.WriteLine(khar.name.ToUpper() + " Thats a neat name...\nfor a cat");
                                Console.WriteLine("Well Mr." + khar.name.ToUpper() + " I have some questions for you");
                                Console.WriteLine("If you had to chose a nice outfit, which one would you chose");
                                khar.outfit = outselect();
                                break;
                            case "N":
                                Console.WriteLine("That is not poggers");
                                break;
                            default:
                                Console.WriteLine("Thats so sad");
                                System.Threading.Thread.Sleep(1000);
                                Environment.Exit(1);
                                break;
                        }
                        break;
                    case "IMAGINE DRAGONS":
                        khar.species = "Dragon Teller";
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Hear me roar motherfucker");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        khar.outfit = outselect();
                        charloop = false;
                        break;
                    default:
                        Console.WriteLine("you didn't say anything useful");
                        break;
                }
            }
            return khar;
        }
        public static Outfit outselect()
        {
            Progress outdes = new Progress();
            bool outloop = true;
            Outfit outfit1 = new Outfit();
            while (outloop)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Outfit");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Shiny Armor");
                Console.WriteLine("2. Brute Armor");
                Console.WriteLine("3. Shadow Cloak");
                Console.WriteLine("4. Light Armor");
                Console.WriteLine("5. Info on Outfits");
                string outfit = Console.ReadLine();
                switch (outfit.ToUpper())
                {
                    case "1":
                    case "SHINY ARMOR":
                        Console.WriteLine("You have chosen the Shiny Armor. Are you sure?[y/n]");
                        string y = Console.ReadLine();
                        if (y.ToUpper() == "Y")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Shiny Armor has been equiped and added to your Inventory");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            outfit1.name = ("Shiny Armor");
                            outloop = false;
                            break;
                        }
                        break;
                    case "2":
                    case "BRUTE ARMOR":
                        Console.WriteLine("You have chosen the Brute Armor. Are you sure?[y/n]");
                        y = Console.ReadLine();
                        if (y.ToUpper() == "Y")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Brute Armor has been equiped and added to your Inventory");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            outfit1.name = ("Brute Armor");
                            outloop = false;
                            break;
                        }
                        break;
                    case "3":
                    case "SHADOW CLOAK":
                        Console.WriteLine("You have chosen the Shadow Cloak. Are you sure?[y/n]");
                        y = Console.ReadLine();
                        if (y.ToUpper() == "Y")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Shadow Cloth has been equiped and added to your Inventory");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            outfit1.name = ("Shadow Cloth");
                            outloop = false;
                            break;
                        }
                        break;
                    case "4":
                    case "LIGHT ARMOR":
                        Console.WriteLine("You have chosen the Light Armor Are you sure?[y/n]");
                        y = Console.ReadLine();
                        if (y.ToUpper() == "Y")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Light Armor has been equiped and added to your Inventory");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            outfit1.name = ("Light Armor");
                            outloop = false;
                            break;
                        }
                        break;
                    case "5":
                    case "INFO ON DESRIPTION":
                        Program.playerManager.outfitdescriptor();
                        break;
                    default:
                        Console.WriteLine("You did not chose an option. Do you want to try agian?[y/n]");
                        y = Console.ReadLine();
                        if (y.ToUpper().Equals("Y"))
                        {
                            y = string.Empty;
                            break;
                        }
                        if (y.ToUpper() == "N")
                        {
                            outfit1.name = "empty";
                            outloop = false;
                        }
                        break;
                }
            }
            return outfit1;
        }       
    }

}

