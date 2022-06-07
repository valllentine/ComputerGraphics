using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Utility;

namespace Classes.Interfaces
{
    public interface IIntersectable
    {
        RayIntersection Intersects(Ray ray);
    }
}
