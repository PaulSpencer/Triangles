using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeometricalObjects;
using System.Drawing;


namespace GeometricalObjects
{
    public interface IGeometryCalculator
    {
        bool IsAStraightLine(Point vertexOne, Point vertexTwo, Point thirdPoint);

        double GetInternalAngle(Point anglePoint, Point secondPoint, Point thirdPoint);

        double GetSide(Point vertexOne, Point vertexTwo);

        double CalculateArea(double sideOne, double sideTwo, double sideThree);

        double GetHeight(double area, double baseSide);
    }
}

