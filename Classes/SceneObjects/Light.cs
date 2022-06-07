using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Utility;

namespace Classes.SceneObjects
{
    public class Light
    {
        public Vector Position { get; set; }

        public Light(Vector pos)
        {
            Position = pos;
        }
    }
    class DirectionLight
    {
        public Vector Position { get; set; }
        public Vector Direction { get; set; }
        public DirectionLight(Vector pos, Vector dir)
        {
            Position = pos;
            Direction = dir;
        }
    }
}
