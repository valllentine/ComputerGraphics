using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Utility;

namespace Classes.Scene
{
    public class Light
    {
        private Vector lightDirection;

        public Light(Vector vector)
        {
            setLight(vector);
        }

        public Vector LightDir
        {
            get { return lightDirection; }
        }

        public void setLight(Vector vector)
        {
            lightDirection = vector.Normalize();
        }
    }
}
