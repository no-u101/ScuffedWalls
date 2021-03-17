using ModChart;
using ModChart.Wall;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;

namespace ScuffedWalls.Functions
{
    [ScuffedFunction("PointDefinition")] 
    class PointDefinition : SFunction
    {
        public void Run()
        {
            string name = null;
            object[][] points = null;
            foreach (var p in Parameters)
            {
                switch (p.Name)
                {
                    case "name":
                        name = p.Data;
                        break;
                    case "points":
                        points = System.Text.Json.JsonSerializer.Deserialize<object[][]>($"[{p.Data}]");
                        break;
                }
            }

            InstanceWorkspace.PointDefinitions.Add(new BeatMap.CustomData.PointDefinition()
            {
                _name = name,
                _points = points
            });

            ConsoleOut("PointDefinition", 1, Time, "PointDefinition");
        }
    }

    [ScuffedFunction("Transformation")]
    class Transformation : SFunction
    {
        public void Run()
        {
            string name = null;
            foreach (var p in Parameters)
            {
                switch (p.Name)
                {
                    case "name":
                        name = p.Data;
                        break;
                }
            }

            Workspace.Transformations.Add(new ModChart.Transformation()
            {
                _name = name,
                _data = System.Text.Json.JsonSerializer.Deserialize<ModChart.Transformation.Data>(System.Text.Json.JsonSerializer.Serialize(Parameters.CustomEventsDataParse()))
            });

            ConsoleOut("Transformation", 1, Time, "Transformation Saved");
        }
    }

    [ScuffedFunction("FontTemplate")]
    class FontTemplate : SFunction
    {
        float letting = 1;
        float leading = 1;
        float size = 1;
        float thicc = 1;
        float compression = 0.1f;
        float shift = 1;
        int linelength = 1000000;
        bool isblackempty = true;
        float alpha = 1;
        float smooth = 0;
        string path = "";
        string name;

        public void Run()
        {
            foreach (var p in Parameters)
            {
                switch (p.Name)
                {
                    case "letting":
                        letting = p.Data.toFloat();
                        break;
                    case "leading":
                        leading = p.Data.toFloat();
                        break;
                    case "size":
                        size = p.Data.toFloat();
                        break;
                    case "path":
                        path = Startup.ScuffedConfig.MapFolderPath + @"\" + p.Data.removeWhiteSpace();
                        break;
                    case "fullpath":
                        path = p.Data;
                        break;
                    case "thicc":
                        thicc = p.Data.toFloat();
                        break;
                    case "compression":
                        compression = p.Data.toFloat();
                        break;
                    case "shift":
                        shift = p.Data.toFloat();
                        break;
                    case "maxlinelength":
                        linelength = Convert.ToInt32(p.Data);
                        break;
                    case "alpha":
                        alpha = p.Data.toFloat();
                        break;
                    case "spreadspawntime":
                        smooth = p.Data.toFloat();
                        break;
                    case "isblackempty":
                        isblackempty = bool.Parse(p.Data);
                        break;
                    case "name":
                        name = p.Data;
                        break;
                }
            }

            Workspace.FontTemplates.Add(new TextSettings() 
            {    
                Text = new string[] { name },
                Leading = leading,
                Letting = letting,
                ImagePath = path,
                ImageSettings = new ImageSettings()
                {
                    scale = size,
                    shift = shift,
                    spread = smooth,
                    alfa = alpha,
                    centered = false,
                    isBlackEmpty = isblackempty,
                    maxPixelLength = linelength,
                    thicc = thicc,
                    tolerance = compression,
                }
            });

            ConsoleOut("FontTemplate", 1, Time, "FontTemplate Saved");
        }
    }


    public struct LyricsLineStruct
    {
        public string text;
        public float beat;
        public float animDuation;

        public LyricsLineStruct(string p_text, float p_beat, float p_animDuration)
        {
            text = p_text;
            beat = p_beat;
            animDuation = p_animDuration;
        }
    }

