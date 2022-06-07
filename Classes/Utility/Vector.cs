using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes.Utility
{
    public class Vector : Interfaces.ITransformable<Vector>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector(float x, float y, float z)
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
        public static Vector operator +(Vector vec1, Vector vec2)
        {
            return new Vector(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }

        public static Vector operator +(Vector vec, float num)
        {
            return new Vector(vec.X + num, vec.Y + num, vec.Z + num);
        }

        public static Vector operator -(Vector vec1, Vector vec2)
        {
            return new Vector(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }

        public static Vector operator -(Vector vec, float num)
        {
            return new Vector(vec.X - num, vec.Y - num, vec.Z - num);
        }

        public static Vector operator *(Vector vec1, Vector vec2)
        {
            return new Vector(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);
        }

        public static Vector operator *(Vector vec, float num)
        {
            return new Vector(vec.X * num, vec.Y * num, vec.Z * num);
        }

        public static Vector operator /(Vector vec, float num)
        {
            return new Vector(vec.X / num, vec.Y / num, vec.Z / num);
        }

        public static Vector operator /(Vector vec1, Vector vec2)
        {
            return new Vector(vec1.X / vec2.X, vec1.Y / vec2.Y, vec1.Z / vec2.Z);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }

        public static float Distance(Vector vec1, Vector vec2)
        {
            return (vec2 - vec1).Magnitude();
        }

        public static float Dot(Vector vec1, Vector vec2)
        {
            return (vec1 * vec2).Magnitude();
        }

        public static Vector Cross(Vector lhs, Vector rhs)
        {
            return new Vector(
                lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.X * rhs.Z - lhs.Z * rhs.X,
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

        public Vector RotateX(float deg)
        {
            return new Vector(
                X,
                (float)(Y * Math.Cos(deg) - Z * Math.Sin(deg)),
                (float)(Y * Math.Sin(deg) + Z * Math.Cos(deg))
                );
        }

        public Vector RotateY(float deg)
        {
            return new Vector(
                (float)(X * Math.Cos(deg) - Z * Math.Sin(deg)),
                Y,
                (float)(-X * Math.Sin(deg) + Z * Math.Cos(deg))
                );
        }

        public Vector RotateZ(float deg)
        {
            return new Vector(
                (float)(X * Math.Cos(deg) - Y * Math.Sin(deg)),
                (float)(X * Math.Sin(deg) + Y * Math.Cos(deg)),
                Z
                );
        }
        public Vector Normalize()
        {
            var mag = Magnitude();
            return new Vector(X / mag, Y / mag, Z / mag);
        }
    }
}
