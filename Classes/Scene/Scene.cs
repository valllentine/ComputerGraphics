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
        
        public void AddLight(Light light)
        {
            this.light = light;
        }
        public static double theNearest(Vector vector, List<IObject> objects, Point camera, out IObject obj, out Point intercept)
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
        public double[,] getScreenArray()
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
                    double minDistance = theNearest(ray, Objects, cameraPos, out IObject nearestObj, out Point nearestIntercept);
                    if (nearestIntercept != null)
                    {
                        double minNumber = light.LightDir * nearestObj.GetNormal(nearestIntercept).Normalize();
                        screen[x, y] = minNumber;
                    }
                }
            }

            return screen;
        }
    }
}
