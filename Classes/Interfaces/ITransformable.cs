using Classes.Utility;

namespace Classes.Interfaces
{
    public interface ITransformable
    {
        public object RotateX(double deg);

        public object RotateY(double deg);

        public object RotateZ(double deg);

        public object Scale(double kx, double ky, double kz);

        public object Translate(Vector dir);
    }
}
