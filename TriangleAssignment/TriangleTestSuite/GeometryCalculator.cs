using System;
using System.Linq;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace GeometricalObjects
{
    class GeometryCalculator : IGeometryCalculator
    {
        private const double SUM_INTERNAL_ANGLES = 180d;

        public bool IsAStraightLine(Point vertexOne, Point vertexTwo, Point vertexThree)
        {
            Contract.Requires(vertexOne != null);
            Contract.Requires(vertexTwo != null);
            Contract.Requires(vertexThree != null);

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

        public double GetInternalAngle(Point anglePoint, Point secondPoint, Point thirdPoint)
        {
            Contract.Requires(anglePoint != null);
            Contract.Requires(secondPoint != null);
            Contract.Requires(thirdPoint != null);
            Contract.Ensures(0d < Contract.Result<double>() && Contract.Result<double>() <= SUM_INTERNAL_ANGLES); 

            var armSideOne = GetSide(anglePoint, secondPoint);
            var oppositeSide = GetSide(secondPoint, thirdPoint);
            var armSideTwo = GetSide(thirdPoint, anglePoint);

            var baseSide= new [] {armSideOne, oppositeSide, armSideTwo}.Max();
       
            var area = CalculateArea(armSideOne, oppositeSide, armSideTwo);

            var height = GetHeight(area, baseSide);
            double theAngle;
            if (baseSide == oppositeSide)
            {
                var angle1 = GetAngleForSide(armSideOne, height) ;
                var angle2 = GetAngleForSide(armSideTwo, height);
                theAngle = SUM_INTERNAL_ANGLES - angle1 - angle2;
            }
            else
            {
                theAngle = baseSide == armSideTwo ?  GetAngleForSide(armSideOne, height) : GetAngleForSide(armSideTwo, height);
            }            
            
            return Math.Round(theAngle, 7);
        }

        private double GetAngleForSide(double hypothenuse, double height)
        {
            var sin = height/hypothenuse;
            return Math.Asin(sin) * SUM_INTERNAL_ANGLES / Math.PI;
        }


        public double GetSide(Point vertexOne, Point vertexTwo)
        {
            Contract.Requires(vertexOne != null);
            Contract.Requires(vertexTwo != null);

            double changeInX = Math.Abs(vertexOne.X - vertexTwo.X);
            double changeInY = Math.Abs(vertexOne.Y - vertexTwo.Y);

            if (changeInX==0d)
            {
                return changeInY;
            }

            // is this a bug? should it be change in Y? have we tested for this?
            if (changeInX == 0d)
            {
                return changeInX;
            }

            return Math.Sqrt(changeInX*changeInX + changeInY*changeInY);
        }

        public double CalculateArea(double sideOne, double sideTwo, double sideThree)
        {
            double p = 0.5*(sideOne + sideTwo + sideThree);
            double result = Math.Sqrt(p*(p - sideOne)*(p - sideTwo)*(p - sideThree));
            return result;
        }

        public double GetHeight(double area, double baseSide)
        {
            if (baseSide == 0d)
                throw new ArgumentException("Base side cannot be of lenght 0!");
            return 2d*area/baseSide;
        }
    }   
}