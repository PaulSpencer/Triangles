using System;

namespace GeometricalObjects
{
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
        {
            // contracts
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Point newPoint = obj as Point;
            if ((object)newPoint == null) return false;
            
            return (X == newPoint.X) && (Y == newPoint.Y);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("X = {0} Y = {1}",X, Y);
        }

        public static bool operator ==(Point a, Point b)
        {
            if (object.ReferenceEquals(a, b)) return true;

            if (((object)a == null) || ((object)b == null)) return false;
 
            // Return true if the fields match:
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
    }
}