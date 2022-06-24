using Classes.Interfaces;
using Classes.Utility;

namespace Classes.Objects
{
    public class TriangularObjects : ITransformable
    {
        private List<IObject> objects;

        public TriangularObjects(List<IObject> objects)
        {
            this.objects = objects;
        }

        public object RotateX(double deg)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i] = (IObject)objects[i].RotateX(deg);
            }
            return this;
        }
        
        public object RotateY(double deg)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i] = (IObject)objects[i].RotateY(deg);
            }
            return this;
        }

        public object RotateZ(double deg)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i] = (IObject)objects[i].RotateZ(deg);
            }
            return this;
        }

        public object Scale(double kx, double ky, double kz)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i] = (IObject)objects[i].Scale(kx, ky, kz);
            }
            return this;
        }

        public object Translate(Vector direction)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Translate(direction);
            }
            return this;
        }
    }
}
