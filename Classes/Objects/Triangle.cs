using System;
using Classes.Utility;
using System;
using Classes.Interfaces;

namespace Classes.Objects
{
    public class Triangle : IObject
    {
        private Point A;
        private Point B;
        private Point C;
        private Vector AB;
        private Vector BC;
        private Vector CA;

        public Triangle(Point a, Point b, Point c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public Triangle(Point a, Point b, Point c, Vector ab, Vector bc, Vector ca) : this(a, b, c)
        {
            this.AB = ab;
            this.BC = bc;
            this.CA = ca;
        }

        public Vector GetNormal(Point point)
        {

            if (AB != null && BC != null && CA != null)
            {
                var sa = Point.Distance(A, point);
                var sb = Point.Distance(B, point);
                var sc = Point.Distance(C, point);
                var sum = sa + sb + sc;

                sa /= sum;
                sb /= sum;
                sc /= sum;

                var b = (AB * sa) + (BC * sb) + (CA * sc);
                return b;
            }

            var e1 = B - A;
            var e2 = C - A;
            var a = Vector.Cross(e1, e2).Normalize();
            return a;
        }

        public bool IsIntersection(Point start, Vector direction)
        {
            var e1 = B - A;
            var e2 = C - A;

            var p = Vector.Cross(direction, e2);
            var det = e1 * p;

            if (det == 0)
            {
                return false;
            }
            else
            {
                var inv_det = 1 / det;
                var T = start - A;

                var u = T * p * inv_det;
                if (u < 0 || u > 1)
                {
                    return false;
                }
                else
                {
                    var q = Vector.Cross(T, e1);
                    var v = direction * q * inv_det;
                    if (v < 0 || u + v > 1)
                    {
                        return false;
                    }
                    else
                    {
                        var t = e2 * q * inv_det;
                        return true;
                    }
                }
            }
        }

        public Point PointOfIntercept(Point start, Vector direction)
        {
            var e1 = B - A;
            var e2 = C - A;

            var p = Vector.Cross(direction, e2);
            var det = e1 * p;

            if (det == 0)
            {
                throw new ArgumentException();
            }
            else
            {
                var inv_det = 1 / det;
                var T = start - A;

                var u = T * p * inv_det;
                if (u < 0 || u > 1)
                {
                    throw new ArgumentException();
                }
                else
                {
                    var q = Vector.Cross(T, e1);
                    var v = direction * q * inv_det;
                    if (v < 0 || u + v > 1)
                    {
                        throw new ArgumentException();
                    }
                    else
                    {
                        var t = e2 * q * inv_det;

                        var x = start.X + t * direction.X;
                        var y = start.Y + t * direction.Y;
                        var z = start.Z + t * direction.Z;
                        return new Point(x, y, z);
                    }
                }
            }
        }

        public object RotateX(double deg)
        {
            A.RotateX(deg);
            B.RotateX(deg);
            C.RotateX(deg);
            AB.RotateX(deg);
            BC.RotateX(deg);
            CA.RotateX(deg);
            return this;
        }

        public object RotateY(double deg)
        {
            A.RotateY(deg);
            B.RotateY(deg);
            C.RotateY(deg);
            AB.RotateY(deg);
            BC.RotateY(deg);
            CA.RotateY(deg);
            return this;
        }

        public object RotateZ(double deg)
        {

            A.RotateZ(deg);
            B.RotateZ(deg);
            C.RotateZ(deg);
            AB.RotateZ(deg);
            BC.RotateZ(deg);
            CA.RotateZ(deg);
            return this;
        }

        public object Scale(double kx, double ky, double kz)
        {
            A.Scale(kx, ky, kz);
            B.Scale(kx, ky, kz);
            C.Scale(kx, ky, kz);
            return this;
        }

        public object Translate(Vector direction)
        {
            A.Translate(direction);
            B.Translate(direction);
            C.Translate(direction);
            return this;
        }
    }
}
