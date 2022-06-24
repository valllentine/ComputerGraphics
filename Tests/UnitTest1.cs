using System;
using Xunit;
using Classes.Utility;
using Classes.Scene;
using Classes.Objects;
using Classes.Interfaces;
using System.Collections.Generic;
using System.Collections;

namespace Tests
{
    public class UnitTest1
    {
        #region Utility
        [Theory]
        [ClassData(typeof(PointsTestData))]
        public void DistanceBetweenPoints(Point point1, Point point2, double expected)
        {
            var result = Point.Distance(point1, point2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 2, 3)]
        [InlineData(3, 4, 0, 5)]
        [InlineData(0, 0, 1, 1)]
        public void GetMagnitudeOfVectorCheckIsCorret(int x, int y, int z, float expected)
        {
            var vector = new Vector(x, y, z);
            var result = vector.Magnitude();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3, 4, 0, 0.6, 0.8, 0)]
        [InlineData(0, 0, 1, 0, 0, 1)]
        public void GetNormalizedVectorCheckIsCorret(int x, int y, int z, float expX, float expY, float expZ)
        {
            var vector = new Vector(x, y, z);
            var result = vector.Normalize();
            Assert.Equal((Math.Round(expX, 1), Math.Round(expY, 1), Math.Round(expZ, 1)), (result.X, result.Y, result.Z));
        }

        [Theory]
        [ClassData(typeof(VectorsCrossingTestData))]
        public void GetVectorCrossingCheckIsCorret(Vector vector1, Vector vector2, Vector expected)
        {
            var result = Vector.Cross(vector1, vector2);
            Assert.Equal((expected.X, expected.Y, expected.Z), (result.X, result.Y, result.Z));
        }

        #endregion

        #region ObjectsPlane

        [Theory]
        [ClassData(typeof(IntersectionWithPlaneTestData))]
        public void GetIntersectionOfVectorAndPlaneCheckIsCorret(Point start, Vector direction, bool expected)
        {
            Plane plane = new Plane(new Point(2, 1, 1), new Vector(1, 0, 0));
            var result = plane.IsIntersection(start, direction);
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(PointOfIntersectionWithPlaneTestData))]
        public void GetIntersectionOfVectorAndPlaneCheckPoint(Point start, Vector direction, Point expected)
        {
            Plane plane = new Plane(new Point(2, 1, 1), new Vector(1, 0, 0));
            var result = plane.PointOfIntercept(start, direction);
            Assert.Equal((expected.X, expected.Y, expected.Z), (result.X, result.Y, result.Z));
        }

        #endregion

        #region ObjectsSphere

        [Theory]
        [ClassData(typeof(IntersectionWithSphereTestData))]
        public void GetIntersectionOfVectorAndSphereCheckIsCorret(Point start, Vector direction, bool expected)
        {
            Sphere sphere = new Sphere(new Point(-7, 5, 2), 2);
            var result = sphere.IsIntersection(start, direction);
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(PointOfIntersectionWithSphereTestData))]
        public void GetIntersectionOfVectorAndSphereCheckPoint(Point start, Vector direction, Point expected)
        {
            Sphere sphere = new Sphere(new Point(-7, 5, 2), 2);
            var result = sphere.PointOfIntercept(start, direction);
            Assert.Equal((expected.X, expected.Y, expected.Z), (Math.Round(result.X, 2), Math.Round(result.Y, 2), Math.Round(result.Z, 2)));
        }

        #endregion

        #region RayTracing

        [Fact]
        public void FindMinDistanceFromVectorToObjectsIsTrue()
        {
            List<IObject> objects = new List<IObject>();
            Sphere sphere = new Sphere(new Point(-2, -2, 100), 4);
            Sphere sphere2 = new Sphere(new Point(0, 2, 10), 2);
            Plane plane = new Plane(new Point(2, 1, 1), new Vector(1, 0, 0));
            objects.Add(sphere);
            objects.Add(sphere2);
            objects.Add(plane);
            Vector light = new Vector(0.5, 0.5, 0.7);

            var resDistance = Scene.TheNearest(light, objects, new Point(0, 0, -10), out IObject resObj, out Point resIntercept);
            Assert.Equal(plane, resObj);
        }

        #endregion
    }

    #region DataForTests

    public class PointsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(0, 0, 0), new Point(1, 2, 2), 3 };
            yield return new object[] { new Point(1, 1, 1), new Point(1, 1, 1), 0 };
            yield return new object[] { new Point(-1, 1, -2), new Point(-1, 1, 0), 2 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }



    public class VectorsCrossingTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Vector(0, 0, 0), new Vector(1, 2, 2), new Vector(0, 0, 0) };
            yield return new object[] { new Vector(1, 1, 1), new Vector(1, 1, 1), new Vector(0, 0, 0) };
            yield return new object[] { new Vector(-1, 1, -2), new Vector(-1, 1, 0), new Vector(2, 2, 0) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class IntersectionWithPlaneTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(0, 0, 1), new Vector(0, 0, 1), false };
            yield return new object[] { new Point(0, 0, 1), new Vector(-2, 0, 1), false };
            yield return new object[] { new Point(0, 0, 1), new Vector(1, 1, 1), true };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class PointOfIntersectionWithPlaneTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(0, 0, 1), new Vector(1, 1, 1), new Point(2, 2, 3) };
            yield return new object[] { new Point(1, 0, 1), new Vector(1, 1, 1), new Point(2, 1, 2) };
            yield return new object[] { new Point(0, 1, 1), new Vector(1, 1, 1), new Point(2, 3, 3) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class IntersectionWithSphereTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(-0.1, 1.8, 1), new Vector(-0.8, 0.5, 0.1), true };
            yield return new object[] { new Point(1, 0, 1), new Vector(-1, 1, 1), false };
            yield return new object[] { new Point(1, 1, -10), new Vector(0.5, -0.5, 0.7), false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class PointOfIntersectionWithSphereTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(-0.1, 1.8, 1), new Vector(-0.8, 0.5, 0.1), new Point(-5.04, 4.89, 1.62) };
            yield return new object[] { new Point(1, 1, -10), new Vector(-0.5, 0.25, 0.8), new Point(-5.67, 4.33, 0.67) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    #endregion
}

