using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Interfaces;

namespace Classes.Utility
{
    public class Vector : ITransformable
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
        public static Vector Neg(Vector u)
        {
            return new Vector(-u.X, -u.Y, -u.Z);
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
        public static Vector operator *(double num, Vector vec)
        {
            return new Vector(vec.X * num, vec.Y * num, vec.Z * num);
        }
        public static Vector Cross(Vector lhs, Vector rhs)
        {
            return new Vector(
                lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.Z * rhs.X - lhs.X * rhs.Z,
                lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        public object RotateX(double deg)
        {
            deg = (float)(deg * Math.PI / 180.0);
            float newY = (float)(Y * Math.Cos(deg) - Z * Math.Sin(deg));
            float newZ = (float)(Y * Math.Sin(deg) + Z * Math.Cos(deg));
            Y = newY;
            Z = newZ;
            return this;

        }

        public object RotateZ(double deg)
        {
            deg = (float)(deg * Math.PI / 180.0);
            float newX = (float)(Z * Math.Sin(deg) + X * Math.Cos(deg));
            float newZ = (float)(Z * Math.Cos(deg) - X * Math.Sin(deg));
            X = newX;
            Z = newZ;
            return this;
        }

        public object RotateY(double deg)
        {
            deg = (float)(deg * Math.PI / 180.0);

            float newX = (float)(X * Math.Cos(deg) - Y * Math.Sin(deg));
            float newY = (float)(X * Math.Sin(deg) + Y * Math.Cos(deg));
            X = newX;
            Y = newY;
            return this;
        }

        public object Scale(double kx, double ky, double kz)
        {
            X *= kx;
            Y *= ky;
            Z *= kz;
            return this;
        }

        public object Translate(Vector direction)
        {
            X += direction.X;
            Y += direction.Y;
            Z += direction.Z;
            return this;
        }

    }
}
