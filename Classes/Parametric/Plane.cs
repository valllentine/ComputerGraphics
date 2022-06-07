using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Interfaces;
using Classes.Utility;

namespace Classes.Parametric
{
    public class Plane : IIntersectable
    {
        public Vector Center { get; set; }
        public Vector Normal { get; set; }
        public Plane(Vector normal, Vector pos)
        {
            Normal = normal;
            Center = pos;
        }

        public RayIntersection Intersects(Ray ray)
        {
            var result = new RayIntersection();

            float denom = -Vector.Dot(Center, ray.Direction);

            if (denom < 0)
                return result;

            Vector L = Center - ray.Origin;
            var t = -Vector.Dot(L, Normal) / denom;

            if (t < 0)
                return result;

            result.intersection.Add(new Vector(
                ray.Origin.X + t * ray.Direction.X,
                ray.Origin.Y + t * ray.Direction.Y,
                ray.Origin.Z + t * ray.Direction.Z));

            result.Normal = Normal;

            return result;

        }
    }
}
