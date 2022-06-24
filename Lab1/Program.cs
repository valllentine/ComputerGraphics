using System;
using Classes.Utility;
using Classes.Scene;
using Classes.Objects;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating Scene
            Camera camera = new Camera(new Point(0, 0, -10), new Vector(0, 0, 1), 40, 200);
            Scene scene = new Scene(camera);

            //Defining an Objects
            Sphere s1 = new Sphere(new Point(2, -2, 100), 3);
            scene.AddObject(s1);
            Sphere s2 = new Sphere(new Point(0, 2, 10), 1);
            scene.AddObject(s2);
            Sphere s3 = new Sphere(new Point(0, -1.5, 7), 1);
            scene.AddObject(s3);
            Plane plane = new Plane(new Point(-6, 1, 1), new Vector(-1, 0, 0));
            scene.AddObject(plane);

            //Creating Light
            //Light light = new Light(new Vector(-1, 1, 2));
            //scene.AddLight(light);
            Light light = new Light(new Vector(-1, -1, 3));
            scene.AddLight(light);

            //Tracing
            double[,] screen = scene.getScreenArray();

            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    if (screen[i, j] <= 0)
                    {
                        Console.Write(' ');
                    }
                    else if (screen[i, j] > 0 && screen[i, j] <= 0.2)
                    {
                        Console.Write('.');
                    }
                    else if (screen[i, j] > 0.2 && screen[i, j] <= 0.5)
                    {
                        Console.Write('*');
                    }
                    else if (screen[i, j] > 0.5 && screen[i, j] <= 0.8)
                    {
                        Console.Write('O');
                    }
                    else
                    {
                        Console.Write('#');
                    }   
                }
                Console.WriteLine();
            }
        }
    }
}
