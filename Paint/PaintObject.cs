using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace Paint
{
    //add another comment make another change
    public abstract class PaintObject
    {

        protected double thickness; //updated a comment
        protected Color color;

        public virtual void setColor(Color color) { this.color = color; }
        public Color getColor() { return color; }
        public virtual void setThickness(double thickness) { this.thickness = thickness; }
        public double getThickness() { return thickness; }

        public abstract PointCollection getPoints();
        public abstract double getStartX();
        public abstract double getStartY();
        public abstract double getEndX();
        public abstract double getEndY();


        public abstract Rect getBoundingBox();
        public abstract Path getRendering();
        public abstract void define(PointCollection points);
    }
}
