using Paint.PaintUtils;
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

            DrawingCanvas.MouseLeave += new MouseEventHandler(objectConstructor.MouseExited);
            DrawingCanvas.MouseDown += new MouseButtonEventHandler(objectConstructor.MousePressed);
            DrawingCanvas.MouseUp += new MouseButtonEventHandler(objectConstructor.MouseReleased);
            DrawingCanvas.MouseMove += new MouseEventHandler(objectConstructor.MouseMoved);
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
            //((PaintCanvas)DrawingCanvas).setTemporaryObject(temporaryObject);
            DrawingCanvas.setTemporaryObject(temporaryObject);
        }

        public void constructionContinuing(PaintObject temporaryObject)
        {
            DrawingCanvas.setTemporaryObject(temporaryObject); //
        }

        public void constructionComplete(PaintObject finalObject)
        {
            DrawingCanvas.setTemporaryObject(null);
            DrawingCanvas.addPaintObject(finalObject);
        }

        public void hoveringOverConstructionArea(PaintObject hoverObject)
        {
            DrawingCanvas.setHoveringObject(hoverObject);
        }

        public IInputElement InputElement
        {
            get { return DrawingCanvas; }
        }

        private void UndoCanvas_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (DrawingCanvas != null)
            {
                e.CanExecute = DrawingCanvas.sizeOfHistory() > 0;
            }
        }

        private void AlwaysExecutableCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ClearCanvas_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DrawingCanvas != null)
            {
                DrawingCanvas.clear();
            }
        }

        private void UndoCanvas_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DrawingCanvas != null)
            {
                DrawingCanvas.undo();
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
