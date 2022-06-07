using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Utility;

namespace Classes.Interfaces
{
    interface ITransformable<T>
    {
        T Rotate(Vector eulers);

        T RotateX(float deg);
        T RotateY(float deg);
        T RotateZ(float deg);

        T Scale(Vector scale);

        T Move(Vector dir);
    }
}
