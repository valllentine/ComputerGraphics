using System.Text;
using Classes.Interfaces;
using Classes.Utility;
using Classes.Scene;
using Classes.Objects;
using System.Diagnostics;
using Lab2.Files;


namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {

            int width = 500;
            int height = 500;

            //string source = @"C:\Users\pmixe\OneDrive\Изображения\Документы\cow.obj";
            //List<IObject> triangles = ObjFile.ReadObjectFile(source);
            //TriangularObjects tobject = new TriangularObjects(triangles);
            //scene.AddObjects(triangles);
            //tobject.RotateZ(90);
            //tobject.RotateX(45);
            //tobject.Scale(2, 2, 2);

            //Creating Scene
            Camera camera = new Camera(new Point(0, 0, -1), new Vector(0, 0, 1), height, width);
            Scene scene = new Scene(camera);

            //Defining an Objects
            Sphere s1 = new Sphere(new Point(2, -2, 100), 10);
            scene.AddObject(s1);
            Sphere s2 = new Sphere(new Point(0, 2, 10), 5);
            scene.AddObject(s2);
            //Sphere s3 = new Sphere(new Point(0, -1.5, 10), 1);
            //scene.AddObject(s3);
            Plane plane = new Plane(new Point(-6, 1, 1), new Vector(-1, 0, 0));
            scene.AddObject(plane);

            //Creating Light
            Light light = new Light(new Vector(-1, 1, 1));
            scene.AddLight(light);

            Stopwatch s = new Stopwatch();
            s.Start();
            double[,] screen = scene.GetScreenArray(50);
            s.Stop();
            Console.WriteLine($"Render time: {s.ElapsedMilliseconds}");

            string filename = @"C:\Users\pmixe\OneDrive\Рабочий стол\ballresult.ppm";
            //string filename = @"C:\Users\pmixe\OneDrive\Рабочий стол\ballresult.ppm";
            StreamWriter destination = new StreamWriter(filename);
            destination.Write("P3\n{0} {1} {2}\n", width, height, 255);
            destination.Flush();


            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int a = Convert.ToInt32(screen[i, j] * 255);
                    if (screen[i, j] == 0)  
                        a = 255;

                    if (a > 255)
                        a = 255;
                    if (a < 0)
                        a = 0;

                    output.Append(a);
                    output.Append(" ");
                    output.Append(a);
                    output.Append(" ");
                    output.Append(a);
                    output.Append(" ");
                }
                destination.WriteLine(output);
                output.Clear();

            }

            destination.Close();
        }


    }
}

