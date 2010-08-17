using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Collections.Generic;

namespace GeometricalObjects
{
    public class Triangle
    {
        private readonly IGeometryCalculator _geometry;
        private readonly IDrawer _drawer; 

        private double _angle1;
        private double _angle2;
        private double _angle3;

        private Point _vertex1;
        private Point _vertex2;
        private Point _vertex3;

        
        internal Triangle(Point vertex1, Point vertex2, Point vertex3, IGeometryCalculator geometry, IDrawer drawer)
        {
            Contract.Requires(vertex1 != null);
            Contract.Requires(vertex2 != null);
            Contract.Requires(vertex3 != null);
            Contract.Ensures(_angle1 + _angle2 + _angle3 == 180);

            _geometry = geometry;
            _drawer = drawer;

            _vertex1 = vertex1;
            _vertex2 = vertex2;
            _vertex3 = vertex3;

            _angle1 = _geometry.GetInternalAngle(_vertex1, _vertex2, _vertex3);
            _angle2 = _geometry.GetInternalAngle(_vertex2, _vertex3, _vertex1);
            _angle3 = _geometry.GetInternalAngle(_vertex3, _vertex1, _vertex2);
        }

        public bool IsEquilateral()
        {
            return (_angle1 == 60d && _angle2 == 60d && _angle3 == 60d);
        }

        public bool IsIsosceles()
        {
            if (IsEquilateral())
                return false;
            if (_angle1 == _angle2 || _angle1 == _angle3 || _angle2 == _angle3)
                return true;
            
            return false;
        }

        public bool IsScalene()
        {
            return !IsEquilateral() && !IsIsosceles();
        }

        public void Render()
        {
            List<Point> points = new List<Point>() { _vertex1, _vertex2, _vertex3};
            _drawer.DrawPolygon(points);
        }
    }
}