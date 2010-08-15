using System;
using System.Linq;

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

        public double GetInternalAngle(Point anglePoint, Point secondPoint, Point thirdPoint)
        {
            var armSideOne = GetSide(anglePoint, secondPoint);
            var oppositeSide = GetSide(secondPoint, thirdPoint);
            var armSideTwo = GetSide(thirdPoint, anglePoint);

            var baseSide= new [] {armSideOne, oppositeSide, armSideTwo}.Max();
       
            var area = CalculateArea(armSideOne, oppositeSide, armSideTwo);

            var height = GetHeight(area, baseSide);

            if (baseSide == oppositeSide)
            {
                var angle1 = GetAngleForSide(armSideTwo, height) * 100;
                var angle2 = GetAngleForSide(armSideOne, height) * 100;
                return 180d - angle1 - angle2;
            }
            
            if (baseSide == armSideTwo)
            {
                return GetAngleForSide(armSideOne, height);
            }
            
            return GetAngleForSide(armSideTwo, height);
        }

        private double GetAngleForSide(double hypothenuse, double height)
        {
            var tempSide = Math.Sqrt(hypothenuse * hypothenuse - height * height);
            var cos = tempSide/hypothenuse;
            return Math.Acos(cos);
        }


        public double GetSide(Point vertexOne, Point vertexTwo)
        {
            double side;
            double changeInX = Math.Abs(vertexOne.X - vertexTwo.X);
            double changeInY = Math.Abs(vertexOne.Y - vertexTwo.Y);

            if (changeInX==0d)
            {
                return changeInY;
            }

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