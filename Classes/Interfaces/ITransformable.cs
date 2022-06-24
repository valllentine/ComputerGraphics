using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Utility;

namespace Classes.Interfaces
{
    public interface ITransformable<T>
    {
        T Rotate(Vector eulers);

        T RotateX(double deg);
        T RotateY(double deg);
        T RotateZ(double deg);

        T Scale(Vector scale);

        T Move(Vector dir);
    }
}
