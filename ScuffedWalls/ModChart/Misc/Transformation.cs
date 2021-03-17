using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ModChart
{
    public class Transformation
    {
        public object _name { get; set; }
        public Data _data { get; set; }

        public class Data
        {
            public dynamic _position { get; set; }
            public dynamic _scale { get; set; }
            public dynamic _rotation { get; set; }
            public dynamic _localRotation { get; set; }
            public dynamic _dissolve { get; set; }
            public dynamic _dissolveArrow { get; set; }
            public dynamic _definitePosition { get; set; }
            public dynamic _color { get; set; }
            public dynamic _time { get; set; }

        }
    }
}
