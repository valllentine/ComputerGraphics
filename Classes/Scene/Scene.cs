using Classes.Interfaces;
using System;
using System.Collections.Generic;
using Classes.Utility;

namespace Classes.Scene
{
    public class Scene
    {
        private List<IObject> Objects = new List<IObject>();
        private Camera camera;
        private Light light;

        public Scene(Camera camera)
        {
            this.camera = camera;
        }

        public void AddObject(IObject obj)
        {
            Objects.Add(obj);
        }
        public void AddObjects(List<IObject> objectsCollection)
        {
            for (int i = 0; i < objectsCollection.Count; i++)
            {
                Objects.Add(objectsCollection[i]);
            }
        }
        public void AddLight(Light light)
        {
            this.light = light;
        }
        public static double TheNearest(Vector vector, List<IObject> objects, Point camera, out IObject obj, out Point intercept)
        {
            double minDistance = Int32.MaxValue;
            obj = null;
            intercept = null;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].IsIntersection(camera, vector))
                {
                    Point tempIntercept = objects[i].PointOfIntercept(camera, vector);
                    double distance = Point.Distance(tempIntercept, camera);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        obj = objects[i];
                        intercept = tempIntercept;
                    }

                }
            }
            return minDistance;

        }
        public double[,] GetScreenArray()
        {
            camera.RefreshScreen();

            var screen = camera.Screen;
            var cameraPos = camera.Position;

            double fov = 60;
            Vector planeOrigin = cameraPos + camera.Direction.Normalize();

            int width = screen.GetUpperBound(0); 
            int height = screen.GetUpperBound(1); 


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    var xNorm = -(x - width / 2) / (double)width;
                    var yNorm = (y - height / 2) / (double)height;
                    float distanceToPlaneFromCamera = 1;
                    var fovInRad = fov / 180f * Math.PI;
                    double realPlaneHeight = (distanceToPlaneFromCamera * Math.Tan(fovInRad));
                    double realPlaneWidht = realPlaneHeight / height * width;

                    var xOnPlane = xNorm * realPlaneWidht / 2;
                    var yOnPlane = yNorm * realPlaneHeight / 2;

                    Vector positionOnPlane = planeOrigin + new Vector(xOnPlane, yOnPlane * 0.5, 0);
                    
                    Vector ray = positionOnPlane - new Vector(cameraPos.X, cameraPos.Y, cameraPos.Z);
                    double minDistance = TheNearest(ray, Objects, cameraPos, out IObject nearestObj, out Point nearestIntercept);
                    if (nearestIntercept != null)
                    {
                        double minNumber = light.LightDir * nearestObj.GetNormal(nearestIntercept).Normalize();
                        screen[x, y] = minNumber;
                    }
                }
            }
            return screen;

        }
        public double[,] GetScreenArray(int taskCount)
        {
            camera.RefreshScreen();

            var screen = camera.Screen;
            var cameraPos = camera.Position;

            float fov = 60;
            Vector planeOrigin = cameraPos + camera.Direction.Normalize();

            int width = screen.GetUpperBound(1) + 1; 
            int height = screen.GetUpperBound(0) + 1;

            float distanceToPlaneFromCamera = 1;
            var fovInRad = fov / 180f * Math.PI;
            float realPlaneHeight = (float)(distanceToPlaneFromCamera * Math.Tan(fovInRad));
            float realPlaneWidht = realPlaneHeight / height * width;

            int partPixelCount = height / taskCount;

            if (width % taskCount > 0)
            {
                taskCount++;
            }

            Task[] tasks = new Task[taskCount];

            int partPixelCounter = 0;
            for (int i = 0; i < taskCount; i++)
            {
                if (i + 1 == taskCount)
                {
                    partPixelCount = width - partPixelCounter;
                }

                int j = i;
                int taskPixelStart = partPixelCounter;
                int taskpartPixelCount = partPixelCount;

                tasks[j] = Task.Run(() => GetPartScreenArray(
                    planeOrigin,
                    realPlaneWidht,
                    realPlaneHeight,
                    taskPixelStart,
                    taskPixelStart + taskpartPixelCount,
                    width,
                    height));

                partPixelCounter += partPixelCount;
            }

            Task.WaitAll(tasks);

            int partCoordinatesCounter = 0;
            for (int i = 0; i < taskCount; i++)
            {
                var part = ((Task < double[,]>)tasks[i]).Result;

                for (int l = 0; l < part.GetUpperBound(1) + 1; l++)
                {
                    for (int j = 0; j < part.GetUpperBound(0) + 1; j++)
                    {
                        screen[j, partCoordinatesCounter] = part[j, l];
                    }

                    partCoordinatesCounter++;
                }
            }

            return screen;
        }

        public double[,] GetPartScreenArray(
            Vector planeOrigin,
            float realPlaneWidht,
            float realPlaneHeight,
            int startWidth,
            int endWidth,
            int width,
            int height)
        {
            double[,] partScreen = new double[height, endWidth - startWidth];
            int xIterator = 0;
            int yIterator = 0;

            Vector right_vec = Vector.Cross(camera.Direction, new Vector(1, 0, 0).Normalize()).Normalize();
            Vector up = Vector.Cross(camera.Direction, Vector.Neg(right_vec));

            var w = camera.Direction;
            var u = Vector.Cross(w, up);
            var v = Vector.Cross(u, w);

            double pixelHeight = realPlaneHeight / height;
            double pixelWidth = realPlaneWidht / width;

            var scanlineStart = planeOrigin - (width / 2) * pixelWidth * u + startWidth * pixelWidth * u + (pixelWidth / 2) * u +
                (height / 2) * pixelHeight * v - (pixelHeight / 2) * v;
            var scanlineStartPoint = new Point(scanlineStart.X, scanlineStart.Y, scanlineStart.Z);

            var pixelWidthU = pixelWidth * u;
            var pixelHeightV = pixelHeight * v;

            for (int x = 0; x < height; x++)
            {
                yIterator = 0;
                var pixelCenter = new Point(scanlineStartPoint.X, scanlineStartPoint.Y, scanlineStartPoint.Z);

                //var pixelCenter = planePoz;
                for (int y = startWidth; y < endWidth; y++)
                {

                    var ray = pixelCenter - camera.Position;

                    double minDistance = TheNearest(ray, Objects, camera.Position, out IObject nearestObj, out Point nearestIntercept);
                    if (nearestIntercept != null)
                    {
                        double minNumber = 0;
                        if (light != null)
                        {
                            Vector shadowVector = Vector.Neg(light.LightDir);
                            bool isShadow = false;

                            //foreach (var obj in Objects)
                            //{
                            //    if (obj.IsIntersection(nearestIntercept, shadowVector))
                            //        isShadow = true;
                            //}

                            minNumber = -(light.LightDir * nearestObj.GetNormal(nearestIntercept).Normalize());
                            if (isShadow)
                                minNumber /= 2;
                        }
                        else
                            minNumber = 0;

                        partScreen[x, yIterator] = minNumber;
                    }

                    yIterator++;
                    pixelCenter = new Point(pixelCenter.X + pixelWidthU.X, pixelCenter.Y + pixelWidthU.Y, +pixelCenter.Z + pixelWidthU.Z);
                }
                scanlineStartPoint = new Point(scanlineStartPoint.X - pixelHeightV.X, scanlineStartPoint.Y - pixelHeightV.Y, +scanlineStartPoint.Z - pixelHeightV.Z);
            }
            return partScreen;
        }
    }
}
