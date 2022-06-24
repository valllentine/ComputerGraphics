using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Utility;

namespace Classes.Scene
{
    public class Light
    {
        public Light(Vector vector)
        {
            setLight(vector);
        }
        public Vector LightDir{ get; private set; }

        public void setLight(Vector vector)
        {
            LightDir = vector.Normalize();
        }
    }
}
