using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGA.Table.Rows
{
    public class BresenhamResultRow : DynamicRow
    {
        public int iterationNumber { get; set; }
        public int e { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int new_e { get; set; }
        public BresenhamResultRow(int iteration, int e, int x, int y, int new_e)
        {
            this.iterationNumber = iteration;
            this.e = e;
            this.x = x;
            this.y = y;
            this.new_e = new_e;
        }
    }
}
