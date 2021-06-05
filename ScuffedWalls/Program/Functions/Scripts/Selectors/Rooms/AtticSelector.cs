using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleApp1.Scripts.Managers.Characters;

namespace ConsoleApp1.Scripts.Selectors
{
    public static class AtticSelector
    {
        public static void intoAttic()
        {
            Console.Clear();

            Program.save.player.savelocation.currentlocal = "Attic";
            Program.save.invsave();

            bool Atticbool = true;


            while (Atticbool)
            {

                if (Program.save.progress.npc.hasSadiehag)
                {
                    Console.WriteLine(@"Walking up the stairs you find a nice looking attic with the ""Old Hag"" standing over a workbench, along with an opening to the fireplace and a sword sitting on the mantle. There is also a beautiful painting on the opposite wall.");
                }else
                {
                    Console.WriteLine(@"Walking up the stairs you find a nice looking attic with an old muscular woman standing over a workbench, along with an opening to the fireplace and a sword sitting on the mantle. There is also a beautiful painting on the opposite wall.");
                }
                for(int i=0; i<Program.save.progress.AtticOptions.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + Program.save.progress.AtticOptions[i]);
                }
                string answer = Console.ReadLine();
                switch (answer.ToUpper())
                {
                    case "GO DOWNSTAIRS":
                        LRSelect.intoLR();
                        break;
                    case "TALK TO THE WOMAN":
                        AnneManager.AnneConvo();
                        break;
                    case "CHECK THE FIREPLACE":
                        if (!Program.save.progress.npc.hasplayerfalled)
                        {
                            Console.WriteLine("You walk to the fireplace. It has a sword on the mantel and a sheild resting inside of the chimney shaft on a thin piece of wood");
                        } else if (Program.save.progress.npc.hasplayerfalled)
                        {
                            Console.WriteLine("You walk towwards the fireplace, There is a sword on the mantel and a hole falling down into the dungeon");
                        }
                        bool fpbool = true;
                        while (fpbool)
                        {
                            for (int i = 0; i < Program.save.progress.FPoptions.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + Program.save.progress.FPoptions[i]);
                            }
                            string answer1 = Console.ReadLine();
                            switch (answer1.ToUpper())
                            {
                                case "TAKE THE SWORD":
                                    bool swordbool = true;
                                    while (swordbool)
                                    {
                                        Console.WriteLine("You take the sword. Do you want to equip it?[y/n]");
                                        string answer2 = Console.ReadLine();
                                        if (answer2.ToUpper() == "Y")
                                        {
                                            Equipable.weaponequiped("sword");
                                            swordbool = false;
                                        }
                                        if (answer2.ToUpper() == "N")
                                        {
                                            Equipable.addinven("sword");
                                            swordbool = false;
                                        }
                                    }
                                    Program.save.progress.FPoptions.Remove("Take the sword");
                                    break;
                                case "TAKE THE SHEILD":
                                    Console.WriteLine("You try to grab the sheild but the thin piece of wood you are standing on collapses and you fall to the dungeon");
                                    Thread.Sleep(8000);
                                    Program.save.progress.FPoptions.Remove("Take the sheild");
                                    Program.save.progress.FPoptions.Add("Jump into the fireplace");
                                    Program.save.invsave();
                                    DungeonSelect.intoDungeon();
                                    break;
                                case "JUMP INTO THE FIREPLACE":
                                    Console.WriteLine("You jump down to the dungeon");
                                    DungeonSelect.intoDungeon();
                                    break;
                                case "LEAVE":
                                    fpbool = false;
                                    break;
                            }
                        }
                        break;
                    case "APPROACH THE PAINTING":
                        Console.WriteLine("You look at a large oil painting. It depicts the first stellar event and the falling of the stars");
                        break;
                }
            }
        }
    }
}
