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
    /// Interaction logic for ColorPanel.xaml
    /// </summary>
    public partial class ColorPanel : UserControl
    {
        private Color startingColor;

        public ColorPanel()
        {
            InitializeComponent();
            startingColor = Color.FromRgb(Convert.ToByte(RSlider.Value),
                                          Convert.ToByte(GSlider.Value),
                                          Convert.ToByte(BSlider.Value));
            this.Loaded += new RoutedEventHandler(ColorPanel_Loaded);
        }

        public void ColorPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.setPreviewColor(startingColor);
        }

        public Color getStartingColor() { return startingColor; }

        private void setPreviewColor(Color newColor)
        {
            this.ColorPreviewRectangle.Fill = new SolidColorBrush(newColor);
        }

        public static readonly RoutedEvent ColorChangedEvent = EventManager.RegisterRoutedEvent(
             "ColorChanged", RoutingStrategy.Bubble, typeof(ColorChangedEventHandler), typeof(ColorPanel));

        public event ColorChangedEventHandler ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        private void ColorSlidersChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!this.IsLoaded)
            {
                return;
            }
            ColorChangedEventArgs newEventArgs = new ColorChangedEventArgs(ColorPanel.ColorChangedEvent);
            newEventArgs.RValue = Convert.ToByte(RSlider.Value);
            newEventArgs.GValue = Convert.ToByte(GSlider.Value);
            newEventArgs.BValue = Convert.ToByte(GSlider.Value);
            newEventArgs.ColorValue = Color.FromRgb(newEventArgs.RValue,
                                                    newEventArgs.GValue,
                                                    newEventArgs.BValue);
            newEventArgs.BrushValue = new SolidColorBrush(newEventArgs.ColorValue);

            this.setPreviewColor(newEventArgs.ColorValue);
            RaiseEvent(newEventArgs);
        }
    }

    public class ColorChangedEventArgs : RoutedEventArgs
    {
        public ColorChangedEventArgs(RoutedEvent theEvent)
            : base(theEvent)
        {
        }

        public byte RValue { get; set; }
        public byte GValue { get; set; }
        public byte BValue { get; set; }
        public Color ColorValue { get; set; }
        public SolidColorBrush BrushValue { get; set; }
    }

    public delegate void ColorChangedEventHandler(object sender, ColorChangedEventArgs e);
}
