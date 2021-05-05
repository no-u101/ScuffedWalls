using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModChart;
using ModChart.Wall;
using ScuffedWalls;
using System.Windows;
using System.IO;
using System.Numerics;
using System.Text.Json;

namespace ScuffedWalls.Functions
{
    [ScuffedFunction("GuitarHerofy")]
    class GuitarHerofy : SFunction
    {
        public void Run()
        {
            float starttime = Time;
            float endtime = GetParam("tobeat", float.PositiveInfinity, p => float.Parse(p));

            //Guitar stuff
            float minTime = GetParam("mintimebetweennotes", 0, p => float.Parse(p));
            float minResetTime = GetParam("mintimeforreset", 0, p => float.Parse(p));
            bool removeDoubles = GetParam("removedoubles", false, p => bool.Parse(p));
            bool sliderToStacks = GetParam("slidertostacks", false, p => bool.Parse(p));
            float sliderTime = GetParam("slidertime", 0.08f, p => float.Parse(p));
            bool removeBombs = GetParam("removebombs", false, p => bool.Parse(p));
            bool remakeStreams = GetParam("remakestreams", false, p => bool.Parse(p));
            float maxStreamDist = GetParam("maxstreamdist", 0, p => float.Parse(p));
            float minInlineDist = GetParam("mininlinedist", 0, p => float.Parse(p));
            float maxCrossoverDist = GetParam("maxcrossoverdist", 0, p => float.Parse(p));

            bool onlyConvert = GetParam("onlynotes", false, p => bool.Parse(p));
            string modelPath = GetParam("appendwalls", null, p => p);
            string[] paths = modelPath.Split(",");


            //Default modeltowall params
            float MapBpm = Utils.Info._beatsPerMinute.toFloat();
            float MapNjs = Utils.InfoDifficulty._noteJumpMovementSpeed.toFloat();
            float MapOffset = Utils.InfoDifficulty._noteJumpStartBeatOffset.toFloat();

            bool preserveTime = GetParam("preservetime", DefaultValue: false, p => bool.Parse(p));
            bool hasanimation = GetParam("hasanimation", DefaultValue: true, p => bool.Parse(p));
            bool assigncamtotrack = GetParam("cameratoplayer", DefaultValue: true, p => bool.Parse(p));
            float[] colormult = GetParam("colormult", new float[] { 1, 1, 1, 1 }, p => JsonSerializer.Deserialize<float[]>(p));
            bool Notes = GetParam("createnotes", DefaultValue: true, p => bool.Parse(p));
            bool spline = GetParam("spline", DefaultValue: false, p => bool.Parse(p));
            float smooth = GetParam("spreadspawntime", DefaultValue: 0, p => float.Parse(p));
            ModelSettings
            .TypeOverride tpye = GetParam("type", DefaultValue: ModelSettings.TypeOverride.ModelDefined, p => (ModelSettings.TypeOverride)int.Parse(p));
            float? alpha = GetParam("alpha", DefaultValue: null, p => (float?)float.Parse(p));
            float? thicc = GetParam("thicc", DefaultValue: null, p => (float?)float.Parse(p));
            bool setdeltapos = GetParam("setdeltaposition", false, p => bool.Parse(p));
            bool setdeltascale = GetParam("setdeltascale", false, p => bool.Parse(p));
            float duration = GetParam("duration", DefaultValue: 0, p => float.Parse(p));

            #region ConvertNotes

            int i = 0;
            List<BeatMap.Note> FixedNotes = new List<BeatMap.Note>();

            BeatMap.Note lastNoteL = null;
            BeatMap.Note lastNoteR = null;

            bool lastRemovedDoubleType = false;

            foreach (var note in InstanceWorkspace.Notes.OrderBy(o => (float)o._time))
            {
                if (note._time.toFloat() >= starttime && note._time.toFloat() <= endtime && (note._type.toFloat() == 1 || note._type.toFloat() == 0))
                {
                    BeatMap.Note lastNoteInst = null;
                    BeatMap.Note lastNoteOther = null;

                    int noteType = (int)note._type.toFloat();

                    if (noteType == 0) //Left
                    {
                        lastNoteInst = lastNoteL;
                        lastNoteOther = lastNoteR;

                    }
                    else if (noteType == 1) //right
                    {
                        lastNoteInst = lastNoteR;
                        lastNoteOther = lastNoteL;
                    }

                    i++;


                    if (lastNoteInst == null || lastNoteOther == null)
                    {
                        if (noteType == 0)
                            lastNoteL = note;
                        if (noteType == 1)
                            lastNoteR = note;

                        FixedNotes.Add(note);
                        continue;
                    }

                    //Fix sliders
                    if (sliderToStacks && (note._time.toFloat() - lastNoteInst._time.toFloat() < sliderTime && note._time.toFloat() != lastNoteInst._time.toFloat()))
                    {
                        int lane = (int)lastNoteInst._lineIndex.toFloat();
                        note._lineIndex = lane;

                    }
                    else if (note._time.toFloat() - lastNoteInst._time.toFloat() < minTime)
                    {
                        if (remakeStreams)
                        {
                            if (note._time.toFloat() - lastNoteInst._time.toFloat() == 2 * (note._time.toFloat() - lastNoteOther._time.toFloat()))
                            {
                                lastNoteOther._time = note._time;
                            }
                        }


                        continue;
                    }


                    //Fix crossovers
                    if ((noteType == 0 && note._lineIndex.toFloat() >= 2) || (noteType == 1 && note._lineIndex.toFloat() <= 1))
                    {
                        if (Math.Abs(note._lineIndex.toFloat() - lastNoteOther._lineIndex.toFloat()) > maxCrossoverDist)
                        {
                            float l_Line = note._lineIndex.toFloat();

                            note._lineIndex = lastNoteOther._lineIndex.toFloat();
                            lastNoteOther._lineIndex = l_Line;
                        }
                    }



                    //Fix inline
                    if (note._time.toFloat() - lastNoteOther._time.toFloat() < minInlineDist && note._lineIndex.toFloat() == lastNoteOther._lineIndex.toFloat())
                    {
                        if (noteType == 1)
                        {
                            if (note._lineIndex.toFloat() == 3)
                            {
                                note._lineIndex = note._lineIndex.toFloat() - 1;
                            }
                            else
                            {
                                note._lineIndex = note._lineIndex.toFloat() + 1;
                            }


                        }
                        else if (noteType == 0)
                        {

                            if (note._lineIndex.toFloat() == 0)
                            {
                                note._lineIndex = note._lineIndex.toFloat() + 1;
                            }
                            else
                            {
                                note._lineIndex = note._lineIndex.toFloat() - 1;
                            }

                        }
                    }


                    //Fix stacks
                    if (note._time == lastNoteInst._time || (note._time == lastNoteOther._time && note._lineIndex.ToString().Equals(lastNoteOther._lineIndex.ToString())))
                        continue;

                    //Remove doubles
                    if (note._time == lastNoteOther._time && removeDoubles)
                    {

                        if (lastRemovedDoubleType)
                        {
                            lastRemovedDoubleType = false;
                            continue;
                        }
                        else
                        {
                            lastRemovedDoubleType = true;
                            FixedNotes.Remove(lastNoteOther);
                        }

                        //note._customData = new BeatMap.CustomData() { _color = new object[] { 0, 0, 0, 1 } };
                        //lastNoteOther._customData = new BeatMap.CustomData() { _color = new object[] { 0, 0, 0, 1 } };
                        //continue;          
                    }


                    //Check for the reset stuff
                    if (float.Parse(note._time.ToString()) - float.Parse(lastNoteInst._time.ToString()) < minResetTime)
                    {
                        //Do dot logic
                        note._cutDirection = 8;

                        //Check if last note should be dot as well
                        if (float.Parse(note._time.ToString()) - float.Parse(lastNoteInst._time.ToString()) < 2 * minResetTime)
                        {
                            lastNoteInst._cutDirection = 8;
                        }


                    }
                    else
                    {
                        //Do reset logic

                        note._cutDirection = 1;

                        float timeBetweenLastOtherNote = float.Parse(note._time.ToString()) - float.Parse(lastNoteOther._time.ToString());

                        if (timeBetweenLastOtherNote * 2 < maxStreamDist)
                        {
                            if (((float)note._time - (float)lastNoteInst._time == 2 * timeBetweenLastOtherNote) &&
                                ((timeBetweenLastOtherNote < minResetTime && float.Parse(note._time.ToString()) != float.Parse(lastNoteOther._time.ToString())))
                                )
                            {
                                note._cutDirection = 8;
                                lastNoteInst._cutDirection = 8;
                                lastNoteOther._cutDirection = 8;
                            }
                        }
                    }


                    note._lineLayer = 0;
                    FixedNotes.Add(note);


                    if (Convert.ToInt32(note._type.ToString()) == 0)
                        lastNoteL = note;
                    if (Convert.ToInt32(note._type.ToString()) == 1)
                        lastNoteR = note;


                }
                else
                {

                    if (removeBombs && note._type.toFloat() == 3)
                        continue;

                    FixedNotes.Add(note);
                }
            }



            InstanceWorkspace.Notes = new List<BeatMap.Note>();
            InstanceWorkspace.Notes.AddRange(FixedNotes.ToArray());
            Console.WriteLine(($"Fixed {i} notes from beats {starttime} to {endtime}"));


            #endregion


            #region AppendWalls

            if (onlyConvert)
                return;

            BeatMap.Note baseNote = new BeatMap.Note()
            {
                _customData = new BeatMap.CustomData()
                {
                    _animation = new BeatMap.CustomData.Animation()
                    {
                        _dissolve = JsonSerializer.Deserialize<object[]>("[[0,0],[0,1]]"),
                        _dissolveArrow = JsonSerializer.Deserialize<object[]>("[[0,0],[0,1]]")
                    }
                }
            };

            int walls = 0;
            int notes = 0;
            int customevents = 0;
            foreach (var note in InstanceWorkspace.Notes.OrderBy(o => o._time.toFloat()))
            {
                if (note._time.toFloat() >= starttime && note._time.toFloat() <= endtime && (note._type.toFloat() == 1 || note._type.toFloat() == 0))
                {
                    float posX = 0;

                    switch (note._lineIndex.ToString())
                    {
                        case "0":
                            posX = 1.5f;
                            break;
                        case "1":
                            posX = 0.5f;
                            break;
                        case "2":
                            posX = -0.5f;
                            break;
                        case "3":
                            posX = -1.5f;
                            break;
                    }

                    int modelType = 0;

                    note.Append((baseNote), AppendTechnique.Overwrites);

                    if (Convert.ToInt32(note._cutDirection.ToString()) == 8)
                        modelType += 2;

                    if (Convert.ToInt32(note._type.ToString()) == 0 || Convert.ToInt32(note._type.ToString()) == 1)
                    {

                        Transformation Delta = new Transformation();
                        Delta.Position = GetParam("deltaposition", DefaultValue: new Vector3(posX, 0, 0), p => JsonSerializer.Deserialize<float[]>(p).ToVector3() + new Vector3(posX, 0, 0));
                        Delta.RotationEul = GetParam("deltarotation", DefaultValue: new Vector3(0, 0, 0), p => JsonSerializer.Deserialize<float[]>(p).ToVector3());
                        Delta.Scale = GetParam("deltascale", DefaultValue: new Vector3(1, 0, 0), p => new Vector3(float.Parse(p), 0, 0));

                        ModelSettings settings = new ModelSettings()
                        {
                            PCOptimizerPro = 0,
                            Path =  paths[(int)note._type.toFloat() + modelType],
                            Thicc = thicc,
                            CreateNotes = Notes,
                            DeltaTransformation = Delta,
                            PreserveTime = preserveTime,
                            Alpha = alpha,
                            technique = ModelSettings.Technique.Normal,
                            AssignCameraToPlayerTrack = assigncamtotrack,
                            CreateTracks = false,
                            Spline = spline,
                            ColorMult = colormult,
                            HasAnimation = hasanimation,
                            ObjectOverride = tpye,
                            BPM = MapBpm,
                            NJS = MapNjs,
                            Offset = MapOffset,
                            SetDeltaScale = setdeltascale,
                            SetDeltaPos = setdeltapos,
                            ScaleDuration = true,
                            Wall = (BeatMap.Obstacle)new BeatMap.Obstacle()
                            {
                                _time = note._time.toFloat(),
                                _duration = duration
                            }.Append(Parameters.CustomDataParse(new BeatMap.Obstacle()), AppendTechnique.Overwrites)
                        };

                        var model = new WallModel(settings);

                        InstanceWorkspace.Walls.AddRange(model.Output._obstacles);
                        InstanceWorkspace.Notes.AddRange(model.Output._notes);
                        InstanceWorkspace.CustomEvents.AddRange(model.Output._customData._customEvents);

                        walls += model.Output._obstacles.Length;
                        notes += model.Output._notes.Length;
                        customevents += model.Output._customData._customEvents.Length;
                        if (walls > 0) ConsoleOut("Wall", walls, Time, "GuitarHerofy");
                        if (notes > 0) ConsoleOut("Note", notes, Time, "GuitarHerofy");
                        if (customevents > 0) ConsoleOut("CustomEvent", customevents, Time, "GuitarHerofy");
                    }
                }

            }

            #endregion
        }
    }
}
