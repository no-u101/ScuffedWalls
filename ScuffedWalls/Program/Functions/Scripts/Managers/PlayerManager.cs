using System;
using System.Collections.Generic;

namespace ConsoleApp1.Scripts

{
    public class PlayerManager
    {
        Stat stat = new Stat();
        //Used to manage the players health, current stats, levels, and equipt outfits and weapons
        public Stat specstatman() //species stat manager
        {
            stat.Playerhealth = 80;
            switch (Program.save.player.species.ToUpper())
            {
                case "HUMAN":
                    stat.Playerstrength = 10;
                    stat.Playerspeed = 10;
                    stat.Playerstealth = 10;
                    stat.Playercharisma = 10;
                    break;
                case "DWARF":
                    stat.Playerstrength = 12;
                    stat.Playerspeed = 8;
                    stat.Playerstealth = 6;
                    stat.Playercharisma = 12;
                    break;
                case "ELF":
                    stat.Playerstrength = 8;
                    stat.Playerspeed = 12;
                    stat.Playerstealth = 12;
                    stat.Playercharisma = 6;
                    break;
            }
            Program.playerManager.outstatman();
            return stat;
        }
        public void outstatman()
        {
            switch (Program.save.player.outfit.name.ToUpper())
            {
                case "SHINY ARMOR":
                    stat.Playerstrength = stat.Playerstrength + 2;
                    stat.Playerspeed = stat.Playerspeed + 2;
                    stat.Playerstealth = stat.Playerstealth + 2;
                    break;
                case "BRUTE ARMOR":
                    stat.Playerstrength = stat.Playerstrength + 4;
                    stat.Playerspeed = stat.Playerspeed - 2;
                    stat.Playerstealth = stat.Playerstealth - 2;
                    break;
                case "SHADOW CLOAK":
                    stat.Playerstrength = stat.Playerstrength - 2;
                    stat.Playerspeed = stat.Playerspeed - 2;
                    stat.Playerstealth = stat.Playerstealth + 4;
                    break;
                case "LIGHT ARMOR":
                    stat.Playerstrength = stat.Playerstrength - 2;
                    stat.Playerspeed = stat.Playerspeed + 4;
                    stat.Playerstealth = stat.Playerstealth - 2;
                    break;
            }
        }

        public void weapstatman()
        {
            switch (Program.save.player.inventory.weapon.ToUpper())
            {
                case "SWORD":
                    stat.Playerstrength = stat.Playerstrength + 2;
                    break;
                case "AXE":
                    stat.Playerstrength = stat.Playerstrength + 4;
                    stat.Playerspeed = stat.Playerspeed - 2;
                    break;
                case "DAGGER":
                    stat.Playerstrength = stat.Playerstrength + 2;
                    stat.Playerspeed = stat.Playerspeed + 2;
                    break;
            }
        }

        public List<string> SpeciesDes = new List<string> { "The race of Men are an intresting bunch. They are known for their capacity for incredible violence but also amazing feats of strength and camaraderie and loyalty", "The dawrves are always involded in the talk of the land. Known for their Strength, Charisma, and powerful greed. They can be good friends but also terrible enimies", "Elfs are the most recluse bunch in the land. Known for their Speed, Stealth, and terrible social skills. It's always good to have an elf at your side, even if you can't talk to them", "Press any key to continue" };
        public void speciesdescriptor()
        {
            for(int i = 0; i < SpeciesDes.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + SpeciesDes[i]);
            }
            Console.ReadLine();
        }

        public List<string> OutfitDes { get; set; } = new List<string> { "The Shiny Armor is a good all around armor. It allows you to have a moderate amount of protection with some benifits towards speed *and* stealth.", "The Brute Armor is a tough armor made for only the strongest of warriors. It offers a large amount of protection but at the cost of speed *and* stealth.", "The Shadow Cloak is a Hooded Cloak made in darkness. It allows its wearer to blend into the shadows but to be stealthy you must give up some strength *and* speed", "The Light Armor is the best option for an adventurer always on the go. It's lightness allows for quicker movement but at the cost of strength *and* stealth", "Press any key to continue" };
        public void outfitdescriptor()
        {
            for(int i=0; i < OutfitDes.Count; i++)
            {
                Console.WriteLine((i+1) + ". " + OutfitDes[i]);
            }
            Console.ReadLine();
        }
    }
    public static class Equipable
    {
        public static void weaponequiped(string weapontobe)
        {
            if (string.IsNullOrEmpty(Program.save.player.inventory.weapon))
            {
                Program.save.player.inventory.weapon = "none";
            } else
            {
                switch (Program.save.player.inventory.weapon.ToUpper())
                {
                    case "SWORD":
                        Program.save.player.stat.Playerstrength = Program.save.player.stat.Playerstrength - 2;
                        break;
                    case "AXE":
                        Program.save.player.stat.Playerstrength = Program.save.player.stat.Playerstrength - 4;
                        Program.save.player.stat.Playerspeed = Program.save.player.stat.Playerspeed + 2;
                        break;
                    case "DAGGER":
                        Program.save.player.stat.Playerstrength = Program.save.player.stat.Playerstrength - 2;
                        Program.save.player.stat.Playerspeed = Program.save.player.stat.Playerspeed + 2;
                        break;
                }
            }
            Program.save.player.inventory.storage.Add(Program.save.player.inventory.weapon);
            Program.save.player.inventory.weapon = weapontobe;
            Program.playerManager.weapstatman();
            Program.save.player.inventory.storage.Remove("none");
            Program.save.invsave();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Program.save.player.inventory.weapon + " has been equiped");
            Console.ResetColor();
        }

        public static void addinven(string addtoinven)
        {
            if (Program.save.player.inventory.storage.Contains(addtoinven))
            {
                Console.WriteLine("You already have the " + addtoinven + " in your inventory");
            }
            else if (!Program.save.player.inventory.storage.Contains(addtoinven))
            {
                Program.save.player.inventory.storage.Add(addtoinven);
                Program.save.invsave();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(addtoinven + " has been added to your inventory");
                Console.ResetColor();
            }
        }
    }
 }
