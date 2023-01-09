using System.Text.Json;
using ModChart;

namespace ScuffedWalls.Functions
{
    [SFunction("Geometry", "EnvironmentGeometry")]
    class EnvironmentGeometry : ScuffedFunction
    {
        protected override void Update()
        {
            InstanceWorkspace.Environment.Add(new TreeDictionary()
            {
                ["geometry"] = new TreeDictionary()
                {
                    ["type"] = GetParam("type", null, p => (object)p),
                    ["material"] = new TreeDictionary()
                    {
                        ["color"] = GetParam("color", null, p => JsonSerializer.Deserialize<object[]>(p)),
                        ["shader"] = GetParam("shader", null, p => (object)p),
                        ["track"] = GetParam("MaterialTrack", null, p => (object)p),
                    },
                    ["collision"] = GetParam("collision", null, p => (object)bool.Parse(p)),
                },
                ["track"] = GetParam("track", null, p => (object)p),
                ["duplicate"] = GetParam("duplicate", null, p => (object)int.Parse(p)),
                ["active"] = GetParam("active", null, p => (object)bool.Parse(p)),
                ["scale"] = GetParam("scale", null, p => JsonSerializer.Deserialize<object[]>(p)),
                ["localPosition"] = GetParam("localposition", null, p => JsonSerializer.Deserialize<object[]>(p)),
                ["localRotation"] = GetParam("localrotation", null, p => JsonSerializer.Deserialize<object[]>(p)),
                ["position"] = GetParam("position", null, p => JsonSerializer.Deserialize<object[]>(p)),
                ["rotation"] = GetParam("rotation", null, p => JsonSerializer.Deserialize<object[]>(p))
                //TODO: components
                // i honestly dont fucking know what properties can be used on objects so ima just add them all and hope people use them right
            });
            RegisterChanges("Environment",1);
        }
    }
}
