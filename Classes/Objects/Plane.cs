using System;
using Classes.Interfaces;
using Classes.Utility;

namespace Classes.Objects
{
    public class Plane : IObject
    {
        public Point Center { get; set; }
        public Vector Normal { get; set; }
        public Plane(Point pos, Vector normal)
        {
            Normal = normal;
            Center = pos;
        }
        public bool IsIntersection(Point start, Vector direction)
        {
            double denom = direction * Normal;

            if (denom > 0)
            {
                Vector k = Center - start;
                var t = k * Normal / denom;
                return (t >= 0);
            }

            return false;
        }
        public Point PointOfIntercept(Point start, Vector direction)
        {

            double denom = direction * Normal;

            if (IsIntersection(start, direction) == false)
                throw new ArgumentException(); 

            Vector k = Center - start;
            var t = k * Normal / denom;

            if (t < 0)
                throw new ArgumentException();

            var x = start.X + t * direction.X;
            var y = start.Y + t * direction.Y;
            var z = start.Z + t * direction.Z;

            return new Point(x, y, z);

        }
        public Vector GetNormal(Point point)
        {
            return Normal;
        }
    }
}
