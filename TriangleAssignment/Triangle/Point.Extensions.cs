﻿using System;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace GeometricalObjects
{
    public static class PointExtensions
    {
        #region Methods that used to be useful
        
        //public static bool AreOnTheSameLine(this Point vertexOne , Point vertexTwo, Point vertexThree)
        //{
        //    // Jamie's Comment: Like it!  DBC/Defensive programming.
        //    Contract.Requires(vertexOne != null);
        //    Contract.Requires(vertexTwo != null);
        //    Contract.Requires(vertexThree != null);

        //    if (vertexOne.X - vertexTwo.X != 0)
        //    {
        //        return ArePointsOnTheSameLine(vertexOne, vertexTwo, vertexThree);
        //    }

        //    if (vertexOne.X - vertexThree.X != 0)
        //    {
        //        return ArePointsOnTheSameLine(vertexOne, vertexThree, vertexTwo);
        //    }

        //    if (vertexTwo.X - vertexThree.X != 0)
        //    {
        //        return ArePointsOnTheSameLine(vertexTwo, vertexThree, vertexOne);
        //    }

        //    throw new ApplicationException();
        //}

        //private static bool ArePointsOnTheSameLine(Point vertexOne, Point vertexTwo, Point vertexThree)
        //{
        //    Contract.Requires(vertexOne.X - vertexTwo.X > 0);

        //    double slope = (vertexOne.Y - vertexTwo.Y) / (vertexOne.X - vertexTwo.X);
        //    double offset = vertexOne.X * slope - vertexOne.Y;

        //    if (vertexThree.Y == slope * vertexThree.X + offset)
        //        return true;

        //    return false;
        //} 

        #endregion

        public static double GetLineLength(this Point vertexOne, Point vertexTwo)
        {
            Contract.Requires(vertexOne != null);
            Contract.Requires(vertexTwo != null);

            double changeInX = Math.Abs(vertexOne.X - vertexTwo.X);
            double changeInY = Math.Abs(vertexOne.Y - vertexTwo.Y);

            if (changeInX == 0d)
            {
                return changeInY;
            }

            if (changeInY == 0d)
            {
                return changeInX;
            }

            return Math.Sqrt(changeInX * changeInX + changeInY * changeInY);
        }

    }
}
