using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint
{
    public class EraserPaint : PencilPaint
    {
        public override void setColor(Color color)
        {
            this.color = Colors.AliceBlue;
        }

        //Adding a comment

        public override void setThickness(double thickness)
        {
            this.thickness = 25;
        }
    }
}
