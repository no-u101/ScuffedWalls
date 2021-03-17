using ModChart;
using ModChart.Wall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScuffedWalls.Functions
{
    [ScuffedFunction("TextToWall")]
    class TextToWall : SFunction
    {
        public void Run()
        {
            List<string> lines = new List<string>();
            float letting = 1;
            float leading = 1;
            float size = 1;
            float thicc = 1;
            float duration = 1;
            float compression = 0.1f;
            float shift = 1;
            int linelength = 1000000;
            bool isblackempty = true;
            float alpha = 1;
            float smooth = 0;
            string path = "";
            var customdata = Parameters.CustomDataParse();
            var isNjs = customdata != null && customdata._noteJumpStartBeatOffset != null;
            float animDuration = 1;
            float definite = 1;

            TextSettings textSettings = null;

            foreach (var p in Parameters)
            {
                switch (p.Name)
                {
                    case "line":
                        lines.Add(p.Data);
                        break;
                    case "duration":
                        duration = p.Data.toFloat();
                        break;
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
                    case "definiteduration":
                        definite = p.Data.toFloat();
                        duration = Startup.bpmAdjuster.GetDefiniteDurationBeats(p.Data.toFloat());
                        if (isNjs) duration = Startup.bpmAdjuster.GetDefiniteDurationBeats(p.Data.toFloat(), customdata._noteJumpStartBeatOffset.toFloat());
                        break;
                    case "definitetime":
                        if (p.Data.ToLower().removeWhiteSpace() == "beats")
                        {
                            if (isNjs) Time = Startup.bpmAdjuster.GetPlaceTimeBeats(Time, customdata._noteJumpStartBeatOffset.toFloat());
                            else Time = Startup.bpmAdjuster.GetPlaceTimeBeats(Time);
                        }
                        else if (p.Data.ToLower().removeWhiteSpace() == "seconds")
                        {
                            if (isNjs) Time = Startup.bpmAdjuster.GetPlaceTimeBeats(Startup.bpmAdjuster.ToBeat(Time), customdata._noteJumpStartBeatOffset.toFloat());
                            else Time = Startup.bpmAdjuster.GetPlaceTimeBeats(Startup.bpmAdjuster.ToBeat(Time));
                        }
                        break;
                    case "definitedurationseconds":
                        duration = Startup.bpmAdjuster.GetDefiniteDurationBeats(Startup.bpmAdjuster.ToBeat(p.Data.toFloat()));
                        if (isNjs) duration = Startup.bpmAdjuster.GetDefiniteDurationBeats(Startup.bpmAdjuster.ToBeat(p.Data.toFloat()), customdata._noteJumpStartBeatOffset.toFloat());
                        break;
                    case "fonttemplate":
                        textSettings = Workspace.FontTemplates.First(f => f.Text[0] == p.Data);

                        letting = textSettings.Letting;
                        leading = textSettings.Leading;
                        size = textSettings.ImageSettings.scale;
                        thicc = textSettings.ImageSettings.thicc;
                        compression = textSettings.ImageSettings.tolerance;
                        shift = textSettings.ImageSettings.shift;
                        linelength = textSettings.ImageSettings.maxPixelLength;
                        isblackempty = textSettings.ImageSettings.isBlackEmpty;
                        alpha = textSettings.ImageSettings.alfa;
                        smooth = textSettings.ImageSettings.spread;
                        path = textSettings.ImagePath;

                        break;
                    case "animduration":
                        animDuration = p.Data.toFloat();
                        break;
                }
            }
            lines.Reverse();

            ScuffedLogger.Log("Anim " + animDuration.ToString());
            ScuffedLogger.Log("duration " +  duration.ToString());

            WallText text = new WallText(new TextSettings()
            {
                Leading = leading,
                Letting = letting,
                ImagePath = path,
                Text = lines.ToArray(),
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
                    Wall = new BeatMap.Obstacle()
                    {
                        _time = Time,
                        _duration = duration,
                        _customData = Parameters.CustomDataParse(animDuration/definite)
                    }
                }
            });
            InstanceWorkspace.Walls.AddRange(text.Walls);
            ConsoleOut("Wall", text.Walls.Length, Time, "TextToWall");
        }
    }


}
