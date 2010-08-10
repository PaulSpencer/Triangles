﻿using System;

namespace GeometricalObjects
{
    public class Triangle
    {
        private IGeometryCalculator geometory;
        private readonly double angleA;
        private readonly double angleB;
        private readonly double angleC;


        public Triangle(Point a, Point b, Point c, IGeometryCalculator geometory)
        {
            this.geometory = geometory;

            A = a;
            B = b;
            C = c;

            if (geometory.IsAStraightLine(A, B, C))
            {
                throw new ArgumentException("Can't have 3 points on same line");
            }

            angleA = geometory.CalculateInternalAngle(A, B, C);
            angleB = geometory.CalculateInternalAngle(B, C, A);
            angleC = geometory.CalculateInternalAngle(C, A, B);

            if (angleA + angleB + angleC != 180d)
            {
                throw new ArgumentException("All internal angles must add up to 180 degrees");
            }
        }


        public Point A { get; private set; }

        public Point B { get; private set; }

        public Point C { get; private set; }

        public bool IsEquilateral()
        {
            return angleA == 60d && angleB == 60d && angleC == 60d;
        }

        public bool IsIsosceles()
        {
            if (IsEquilateral())
            {
                return false;
            }

            return angleA == angleB || angleB == angleC || angleC == angleA;
        }

        internal bool IsScalene()
        {
            return !(IsIsosceles()  || IsEquilateral());
        }
    }
}