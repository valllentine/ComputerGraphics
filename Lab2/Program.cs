using System.Text;
using Classes.Interfaces;
using Classes.Utility;
using Classes.Scene;
using Classes.Objects;
using System.Diagnostics;


namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {

            int width = 500;
            int height = 500;

            Camera camera = new Camera(new Point(0, 0.2f, -1), new Vector(0, 0, 1), height, width);
            Scene scene = new Scene(camera);;

            Light light = new Light(new Vector(-1, 1, 1));
            scene.AddLight(light);

            string source = @"C:\Users\pmixe\OneDrive\Изображения\Документы\cow.obj";
            List<IObject> triangles = ObjFile.ReadObjectFile(source);
            TriangularObjects objects = new TriangularObjects(triangles);
            scene.AddObjects(triangles);
            objects.RotateZ(90);
            objects.RotateX(45);
            objects.Scale(0.9f, 0.91f, 0.9f);

            Stopwatch s = new Stopwatch();
            s.Start();

            double[,] screen = scene.GetScreenArray(50);
            s.Stop();
            Console.WriteLine($"Render time: {s.ElapsedMilliseconds}");

            string filename = @"C:\Users\pmixe\OneDrive\Рабочий стол\task.ppm"; 
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

