using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp1.Scripts
{
    public class Character
    {
        public string name { get; set; }
        public string species { get; set; }
        public Outfit outfit {get; set;}
        public Stat stat { get; set; }
        public invenmanager inventory { get; set; }
        public Savelocation savelocation { get; set; }
    }

    public class Outfit
    {
        public string name { get; set; }
    }

    public class Stat
    {
        private int realhealth;
        private int realstrenght;
        private int realspeed;
        private int realstealth;
        private int realcharisma;
        public int Playerhealth
        {
            get { return realhealth; } 
            set { if (value > 100)
                    realhealth = 80;
                else if (value < 0)
                    realhealth = 0;
                else
                    realhealth = value;
            } 
        }
        public int Playerstrength
        {
            get { return realstrenght; }
            set
            {
                if (value > 30)
                    realstrenght = 30;
                else if (value < 1)
                    realstrenght = 1;
                else
                    realstrenght = value;
            }
        }
        public int Playerspeed
        {
            get { return realspeed; }
            set
            {
                if (value > 30)
                    realspeed = 30;
                else if (value < 1)
                    realspeed = 1;
                else
                    realspeed = value;
            }
        }
        public int Playerstealth
        {
            get { return realstealth; }
            set
            {
                if (value > 30)
                    realstealth = 30;
                else if (value < 1)
                    realstealth = 1;
                else
                    realstealth = value;
            }
        }
        public int Playercharisma
        {
            get { return realcharisma; }
            set
            {
                if (value > 30)
                    realcharisma = 30;
                else if (value < 1)
                    realcharisma = 1;
                else
                    realcharisma = value;
            }
        }

        public int PlayerDeaths { get; set; }
    }
    
    public class invenmanager
    {
        public string weapon { get; set; }
        public string tool { get; set; }

        public List<string> storage { get; set; } = new List<string>();
    }

    public class NPC
    {
        public bool isSadieok { get; set; }
        public bool isAnneok { get; set; }
        public bool hasSadieTroll { get; set; }
        public bool hasSadiehag { get; set; }
        public bool hasanneconvo { get; set; }
        public bool istrolldead { get; set; }
        public bool istrolldealt { get; set; }
        public bool hasplayerfalled { get; set; }
        public bool isgameover { get; set; }

    }

    public class Savelocation
    {
        public string currentlocal { get; set; }
    }

    public class Progress
    {
        public NPC npc { get; set; }
        public List<string> SadieConvo { get; set; } = new List<string> { "Greet her", "Ask about the house", "Ask about the upstairs", "Ask about the door downstairs", "Leave" };
        public List<string> AnneConvo { get; set; } = new List<string> { "Greet her","Ask about the other woman","Ask about the painting","Talk about life","Leave" };
        public List<string> AnneConvo2 { get; set; } = new List<string> { "Want me to check it out?", "Have you investigated it?", "Move on" };
        public List<string> TrollConvo { get; set; } = new List<string> {"How did you get here","What is your name","Leave"};
        
        public List<string> caveoptions { get; set; } = new List<string> { "Go upstairs", "Try to open the door", "Feel around the room to find more stuff" };
        public List<string> LRoptions { get; set; } = new List<string> { "Go upstairs", "Go downstairs","Talk to the old woman","Raid the cubbard",};
        public List<string> AtticOptions { get; set; } = new List<string> { "Go downstairs", "Talk to the woman", "Check the fireplace","Approach the Painting" };
        public List<string> DungeonOptionsA { get; set; } = new List<string> { "[try]Talk to the troll","Attack the troll"};
        public List<string> DungeonOptionsD1 { get; set; } = new List<string> {"Search the troll", "Check the metal door" };
        public List<string> DungeonOptionsD2 { get; set; } = new List<string> { "Ask the troll for stuff", "Check the metal door" };

        public List<string> FPoptions { get; set; } = new List<string> { "Take the sword","Take the sheild","Leave" };
    }
 }