    [ScuffedFunction("LyricsLine")]
    class LyricsLine : SFunction
    {
        public void Run()
        {
            TextSettings fontSettings = new TextSettings();
            float animDuration;

            List<LyricsLineStruct> lyricsList = new List<LyricsLineStruct>();
            float totalLenght = 0;
            List<string> animationList = new List<string>();

            foreach (var p in Parameters)
            {
                switch (p.Name)
                {
                    case "fonttemplate":
                        fontSettings = Workspace.FontTemplates.First(f => f.Text[0] == p.Data);
                        break;

                    case "word":
                        object[] d = JsonConvert.DeserializeObject<object[]>(p.Data);
                        lyricsList.Add(new LyricsLineStruct(d[0].ToString(), d[1].toFloat(), d[2].toFloat()));
                        break;

                    case "animation":
                        animationList.AddRange(JsonConvert.DeserializeObject<string[]>(p.Data));
                        break;
                }
            }

            fontSettings.animationList = animationList;
            fontSettings.IsLyrics = true;
            fontSettings.lyricsList = lyricsList;

            fontSettings.ImageSettings.Wall = new BeatMap.Obstacle()
            {
                _time = Time,
                _duration = 0,
                _customData = Parameters.CustomDataParse()
            };

            InstanceWorkspace.Walls.AddRange(new WallText(fontSettings).Walls);
        }
    }



    [ScuffedFunction("AnimateTrack")]
    class CustomEventAnimateTrack : SFunction
    {
        public Variable Repeat;
        public Variable Beat;
        public void SetParameters()
        {
            Repeat = new Variable { Name = "repeat", Data = "1" };
            Beat = new Variable { Name = "time", Data = Time.ToString() };
            Parameters = Parameters.AddVariables(new Variable[] { Repeat, Beat });
        }
        public void Run()
        {
            SetParameters();
            int repeatcount = 1;
            float repeatTime = 0;
            foreach (var p in Parameters)
            {
                if (p.Name == "repeat") repeatcount = Convert.ToInt32(p.Data);
                else if (p.Name == "repeataddtime") repeatTime = Convert.ToSingle(p.Data);
            }
            for (float i = 0; i < repeatcount; i++)
            {
                InstanceWorkspace.CustomEvents.Add(new BeatMap.CustomData.CustomEvents()
                {
                    _time = Time + (i * repeatTime),
                    _type = "AnimateTrack",
                    _data = Parameters.CustomEventsDataParse()
                });
                Repeat.Data = i.ToString();
                Beat.Data = (Time + (i * repeatTime)).ToString();
            }
            ConsoleOut("AnimateTrack", repeatcount, Time, "CustomEvent");
        }
    }
    [ScuffedFunction("AssignPathAnimation")]
    class CustomEventAssignpath : SFunction
    {
        public Variable Repeat;
        public Variable Beat;
        public void SetParameters()
        {
            Repeat = new Variable { Name = "repeat", Data = "1" };
            Beat = new Variable { Name = "time", Data = Time.ToString() };
            Parameters = Parameters.AddVariables(new Variable[] { Repeat, Beat });
        }
        public void Run()
        {
            SetParameters();
            int repeatcount = 1;
            float repeatTime = 0;
            foreach (var p in Parameters)
            {
                if (p.Name == "repeat") repeatcount = Convert.ToInt32(p.Data);
                else if (p.Name == "repeataddtime") repeatTime = Convert.ToSingle(p.Data);
            }
            for (float i = 0; i < repeatcount; i++)
            {
                InstanceWorkspace.CustomEvents.Add(new BeatMap.CustomData.CustomEvents()
                {
                    _time = Time + (i * repeatTime),
                    _type = "AssignPathAnimation",
                    _data = Parameters.CustomEventsDataParse()
                });
                Repeat.Data = i.ToString();
                Beat.Data = (Time + (i * repeatTime)).ToString();
            }
            ConsoleOut("AssignPathAnimation", repeatcount, Time, "CustomEvent");
        }
    }
    [ScuffedFunction("AssignPlayerToTrack")]
    public class CustomEventPlayerTrack : SFunction
    {
        public void Run()
        {
            InstanceWorkspace.CustomEvents.Add(new BeatMap.CustomData.CustomEvents()
            {
                _time = Time,
                _type = "AssignPlayerToTrack",
                _data = Parameters.CustomEventsDataParse()
            });
            ConsoleOut("AssignPlayerToTrack", 1, Time, "CustomEvent");
        }
    }
    
    [ScuffedFunction("ParentTrack")]
    public class CustomEventParent : SFunction
    {
        public void Run()
        {
            InstanceWorkspace.CustomEvents.Add(new BeatMap.CustomData.CustomEvents()
            {
                _time = Time,
                _type = "AssignTrackParent",
                _data = Parameters.CustomEventsDataParse()
            });
            ConsoleOut("AssignTrackParent", 1, Time, "CustomEvent");
        }
    }


    
}
