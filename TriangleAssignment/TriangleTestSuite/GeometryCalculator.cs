using System;

namespace GeometricalObjects
{
    class GeometryCalculator : IGeometryCalculator
    {
        public bool IsAStraightLine(Point vertexOne, Point vertexTwo, Point vertexThree)
        {

            double slope = 0;
            double offset = 0;

            if (vertexOne.X - vertexTwo.X != 0)
            {
                slope = (vertexOne.Y - vertexTwo.Y) / (vertexOne.X - vertexTwo.X);
                offset = vertexOne.X * slope - vertexOne.Y;

                if (vertexThree.Y == slope * vertexThree.X + offset)
                    return true;
                return false;
            }

            if (vertexOne.X - vertexThree.X != 0)
            {
                slope = (vertexOne.Y - vertexThree.Y) / (vertexOne.X - vertexThree.X);
                offset = vertexOne.X * slope - vertexOne.Y;

                if (vertexTwo.Y == slope * vertexTwo.X + offset)
                    return true;
                return false;
            }


            if (vertexTwo.X - vertexThree.X != 0)
            {
                slope = (vertexTwo.Y - vertexThree.Y) / (vertexTwo.X - vertexThree.X);
                offset = vertexOne.X * slope - vertexOne.Y;

                if (vertexOne.Y == slope * vertexOne.X + offset)
                    return true;
                return false;
            }

            return true;
        }

        public double GetInternalAngle(Point firstPoint, Point secondPoint, Point thirdPoint)
        {
            throw new NotImplementedException();
        }
    }
}