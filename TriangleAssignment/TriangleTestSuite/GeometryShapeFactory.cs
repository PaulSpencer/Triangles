using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeometricalObjects
{
    public static class GeometryShapeFactory
    {
        public static Triangle CreateTriangle(Point point1, Point point2, Point point3, IGeometryCalculator calculator)
        {
            if (!calculator.IsAStraightLine(point1, point2, point3))
            {
                return new Triangle(point1,point2,point3,calculator);
            }

            throw new ArgumentException("Can't have 3 points on same line");
        }
    }
}
