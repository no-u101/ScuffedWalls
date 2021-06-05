using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Scripts.Managers.Characters
{
    public static class AnneManager
    {
        public static void AnneConvo()
        {
            bool AnneConvo = true;

            while (AnneConvo)
            {
                if (Program.save.progress.npc.istrolldead || Program.save.progress.npc.istrolldealt)
                {
                    Program.save.progress.AnneConvo.Remove("I met a troll in the downstairs");
                    if (!Program.save.progress.npc.isAnneok )
                    {
                        Program.save.progress.AnneConvo.Add("I met a troll downstairs");
                    }
                    Program.save.invsave();
                }
                for (int i = 0; i<Program.save.progress.AnneConvo.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + Program.save.progress.AnneConvo[i]);
                }
                string answer = Console.ReadLine();
                switch (answer.ToUpper())
                {
                    case "GREET HER":
                        PrintAnne("Hello, I heard you talking downstairs");
                        break;
                    case "ASK ABOUT THE OTHER WOMAN":
                        PrintAnne("She's my roommate of sort, we've been having a bit of a spat recently. I can only assume she told you all sorts of bad things about me.");
                        break;
                    case "ASK ABOUT THE PAINTING":
                        PrintAnne("Her father painted that when she was a young girl. It is supposed to represent the first stellar event.");
                        break;
                    case "TALK ABOUT LIFE":
                        PrintAnne("Besides our arguments lately I can't complain, I keep hearing some weird groaning sounds from the room beside the basement though");
                        if (Program.save.progress.npc.hasSadieTroll == true)
                        {
                            Program.save.progress.AnneConvo2.Remove("The other woman also heard something");
                            Program.save.progress.AnneConvo2.Add("The other woman also heard something");
                            Program.save.invsave();
                        }
                        bool atrollbool = true;
                        while (atrollbool)
                        {
                            
                            for(int i = 0; i < Program.save.progress.AnneConvo2.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + Program.save.progress.AnneConvo2[i]);
                            }
                            string answer1 = Console.ReadLine();
                            switch (answer1.ToUpper())
                            {
                                case "WANT ME TO CHECK IT OUT":
                                case "WANT ME TO CHECK IT OUT?":
                                    PrintAnne("If you really want to dear, I don't know how you could get down there though");
                                    atrollbool = false;
                                    break;
                                case "HAVE YOU INVESTIGATED IT":
                                case "HAVE YOU INVESTIGATED IT?":
                                    PrintAnne("No, I try not to go downstairs to often when she's mad at me");
                                    bool atrollbool1 = true;
                                    while (atrollbool1)
                                    {
                                        Console.WriteLine("1. Want me to check it out \n2. Move on");
                                        string answer2 = Console.ReadLine();
                                        switch (answer2.ToUpper())
                                        {
                                            case "WANT ME TO CHECK IT OUT":
                                                PrintAnne("If you really want to dear, I don't know how you could get down there though");
                                                atrollbool1 = false;
                                                break;
                                            case "MOVE ON":
                                                atrollbool1 = false;
                                                atrollbool = false;
                                                break;
                                        }
                                    }
                                    break;
                                case "MOVE ON":
                                    atrollbool = false;
                                    break;
                                case "THE OTHER WOMAN ALSO HEARD SOMETHING":
                                    PrintAnne("Hmm... Its probably the house settling, Did she seemed worried?");
                                    bool atrollbool2 = true;
                                    while (atrollbool2)
                                    {
                                        Console.WriteLine("1. Yes \n2. No");
                                        string answer3 = Console.ReadLine();
                                        switch (answer3.ToUpper())
                                        {
                                            case "YES":
                                                PrintAnne("She always was a little paranoid");
                                                atrollbool2 = false;
                                                atrollbool = false;
                                                break;
                                            case "NO":
                                                PrintAnne("That doesn't seem like her. But if she's not worried then I guess its fine");
                                                atrollbool2 = false;
                                                atrollbool = false;
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "LEAVE":
                        AnneConvo = false;
                        break;
                    case "I MET A TROLL DOWNSTAIRS":
                        Program.save.progress.AnneConvo.Remove("I met a troll in the downstairs");
                        Program.save.progress.npc.isAnneok = true;
                        Program.save.invsave();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Wow, A troll?? ");
                        if (Program.save.progress.npc.isSadieok)
                        {
                            Console.WriteLine("Good thing you already told her, meet us in the Basement.");
                        }
                        if (!Program.save.progress.npc.isSadieok)
                        {
                            Console.WriteLine("You should go tell the woman downstairs. Lets meet in the Basement.");
                        }
                        Console.ResetColor();
                        Program.save.progress.AnneConvo.Remove("I met a troll in the dungeon");
                        AnneConvo = false;
                        Program.save.invsave();
                        break;
                }
            }
        }

        public static void PrintAnne(string b)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(b);
            Console.ResetColor();
        }
    }
}
