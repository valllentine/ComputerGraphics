using Classes.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes.Parametric
{
    class Sphere : Interfaces.IIntersectable
    {
        public Vector Center {get; set;}
        public float Radius { get; set; }

        public Sphere(float rad, Vector center)
        {
            Center = center;
            Radius = rad;
        }

        public RayIntersection Intersects(Ray ray)
        {
            var result = new RayIntersection();

            var radius2 = Math.Pow(Radius, 2);
            Vector L = ray.Origin - Center;
            float tca = Vector.Dot(L, ray.Direction);
            var d2 = Vector.Dot(L, L) - Math.Pow(tca, 2);

            if (d2 > radius2)
                return result;

            float thc = (float)Math.Sqrt(radius2 - d2);

            var t0 = tca - thc;
            var t1 = tca + thc;

            if (t0 > t1) (t1, t0) = (t0, t1);

            if (t0 > 0)
            
                result.intersection.Add(new Vector(
                    ray.Origin.X + t0 * ray.Direction.X,
                    ray.Origin.Y + t0 * ray.Direction.Y,
                    ray.Origin.Z + t0 * ray.Direction.Z));

            if (t1 > 0)
                result.intersection.Add(new Vector(
                    ray.Origin.X + t1 * ray.Direction.X,
                    ray.Origin.Y + t1 * ray.Direction.Y,
                    ray.Origin.Z + t1 * ray.Direction.Z));

            if (result.intersection.Count > 0)
            {
                result.Normal = new Vector(result.intersection[0] - Center).Normalize();
            }

            return result;
        }
    }
}
