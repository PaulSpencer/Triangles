using System;

namespace GeometricalObjects
{
    public class Triangle
    {
        IGeometryCalculator geometory;
        public Triangle(Point a, Point b, Point c, IGeometryCalculator geometory)
        {
            this.geometory = geometory;

            A = a;
            B = b;
            C = c;

            if (!IsValidTriangle())
            {
                throw new ArgumentException("Can't have 3 points on same line");
            }
        }

        private bool IsValidTriangle()
        {
            return !(A.X == B.X && B.X == C.X) && !(A.Y == B.Y && B.Y == C.Y);
        }

        public Point A
        {
            get; private set;
        }

        public Point B
        {
            get; private set;
        }

        public Point C
        {
            get; private set;
        }
    }
}