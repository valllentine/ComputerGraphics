using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes.Utility
{

    public struct RayIntersection
    {
        public Vector Normal { get; set; }
        public List<Vector> intersection;
    }

    public class Ray
    {
        private Vector _direction;
        public Vector Direction {
            get => _direction; 
            set => _direction = value.Normalize();
        }
        public Vector Origin { get; set; }

        public Ray(Vector origin, Vector dir)
        {
            Origin = origin;
            Direction = dir;
        }

    }
}
