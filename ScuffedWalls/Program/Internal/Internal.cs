using ModChart;
using ModChart.Wall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace ScuffedWalls
{

    static class Internal
    {
        public static T[] CombineWith<T>(this T[] first, params T[][] arrays)
        {
            List<T> list = new List<T>();
            list.AddRange(first);
            foreach (var array in arrays) if(array != null) list.AddRange(array);
            return list.ToArray();
        }
        public static string Remove(this string str, char ch)
        {
            return string.Join("", str.Split(ch));
        }
        public static string Remove(this string str, string ch)
        {
            return string.Join("", str.Split(ch));
        }
        public static T[] GetAllBetween<T>(this T[] mapObjects, float starttime, float endtime)
        {
            return mapObjects.Where(obj => (Convert.ToSingle(typeof(T).GetProperty("_time").GetValue(obj, null).ToString()) >= starttime) && (Convert.ToSingle(typeof(T).GetProperty("_time").GetValue(obj, null).ToString()) <= endtime)).ToArray();
        }
        static public bool MethodExists<t>(this string methodname, Type attribute)
        {
            foreach (var methods in typeof(t).GetMethods().Where(m => m.GetCustomAttributes(attribute).Count() > 0))
            {
                if (methods.Name == methodname) return true;
            }
            return false;
        }
        static public bool MethodExists<t>(this string methodname)
        {
            foreach (var methods in typeof(t).GetMethods())
            {
                if (methods.Name == methodname) return true;
            }
            return false;
        }
        public static bool needsNoodleExtensions(this BeatMap map)
        {
            //are there any custom events?
            if (map._customData != null && map._customData._customEvents != null && map._customData._customEvents.Length > 0) return true;

            //do any notes have any noodle data other than color?
            if (map._notes.Any(note => note._customData != null && (typeof(BeatMap.CustomData).GetProperties().Any(p => p.GetValue(note._customData) != null && p.Name != "_color")))) return true;

            //do any walls have any noodle data other than color?
            if (map._obstacles.Any(wall => wall._customData != null && (typeof(BeatMap.CustomData).GetProperties().Any(p => p.GetValue(wall._customData) != null && p.Name != "_color")))) return true;

            return false;
        }
        public static bool needsChroma(this BeatMap map)
        {
            //do light have color
            if (map._events.Any(light => light._customData != null && light._customData._color != null)) return true;

            //do wal have color or animate color
            if (map._obstacles.Any(wall => wall._customData != null && (wall._customData._color != null || (wall._customData._animation != null && wall._customData._animation._color != null)))) return true;

            //do note have color or animate color
            if (map._notes.Any(note => note._customData != null && (note._customData._color != null || (note._customData._animation != null && note._customData._animation._color != null)))) return true;

            return false;

        }
        public static int getCountByID(this int type)
        {
            if (type == 0 || type == 2 || type == 3) return 5;
            else if (type == 1) return 15;
            else if (type == 4) return 9;
            return 0;
        }
        public static int getValueFromOld(this int value)
        {
            if (value == 0) return 0;
            return 5;
        }
        public static T DeepClone<T>(this T a)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(a));
        }
        public static string ToFileString(this DateTime time)
        {
            return $"Backup - {time.ToFileTime()}";
        }

        public static string removeWhiteSpace(this string WhiteSpace)
        {
            return new string(WhiteSpace.Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }
    }

