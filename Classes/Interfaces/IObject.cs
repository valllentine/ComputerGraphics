using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes.Utility;

namespace Classes.Interfaces
{
    public interface IObject : ITransformable
    {
        bool IsIntersection(Point start, Vector direction);
        Point PointOfIntercept(Point start, Vector direction);
        Vector GetNormal(Point point);
    }
}
