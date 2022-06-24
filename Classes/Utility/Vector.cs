using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Interfaces;

namespace Classes.Utility
{
    public class Vector : ITransformable<Vector>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector(Vector vec)
        {
            X = vec.X;
            Y = vec.Y;
            Z = vec.Z;
        }
        public double Magnitude()
        {
            return (double)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }
        public Vector Normalize()
        {
            var m = Magnitude();
            return new Vector(X / m, Y / m, Z / m);
        }
        public static double Distance(Vector vec1, Vector vec2)
        {
            return (vec2 - vec1).Magnitude();
        }

 
        public static Vector operator +(Vector vec1, Vector vec2)
        {
            return new Vector(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }

        public static Vector operator +(Vector vec, double num)
        {
            return new Vector(vec.X + num, vec.Y + num, vec.Z + num);
        }

        public static Vector operator -(Vector vec1, Vector vec2)
        {
            return new Vector(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }

        public static Vector operator -(Vector vec, double num)
        {
            return new Vector(vec.X - num, vec.Y - num, vec.Z - num);
        }

        public static double operator *(Vector vec1, Vector vec2)
        {
            return (vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z);
        } 

        public static Vector operator *(Vector vec, double num)
        {
            return new Vector(vec.X * num, vec.Y * num, vec.Z * num);
        }
        public static Vector Cross(Vector lhs, Vector rhs)
        {
            return new Vector(
                lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.Z * rhs.X - lhs.X * rhs.Z,
                lhs.X * rhs.Y - lhs.Y * rhs.X
                );
        }
        public Vector Rotate(Vector eulers)
        {
            return RotateX(eulers.X).RotateY(eulers.Y).RotateZ(eulers.Z);
        }

        public Vector Move(Vector dir)
        {
            return new Vector(X + dir.X, Y + dir.Y, Z + dir.Z);
        }

        public Vector Scale(Vector scale)
        {
            return new Vector(X * scale.X, Y * scale.Y, Z * scale.Z);
        }

        public Vector RotateX(double deg)
        {
            return new Vector(
                X,
                (double)(Y * Math.Cos(deg) - Z * Math.Sin(deg)),
                (double)(Y * Math.Sin(deg) + Z * Math.Cos(deg))
                );
        }
        public Vector RotateY(double deg)
        {
            return new Vector(
                (double)(X * Math.Cos(deg) - Z * Math.Sin(deg)),
                Y,
                (double)(-X * Math.Sin(deg) + Z * Math.Cos(deg))
                );
        }

        public Vector RotateZ(double deg)
        {
            return new Vector(
                (double)(X * Math.Cos(deg) - Y * Math.Sin(deg)),
                (double)(X * Math.Sin(deg) + Y * Math.Cos(deg)),
                Z
                );
        }

    }
}
