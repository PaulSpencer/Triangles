using System;

namespace GeometricalObjects
{
    public class Triangle
    {
        private readonly IGeometryCalculator _geometry;
        
        private double? angle1;
        private double? angle2;
        private double? angle3;
        
        #region Properties

        public Point Vertex1 { get; private set; }
        public Point Vertex2 { get; private set; }
        public Point Vertex3 { get; private set; }

        public double Angle1
        {
            get
            {
                if (angle1.HasValue)
                {
                    return angle1.Value;
                }
                angle1 = _geometry.GetInternalAngle(Vertex1, Vertex2, Vertex3);
                return angle1.Value;
            }
        }

        public double Angle2
        {
            get
            {
                if (angle2.HasValue)
                {
                    return angle2.Value;
                }
                angle2 = _geometry.GetInternalAngle(Vertex2, Vertex1, Vertex3);
                return angle2.Value;
            }

        }

        public double Angle3
        {
            get
            {
                if (angle3.HasValue)
                {
                    return angle3.Value;
                }
                angle3 = _geometry.GetInternalAngle(Vertex3, Vertex1, Vertex2);
                return angle3.Value;
            }
        } 
        
        #endregion

        internal Triangle(Point vertex1, Point vertex2, Point vertex3, IGeometryCalculator geometry)
        {
            _geometry = geometry;

            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }

        public bool IsEquilateral()
        {
            return (Angle1 == 60d && Angle2 == 60d && Angle3 == 60d);
        }

        public bool IsIsosceles()
        {
            if (IsEquilateral())
                return false;
            if (Angle1 == Angle2 || Angle1 == Angle3 || Angle2 == Angle3)
                return true;
            
            return false;
        }

        internal bool IsScalene()
        {
            return !IsEquilateral() && !IsIsosceles();
        }


    }
}