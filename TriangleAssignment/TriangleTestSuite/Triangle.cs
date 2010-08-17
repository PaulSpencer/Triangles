using System;
using System.Diagnostics.Contracts;

namespace GeometricalObjects
{
    public class Triangle
    {
        private readonly IGeometryCalculator _geometry;

        private double angle1;
        private double angle2;
        private double angle3;
        
        internal Triangle(Point vertex1, Point vertex2, Point vertex3, IGeometryCalculator geometry)
        {
            Contract.Requires(vertex1 != null);
            Contract.Requires(vertex2 != null);
            Contract.Requires(vertex3 != null);
            _geometry = geometry;

            angle1 = _geometry.GetInternalAngle(vertex1, vertex2, vertex3);
            angle2 = _geometry.GetInternalAngle(vertex2, vertex3, vertex1);
            angle3 = _geometry.GetInternalAngle(vertex3, vertex1, vertex2);
        }

        public bool IsEquilateral()
        {
            return (angle1 == 60d && angle2 == 60d && angle3 == 60d);
        }

        public bool IsIsosceles()
        {
            if (IsEquilateral())
                return false;
            if (angle1 == angle2 || angle1 == angle3 || angle2 == angle3)
                return true;
            
            return false;
        }

        public bool IsScalene()
        {
            return !IsEquilateral() && !IsIsosceles();
        }
    }
}