<<<<<<< Updated upstream
    public class JsoValidator
    {
        public dynamic Deserialized;
        public bool WasSuccess;
        public string Raw;
        public static JsoValidator Check(string s)
=======

        //adjust for lower caseeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
        public static BeatMap.CustomData CustomDataParse(this Parameter[] CustomNoodleData, float animDuration = -1)
>>>>>>> Stashed changes
        {
            var val = new JsoValidator() { Raw = s };

            try
            {
<<<<<<< Updated upstream
                val.Deserialized = JsonSerializer.Deserialize<object>(s);
                val.WasSuccess = true;
            }
            catch
            {
                val.WasSuccess = false;
=======

                if (param.Name == "AnimateDefinitePosition".ToLower()) Animation._definitePosition = JsonSerializer.Deserialize<object[][]>($"[{param.Data}]");
                else if (param.Name == "DefineAnimateDefinitePosition".ToLower()) Animation._definitePosition = param.Data;

                else if (param.Name == "AnimatePosition".ToLower()) Animation._position = JsonSerializer.Deserialize<object[][]>($"[{param.Data}]");
                else if (param.Name == "DefineAnimatePosition".ToLower()) Animation._position = param.Data;

                else if (param.Name == "AnimateDissolve".ToLower()) Animation._dissolve = JsonSerializer.Deserialize<object[][]>($"[{param.Data}]");
                else if (param.Name == "DefineAnimateDissolve".ToLower()) Animation._dissolve = param.Data;

                else if (param.Name == "AnimateDissolveArrow".ToLower()) Animation._dissolveArrow = JsonSerializer.Deserialize<object[][]>($"[{param.Data}]");
                else if (param.Name == "DefineAnimateDissolveArrow".ToLower()) Animation._dissolveArrow = param.Data;

                else if (param.Name == "AnimateColor".ToLower()) Animation._color = JsonSerializer.Deserialize<object[][]>($"[{param.Data}]");
                else if (param.Name == "DefineAnimateColor".ToLower()) Animation._color = param.Data;

                else if (param.Name == "AnimateRotation".ToLower()) Animation._rotation = JsonSerializer.Deserialize<object[][]>("[" + param.Data + "]");
                else if (param.Name == "DefineAnimateRotation".ToLower()) Animation._rotation = param.Data;

                else if (param.Name == "AnimateLocalRotation".ToLower()) Animation._localRotation = JsonSerializer.Deserialize<object[][]>($"[{param.Data}]");
                else if (param.Name == "DefineAnimateLocalRotation".ToLower()) Animation._localRotation = param.Data;

                else if (param.Name == "AnimateScale".ToLower()) Animation._scale = JsonSerializer.Deserialize<object[][]>($"[{param.Data}]");
                else if (param.Name == "DefineAnimateScale".ToLower()) Animation._scale = param.Data;

                else if (param.Name == "AnimateInteractable".ToLower()) Animation._interactable = JsonSerializer.Deserialize<object>($"[{param.Data}]");
                else if (param.Name == "DefineAnimateInteractable".ToLower()) Animation._interactable = param.Data;

                else if (param.Name == "Transformation".ToLower())
                {
                    

                    Transformation trans = Workspace.Transformations.First(t => t._name.ToString() == param.Data.removeWhiteSpace());

                    Transformation newTrans = new Transformation();
                    newTrans._data = new Transformation.Data();

                    //ScuffedLogger.Log(trans._data._position.ToString());

                    if (animDuration != -1)
                    {
                        if (!(trans._data._position is null))
                        {
                            object[][] pos = JsonSerializer.Deserialize<object[][]>(trans._data._position.ToString());



                            for (int i = 0; i < pos.Length; i++)
                            {
                                pos[i][3] = float.Parse(pos[i][3].ToString()) * animDuration;
                            }

                            newTrans._data._position = pos;

                            ScuffedLogger.Log(JsonSerializer.Serialize(newTrans._data._position));
                        }

                        if (!(trans._data._dissolve is null))
                        {
                            object[][] all = JsonSerializer.Deserialize<object[][]>(trans._data._dissolve.ToString());

                            for (int i = 0; i < all.Length; i++)
                            {
                                all[i][1] = float.Parse(all[i][1].ToString()) * animDuration;
                            }

                            newTrans._data._dissolve = all;
                        }

                        if (!(trans._data._rotation is null))
                        {
                            object[][] all = JsonSerializer.Deserialize<object[][]>(trans._data._rotation.ToString());

                            for (int i = 0; i < all.Length; i++)
                            {
                                all[i][3] = float.Parse(all[i][3].ToString()) * animDuration;
                            }

                            newTrans._data._rotation = all;
                        }
                    } else
                    {
                        newTrans = trans;
                    }


                    Animation = JsonSerializer.Deserialize<BeatMap.CustomData.Animation>(JsonSerializer.Serialize(newTrans._data));
                }

                else if (param.Name == "interactable".ToLower()) CustomData._interactable = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "rotation".ToLower()) CustomData._rotation = JsonSerializer.Deserialize<object[]>(param.Data);

                else if (param.Name == "LocalRotation".ToLower()) CustomData._localRotation = JsonSerializer.Deserialize<object[]>(param.Data);

                else if (param.Name == "fake".ToLower()) CustomData._fake = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "position".ToLower()) CustomData._position = JsonSerializer.Deserialize<object[]>(param.Data);

                else if (param.Name == "cutDirection".ToLower()) CustomData._cutDirection = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "scale".ToLower()) CustomData._scale = JsonSerializer.Deserialize<object[]>(param.Data);

                else if (param.Name == "track".ToLower()) CustomData._track = JsonSerializer.Deserialize<object>($"\"{param.Data.removeWhiteSpace()}\"");

                else if (param.Name == "color".ToLower()) CustomData._color = JsonSerializer.Deserialize<object[]>(param.Data);
                else if (param.Name == "rgbcolor".ToLower()) CustomData._color = JsonSerializer.Deserialize<object[]>(param.Data).Select(o => { return (object)(o.toFloat() / 255f); }).ToArray();

                else if (param.Name == "NJSOffset".ToLower()) CustomData._noteJumpStartBeatOffset = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "NJS".ToLower()) CustomData._noteJumpMovementSpeed = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "NoSpawnEffect".ToLower()) CustomData._disableSpawnEffect = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "CPropID".ToLower()) CustomData._propID = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "CLightID".ToLower()) CustomData._lightID = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "CgradientDuration".ToLower()) { CustomData._lightGradient ??= new BeatMap.CustomData.LightGradient(); CustomData._lightGradient._duration = JsonSerializer.Deserialize<object[]>(param.Data); }

                else if (param.Name == "CgradientStartColor".ToLower()) { CustomData._lightGradient ??= new BeatMap.CustomData.LightGradient(); CustomData._lightGradient._startColor = JsonSerializer.Deserialize<object[]>(param.Data); }

                else if (param.Name == "CgradientEndColor".ToLower()) { CustomData._lightGradient ??= new BeatMap.CustomData.LightGradient(); CustomData._lightGradient._endColor = JsonSerializer.Deserialize<object[]>(param.Data); }

                else if (param.Name == "CgradientEasing".ToLower()) { CustomData._lightGradient ??= new BeatMap.CustomData.LightGradient(); CustomData._lightGradient._easing = JsonSerializer.Deserialize<object>($"\"{param.Data}\""); }

                else if (param.Name == "CLockPosition".ToLower()) CustomData._lockPosition = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "CPreciseSpeed".ToLower()) CustomData._preciseSpeed = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "Cdirection".ToLower()) CustomData._direction = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "CNameFilter".ToLower()) CustomData._nameFilter = JsonSerializer.Deserialize<object>($"\"{param.Data}\"");

                else if (param.Name == "Creset".ToLower()) CustomData._reset = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "Cstep".ToLower()) CustomData._step = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "Cprop".ToLower()) CustomData._prop = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "Cspeed".ToLower()) CustomData._speed = JsonSerializer.Deserialize<object>(param.Data);

                else if (param.Name == "CCounterSpin".ToLower()) CustomData._counterSpin = JsonSerializer.Deserialize<object>(param.Data);

                if (typeof(BeatMap.CustomData.Animation).GetProperties().Any(p => p.GetValue(Animation) != null)) CustomData._animation = Animation;

>>>>>>> Stashed changes
            }

            return val;
        }
        public static JsoValidator Check<t>(string s)
        {
            var val = new JsoValidator() { Raw = s };

            try
            {
                val.Deserialized = JsonSerializer.Deserialize<t>(s);
                val.WasSuccess = true;
            }
            catch
            {
                val.WasSuccess = false;
            }

            return val;
        }
    }
    public class TimeKeeper
    {
        public DateTime StartTime;
        public void Start() => StartTime = DateTime.Now;
        public void Complete() =>  ScuffedLogger.Log($"Completed in {(DateTime.Now - StartTime).TotalSeconds} seconds");
    }




}
