using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics.Contracts;

namespace GeometricalObjects
{
    public static class GeometryShapeFactory
    {
        public static Triangle CreateTriangle(Point point1, Point point2, Point point3, IGeometryCalculator calculator, IDrawer drawer)
        {
            if (calculator.IsAStraightLine(point1, point2, point3))
            {
                throw new ArgumentException("Can't have 3 points on same line");                
            }

            return new Triangle(point1, point2, point3, calculator, drawer);
        }
    }
}
