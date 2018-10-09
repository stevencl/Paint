using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Paint
{
    public static class PaintCanvasCommands
    {
        static PaintCanvasCommands()
        {
            ClearCanvas = new RoutedUICommand(
               "Clear Canvas", "ClearCanvas",
               typeof(PaintCanvasCommands));

            UndoCanvas = new RoutedUICommand(
               "Undo my last stroke", "UndoCanvas",
               typeof(PaintCanvasCommands));

            ChangeToPencil = new RoutedUICommand(
                "Pencil", "ChangeToPencil",
                typeof(PaintCanvasCommands));

            ChangeToEraser = new RoutedUICommand(
                "Eraser", "ChangeToEraser",
                typeof(PaintCanvasCommands));

            ChangeToLine = new RoutedUICommand("Line", "ChangeToLine",typeof(PaintCanvasCommands));
        }

        public static RoutedUICommand ClearCanvas
        {
            get;
            set;
        }

        public static RoutedUICommand UndoCanvas
        {
            get;
            set;
        }

        public static RoutedUICommand ChangeToPencil
        {
            get;
            set;
        }


        public static RoutedUICommand ChangeToEraser
        {
            get;
            set;
        }

        public static RoutedUICommand ChangeToLine
        {
            get;
            set;
        }
    }


}
