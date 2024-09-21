using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGA.Table.Rows
{
    public class VuResultRow : DynamicRow
    {
        public int iterationNumber { get; set; }
        public double x {  get; set; }
        public double y { get; set; }
        public int plotX { get; set; }
        public int plotY { get; set; }
        public double transparency { get; set; }
        public VuResultRow(int iteration, double x, double y, int plotX, int plotY, double transparency)
        {
            this.iterationNumber = iteration;
            this.x = x;
            this.y = y;
            this.plotX = plotX;
            this.plotY = plotY;
            this.transparency = Math.Round(transparency, 3);
        }
    }
}
