using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GeometricalObjects
{
    public interface IDrawer
    {
        void DrawPolygon(IEnumerable<Point> points);
    }
}
