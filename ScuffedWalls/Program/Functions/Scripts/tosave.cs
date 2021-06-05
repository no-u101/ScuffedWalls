using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Scripts
{
    public class tosave
    {
        public Character player { get; set; }
        public int savenum { get; set; }
        public Progress progress { get; set; }


        public void invsave()
        {
            File.WriteAllText(Program.savdir+ @"\Scuffed Saves" + savenum+".json", JsonSerializer.Serialize(this,new JsonSerializerOptions(){WriteIndented = true}));
        }

        public static tosave[] inread()
        {
            List<tosave> something = new List<tosave>();
            DirectoryInfo mapFolder = new DirectoryInfo(Program.savdir);
            FileInfo[] mapDataFiles = mapFolder.GetFiles("*.json");
            foreach(var file in mapDataFiles)
            {
                something.Add(JsonSerializer.Deserialize<tosave>(File.ReadAllText(file.FullName)));
            }
            if (mapDataFiles.Any())
            {
                return something.ToArray();
            }
            return null;
        }

        public tosave(int somethingsmart) { savenum = somethingsmart; }
        public tosave() {; }

    }
}
