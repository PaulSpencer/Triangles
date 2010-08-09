using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeometricalObjects
{
    public interface IGeometryCalculator
    {
        bool IsAStraightLine(Point firstPoint, Point secondPoint, Point thirdPoint);
        double CalculateInternalAngle(Point endPoint, Point leftStart, Point rightStart);
    }
}
