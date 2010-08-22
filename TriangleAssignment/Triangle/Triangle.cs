using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace GeometricalObjects
{
    public class Triangle
    {
        private const double SUM_INTERNAL_ANGLES = 180d;
        private const int ROUNDING_ACCURACY = 7;

        private double angle12;
        private double angle23;
        private double angle31;
        
        private double side1;
        private double side2;
        private double side3;

        public Triangle(Point point1, Point point2, Point point3)
        {
            Contract.Requires(point1 != null && point2 != null && point3 != null);
            Contract.Ensures(Math.Round(angle12 + angle23 + angle31, ROUNDING_ACCURACY) == SUM_INTERNAL_ANGLES);

            if (point1.AreOnTheSameLine(point2, point3))
            {
                throw new ArgumentException("Can't have 3 points on same line");
            }

            this.side1 = point1.GetLineLength(point2);
            this.side2 = point2.GetLineLength(point3);
            this.side3 = point3.GetLineLength(point1);

            InitialiseAngles();
        }

        public Triangle(double side1, double side2, double side3)
        {
            Contract.Requires(0 < side1 && 0 < side2 && 0 < side3);
            Contract.Ensures(Math.Round(angle12 + angle23 + angle31, ROUNDING_ACCURACY) == SUM_INTERNAL_ANGLES);

            if (side1+side2<=side3 || side1+side3<=side2 || side2+side3<=side1)
            {
                throw new ArgumentException("Invalid Triangle: sum of two sides cannot be less than the third side!");
            }

            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;

            InitialiseAngles();
        }

        private void InitialiseAngles()
        {
            angle12 = GetInternalAngle(this.side1, this.side2, this.side3);
            angle23 = GetInternalAngle(this.side2, this.side3, this.side1);
            angle31 = GetInternalAngle(this.side1, this.side3, this.side2);
        }

        private double GetInternalAngle(double armSideOne, double armSideTwo, double oppositeSide)
        {
            Contract.Ensures(0d < Contract.Result<double>() && Contract.Result<double>() <= SUM_INTERNAL_ANGLES);

            var baseSide = new[] { armSideOne, oppositeSide, armSideTwo }.Max();

            var area = Area(armSideOne, oppositeSide, armSideTwo);

            var height = Height(area, baseSide);

            var angle1 = GetAngleForSide(armSideOne, height);
            var angle2 = GetAngleForSide(armSideTwo, height);
            var angle3 = Math.Round(SUM_INTERNAL_ANGLES - angle1 - angle2, ROUNDING_ACCURACY);


            if (baseSide == oppositeSide)
                return angle3;
            if (baseSide == armSideTwo)
                return angle1;
            if (baseSide == armSideOne)
                return angle2;

            throw new ApplicationException();
        }

        public bool IsEquilateral()
        {
            // Jamie's Comment: double equality... naughty.
            return (angle12 == 60d && angle23 == 60d && angle31 == 60d);
        }

        public bool IsIsosceles()
        {
            if (IsEquilateral())
                return false;
            if (angle12 == angle23 || angle12 == angle31 || angle23 == angle31)
                return true;
            
            return false;
        }

        public bool IsScalene()
        {
            return !IsEquilateral() && !IsIsosceles();
        }

        private double GetAngleForSide(double hypothenuse, double height)
        {
            var sin = height / hypothenuse;
            return Math.Round((Math.Asin(sin) * SUM_INTERNAL_ANGLES / Math.PI), ROUNDING_ACCURACY);
        }

        // Jamie's Comment - you are not getting the height, you are calculating it.
        // Still not sure about these methods.  Surely triangle should have a method
        // height() - the calling doesn't care how it's calculated...  Don't reveal your
        // imlpementation in the name.
        private double Height(double area, double baseSide)
        {
            Contract.Requires(0 < area);
            Contract.Requires(0 < baseSide);

            return 2d * area / baseSide;
        }

        private double Area(double sideOne, double sideTwo, double sideThree)
        {
            double p = 0.5 * (sideOne + sideTwo + sideThree);
            double result = Math.Sqrt(p * (p - sideOne) * (p - sideTwo) * (p - sideThree));
            return result;
        }

        private double GetInternalAngle(Point anglePoint, Point secondPoint, Point thirdPoint)
        {
            Contract.Requires(anglePoint != null);
            Contract.Requires(secondPoint != null);
            Contract.Requires(thirdPoint != null);
            // Jamie's Comment: that does this mean?  Refactor into a method that explains this.

            double armSideOne = anglePoint.GetLineLength(secondPoint);
            double oppositeSide = secondPoint.GetLineLength( thirdPoint);
            double armSideTwo = thirdPoint.GetLineLength(anglePoint);

            return GetInternalAngle(armSideOne, armSideTwo, oppositeSide);
        }

        public void Render()
        {
        }
    }
}