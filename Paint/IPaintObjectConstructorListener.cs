using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint
{
    public interface IPaintObjectConstructorListener
    {
        void constructionBeginning(PaintObject temporaryObject);
        void constructionContinuing(PaintObject temporaryObject);
        void constructionComplete(PaintObject finalObject);
        void hoveringOverConstructionArea(PaintObject hoverObject);
        IInputElement InputElement { get; }
    }
}
