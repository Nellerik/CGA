using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGA
{
    public struct Pixel
    {
        public readonly int x;
        public readonly int y;

        public Pixel(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
