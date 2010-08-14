using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeometricalObjects;


namespace GeometricalObjects
{
    public interface IGeometryCalculator
    {
        bool IsAStraightLine(Point vertexOne, Point vertexTwo, Point thirdPoint);

        double GetInternalAngle(Point firstPoint, Point secondPoint, Point thirdPoint);

    }
}

