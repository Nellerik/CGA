using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGA.Table.Rows
{
    public class DDAResultRow : DynamicRow
    {
        public int iterationNumber { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int plotX { get; set; }
        public int plotY { get; set; }

        public DDAResultRow(int iterationNumber, float x, float y, int plotX, int plotY)
        {
            this.iterationNumber = iterationNumber;
            this.x = x;
            this.y = y;
            this.plotX = plotX;
            this.plotY = plotY;
        }
    }
}
