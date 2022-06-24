using Classes.Utility;
using System;
using Classes.Interfaces;

namespace Classes.Objects
{
    public class Sphere : IObject
    {
        public Vector Center {get; set;}
        public double Radius { get; set; }
        public Sphere(Vector center, double rad)
        {
            Center = center;
            Radius = rad;
        }

        public bool IsIntersection(Point start, Vector direction)
        {
            Vector k = start - Center; 
            var a = direction * direction;
            var b = 2 * (direction * k);
            var c = k * k - Radius * Radius;
            var D = b * b - 4 * a * c;

            if (D < 0)
                return false;
            var t = (-b - Math.Sqrt(D)) / (2 * a);

            if (t >= 0)
                return true;
            return false;
        }

        public Point PointOfIntercept(Point start, Vector direction)
        {
            Vector k = start - Center;
            var a = direction * direction;
            var b = 2 * (direction * k);
            var c = k * k - Radius * Radius;
            var D = b * b - 4 * a * c;

            if (IsIntersection(start, direction) == false)
                throw new ArgumentException();

            var t = (-b - Math.Sqrt(D)) / (2 * a);

            if (t < 0)
                throw new ArgumentException();

            var x = start.X + t * direction.X;
            var y = start.Y + t * direction.Y;
            var z = start.Z + t * direction.Z;
            return new Point(x, y, z);
        }

        public Vector GetNormal(Point point)
        {
            Vector v = Center - point;
            return v;
        }
    }
}
