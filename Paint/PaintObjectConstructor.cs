using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Reflection;

namespace Paint.PaintUtils
{
    public class PaintObjectConstructor
    {
        private PointCollection pointsGathered;
        private IPaintObjectConstructorListener constructorListener;
        private Type paintObjectType;
        private PaintObject temporaryObject;

        private Color color;
        private int thickness;

        private bool IsDragging = false;

        public PaintObjectConstructor(IPaintObjectConstructorListener listener)
        {
            this.constructorListener = listener;
        }

        public void setThickness(int thickness) { this.thickness = thickness; }
        public void setColor(Color color) { this.color = color; }
        public Color getColor() { return this.color; }
        public void setType(string typeName) { this.paintObjectType = Type.GetType(typeName, true); }

        public void MouseExited(object sender, MouseEventArgs e)
        {
            constructorListener.hoveringOverConstructionArea(null);
        }

        public void MouseMoved(object sender, MouseEventArgs e)
        {
            constructorListener.hoveringOverConstructionArea(makeHoveringPrototype(e.GetPosition(constructorListener.InputElement)));
            if (this.IsDragging)
            {
                this.MouseDragged(sender, e);
            }
        }

        public void MousePressed(object sender, MouseButtonEventArgs e)
        {
            this.IsDragging = true;

            pointsGathered = new PointCollection();
            pointsGathered.Add(e.GetPosition(constructorListener.InputElement));

            try
            {
                temporaryObject = Activator.CreateInstance(paintObjectType) as PaintObject;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error creating paintObjectType: " + exception.Message);
            }

            temporaryObject.setColor(color);
            temporaryObject.setThickness(thickness);
            temporaryObject.define(pointsGathered);

            constructorListener.hoveringOverConstructionArea(makeHoveringPrototype(e.GetPosition(((IPaintObjectConstructorListener)constructorListener).InputElement))); //
            constructorListener.constructionBeginning(temporaryObject);
        }

        public void MouseDragged(object sender, MouseEventArgs e)
        {
            pointsGathered.Add(e.GetPosition(constructorListener.InputElement)); //
            temporaryObject.define(pointsGathered); //
            constructorListener.hoveringOverConstructionArea(makeHoveringPrototype(e.GetPosition(constructorListener.InputElement))); //1
            constructorListener.constructionContinuing(temporaryObject);
        }

        public void MouseReleased(object sender, MouseButtonEventArgs e)
        {
            this.IsDragging = false;

            pointsGathered.Add(e.GetPosition(constructorListener.InputElement)); //1
            temporaryObject.define(pointsGathered); //1
            constructorListener.constructionComplete(temporaryObject);
            constructorListener.hoveringOverConstructionArea(null); //

            pointsGathered = null;
            temporaryObject = null;


        }

        private PaintObject makeHoveringPrototype(Point p)
        {
            PaintObject prototype = null;
            try
            {
                prototype = (PaintObject)Activator.CreateInstance(paintObjectType);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error creating paintObjectType error: " + exception.Message);
            }

            PointCollection points = new PointCollection(2);
            points.Add(p);
            points.Add(p); //
            prototype.define(points);
            prototype.setThickness(5);
            prototype.setColor(this.color);

            return prototype;
        }

    }
}
