using System;
using System.Diagnostics.Contracts;
using System.Drawing;
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
        
        private readonly double side1;
        private readonly double side2;
        private readonly double side3;

        #region Construction with Points
        
        //private Point point1;
        //private Point point2;
        //private Point point3;

        //public Triangle(Point point1, Point point2, Point point3)
        //{
        //    Contract.Requires(point1 != null && point2 != null && point3 != null);
        //    Contract.Ensures(Math.Round(angle12 + angle23 + angle31, ROUNDING_ACCURACY) == SUM_INTERNAL_ANGLES);

        //    if (point1.AreOnTheSameLine(point2, point3))
        //    {
        //        throw new ArgumentException("Can't have 3 points on same line");
        //    }

        //    this.point1 = point1;
        //    this.point2 = point2;
        //    this.point3 = point3;

        //    this.side1 = point1.GetLineLength(point2);
        //    this.side2 = point2.GetLineLength(point3);
        //    this.side3 = point3.GetLineLength(point1);

        //    InitializeAngles();
        //} 
        
        //private double GetInternalAngle(Point anglePoint, Point secondPoint, Point thirdPoint)
        //{
        //    Contract.Requires(anglePoint != null);
        //    Contract.Requires(secondPoint != null);
        //    Contract.Requires(thirdPoint != null);

        //    double side1 = anglePoint.GetLineLength(secondPoint);
        //    double oppositeSide = secondPoint.GetLineLength( thirdPoint);
        //    double side2 = thirdPoint.GetLineLength(anglePoint);

        //    return GetInternalAngle(side1, side2, oppositeSide);
        //}

        #endregion

        public Triangle(double side1, double side2, double side3)
        {
            Contract.Requires(0 < side1 && 0 < side2 && 0 < side3);
            
            if (side1+side2<=side3 || side1+side3<=side2 || side2+side3<=side1)
            {
                throw new ArgumentException("Invalid Triangle: sum of two sides cannot be less than the third side!");
            }

            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;

            InitializeAngles();
        }

        private void InitializeAngles()
        {
            angle12 = GetInternalAngle(this.side1, this.side2, this.side3);
            angle23 = GetInternalAngle(this.side2, this.side3, this.side1);
            angle31 = GetInternalAngle(this.side1, this.side3, this.side2);
        }

        private double Height
        {
            get
            {
                Contract.Ensures(Contract.Result<double>() > 0);

                return 2d*Area/GetMaxSide();
            }
        }

        private double Area
        {
            get
            {
                Contract.Ensures(Contract.Result<double>() > 0);
                double p = 0.5*(side1 + side2 + side3);
                double result = Math.Sqrt(p*(p - side1)*(p - side2)*(p - side3));
                return result;
            }
        }

        [ContractInvariantMethod]
        private void TriangleInvariants()
        {
            Contract.Invariant(Math.Round(angle12 + angle23 + angle31, ROUNDING_ACCURACY) == SUM_INTERNAL_ANGLES);
        }

        private double GetInternalAngle(double side1, double side2, double oppositeSide)
        {
            Contract.Ensures(0d < Contract.Result<double>() && Contract.Result<double>() <= SUM_INTERNAL_ANGLES);

            var baseSide = GetMaxSide();

            var height = Height;

            var angle1 = GetAngleForSide(side1, height);
            var angle2 = GetAngleForSide(side2, height);
            var angle3 = Math.Round(SUM_INTERNAL_ANGLES - angle1 - angle2, ROUNDING_ACCURACY);

            if (baseSide == oppositeSide)
                return angle3;
            if (baseSide == side2)
                return angle1;
            if (baseSide == side1)
                return angle2;

            throw new ApplicationException();
        }

        private double GetMaxSide()
        {
            return new[] { side1, side2, side3 }.Max();
        }

        private double GetMinSide()
        {
            return new[] { side1, side2, side3 }.Min();
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
            
            return (angle12 == angle23 || angle12 == angle31 || angle23 == angle31);
        }

        public bool IsScalene()
        {
            return !IsEquilateral() && !IsIsosceles();
        }

        private double GetAngleForSide(double hypothenuse, double height)
        {
            Contract.Ensures(Contract.Result<double>() > 0);
            
            var sin = height / hypothenuse;
            return Math.Round((Math.Asin(sin) * SUM_INTERNAL_ANGLES / Math.PI), ROUNDING_ACCURACY);
        }

        public void Render(Graphics graphics)
        {
            var clipBounds = graphics.VisibleClipBounds;

            double shorterSide = GetMinSide();
            double baseSide = GetMaxSide();

            var offset = (int)Math.Sqrt(shorterSide * shorterSide - Height * Height);

            int originX = (int)((int)(clipBounds.Width / 2) - baseSide/2);
            int originY = (int)((int)(clipBounds.Height / 2) - Height/2);
            
            var vertex1 = new Point(originX, originY);
            var vertex2 = new Point(originX + (int)baseSide, originY);
            var vertex3 = new Point(offset+originX, (int)Height + originY);

            graphics.DrawPolygon(new Pen(Color.RoyalBlue), new[] { vertex1, vertex2, vertex3 });

        }

        
    }
}