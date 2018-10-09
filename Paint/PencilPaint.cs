using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint
{
    public class PencilPaint : PaintObject
    {
        protected PointCollection points; //This is a comment on points. This is a bad comment

        public PencilPaint()
        {
            this.points = new PointCollection();
        }

        public override double getStartX() { return points[0].X; }
        public override double getStartY() { return points[0].Y; }
        public override double getEndX() { return points[points.Count - 1].X; }
        public override double getEndY() { return points[points.Count - 1].Y; }


        public override void define(PointCollection points)
        {
            this.points = points;
        }

        public override PointCollection getPoints()
        {
            return points;
        }

        public override Rect getBoundingBox()
        {
            double minX = 100000, minY = 100000;
            double maxX = 0, maxY = 0;

            for (int pointIndex = points.Count - 1; pointIndex >= 0; pointIndex--)
            {
                double x = points[pointIndex].X;
                double y = points[pointIndex].Y;
                if (x - this.thickness / 2 < minX) minX = x - this.thickness / 2;
                else if (x + this.thickness / 2 > maxX) maxX = x + this.thickness / 2;
                if (y - this.thickness / 2 < minY) minY = y - this.thickness / 2;
                else if (y + this.thickness / 2 > maxY) maxY = y + this.thickness / 2;
            }

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        public override Path getRendering()
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = points[0];

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            for (int i = 1; i < points.Count; i++)
            {
                LineSegment myLineSegment = new LineSegment();
                myLineSegment.Point = points[i];
                myPathSegmentCollection.Add(myLineSegment);
            }
            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            SolidColorBrush scb = new SolidColorBrush(this.getColor());
            myPath.Stroke = scb;
            myPath.StrokeThickness = this.getThickness();
            myPath.Data = myPathGeometry;
            return myPath;
        }
    }
}
