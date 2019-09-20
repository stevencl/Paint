using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Paint.PaintUtils;

namespace Paint
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window, IPaintObjectConstructorListener
    {
        private PaintObjectConstructor objectConstructor;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        public void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            objectConstructor = new PaintObjectConstructor(this);
            objectConstructor.setType("Paint.PencilPaint");
            objectConstructor.setColor(ColorSliderPanel.getStartingColor());
            objectConstructor.setThickness(5);

            ((PaintCanvas)DrawingCanvas).MouseLeave += new MouseEventHandler(objectConstructor.MouseExited);
            ((PaintCanvas)DrawingCanvas).MouseDown += new MouseButtonEventHandler(objectConstructor.MousePressed);
            ((PaintCanvas)DrawingCanvas).MouseUp += new MouseButtonEventHandler(objectConstructor.MouseReleased);
            ((PaintCanvas)DrawingCanvas).MouseMove += new MouseEventHandler(objectConstructor.MouseMoved);
        }

        // Call this method when the radio buttons change (pencil / line / eraser)
        public void setPaintObjectType(String paintObjectTypeName)
        {
            objectConstructor.setType(paintObjectTypeName);
        }

        private void SetPaintColor(object sender, ColorChangedEventArgs e)
        {
            objectConstructor.setColor(e.ColorValue);
        }

        public void constructionBeginning(PaintObject temporaryObject)
        {
            ((PaintCanvas)DrawingCanvas).setTemporaryObject(temporaryObject);
        }

        public void constructionContinuing(PaintObject temporaryObject)
        {
            ((PaintCanvas)DrawingCanvas).setTemporaryObject(temporaryObject); //
        }

        public void constructionComplete(PaintObject finalObject)
        {
            ((PaintCanvas)DrawingCanvas).setTemporaryObject(null);
            ((PaintCanvas)DrawingCanvas).addPaintObject(finalObject);
        }

        public void hoveringOverConstructionArea(PaintObject hoverObject)
        {
            ((PaintCanvas)DrawingCanvas).setHoveringObject(hoverObject);
        }

        public IInputElement InputElement
        {
            get { return ((PaintCanvas)DrawingCanvas); }
        }

        private void UndoCanvas_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (((PaintCanvas)DrawingCanvas) != null)
            {
                e.CanExecute = ((PaintCanvas)DrawingCanvas).sizeOfHistory() > 0;
            }
        }

        private void AlwaysExecutableCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ClearCanvas_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (((PaintCanvas)DrawingCanvas) != null)
            {
                ((PaintCanvas)DrawingCanvas).clear();
            }
        }

        private void UndoCanvas_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (((PaintCanvas)DrawingCanvas) != null)
            {
                ((PaintCanvas)DrawingCanvas).undo();
            }
        }

        private void setPaintObjectType(object sender, ExecutedRoutedEventArgs e)
        {
            String typeName = "";
            RoutedUICommand sourceCommand = e.Command as RoutedUICommand;

            switch (sourceCommand.Text)
            {
                case "Pencil":
                    typeName = "Paint.PencilPaint";
                    break;
                case "Eraser":
                    typeName = "Paint.EraserPaint";
                    break;
                case "Line":
                    typeName = "Paint.LineTo";
                    break;
            }

            objectConstructor.setType(typeName);
        }

    }
}
