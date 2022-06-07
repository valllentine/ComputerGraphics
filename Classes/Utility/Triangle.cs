using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Utility;

namespace Classes
{
    //TODO
    class Triangle
    {
        public Vector Normal { get; set; }

        public Vector[] points = new Vector[3];

        public Triangle(Vector p1, Vector p2, Vector p3)
        {
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }
    }
}
