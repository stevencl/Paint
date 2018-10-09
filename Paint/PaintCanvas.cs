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
    public class PaintCanvas : System.Windows.Controls.Canvas
    {
        private List<List<PaintObject>> history;
        private List<PaintObject> paintObjects;

        private PaintObject temporaryObject;
        private PaintObject hoveringObject;
        private Rectangle hoveringRender;

        public PaintCanvas()
            : base()
        {
            history = new List<List<PaintObject>>();
            paintObjects = new List<PaintObject>();

            hoveringRender = new Rectangle();
            hoveringRender.StrokeThickness = 1;
            hoveringRender.Stroke = Brushes.DarkGray;
            hoveringRender.Visibility = Visibility.Hidden;
            this.Children.Add(hoveringRender);
        }

        public void Repaint()
        {
            this.Children.Clear();
            foreach (PaintObject po in paintObjects)
            {
                this.Children.Add(po.getRendering());
            }

            this.Children.Add(hoveringRender); //
        }

        public int sizeOfHistory() { return history.Count; }

        public void setTemporaryObject(PaintObject temporaryObject)
        {
            if (temporaryObject != null)
            {
                this.temporaryObject = temporaryObject;
                this.Children.Add(temporaryObject.getRendering());
            }
        }

        public void setHoveringObject(PaintObject hoveringObject)
        {
            this.hoveringObject = hoveringObject;
            if (hoveringObject != null)
            {
                hoveringRender.Visibility = Visibility.Visible;
                hoveringRender.Width = hoveringRender.Height = hoveringObject.getThickness() + 2;
                double offset = hoveringObject.getThickness() / 2 + 1;
                hoveringRender.Fill = new SolidColorBrush(hoveringObject.getColor());
                PaintCanvas.SetLeft(hoveringRender, hoveringObject.getStartX() - offset);
                PaintCanvas.SetTop(hoveringRender, hoveringObject.getStartY() - offset);
                PaintCanvas.SetZIndex(hoveringRender, 10);
            }
            else { hoveringRender.Visibility = Visibility.Hidden; }
        }

        public void addPaintObject(PaintObject newObject)
        {
            history.Add(new List<PaintObject>(paintObjects));
            paintObjects.Add(newObject);
            this.Repaint();
        }

        public void clear()
        {
            history.Add(new List<PaintObject>(paintObjects)); //
            paintObjects.Clear();
            this.Repaint(); //
        }

        public void undo()
        {
            paintObjects = history.Last<List<PaintObject>>();
            history.RemoveAt(history.Count - 1);
            //this.Repaint();

        }
    }
}
