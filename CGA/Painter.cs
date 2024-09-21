using CGA.Table;
using CGA.Table.Rows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace CGA
{
    public class Painter
    {
        private Canvas canvas;
        private List<Pixel> clickedPixels = new();

        private ResultsTable table;

        public Painter(Grid canvasGrid, ResultsTable table)
        {
            canvas = new Canvas(canvasGrid, (p) =>
            {
                clickedPixels.Add(p);
                if (clickedPixels.Count == 1)
                    canvas.Plot(p.x, p.y, Brushes.Black);
                Draw();
            });
            this.table = table;
        }

        public void Draw()
        {
            if (clickedPixels.Count < 2)
                return;

            table.Clear();
            DrawLineDDA(clickedPixels[0], clickedPixels[1]);
            clickedPixels.Clear();
        }

        private void DrawLineDDA(Pixel p1, Pixel p2)
        {
            int deltaX = Math.Abs(p2.x - p1.x);
            int deltaY = Math.Abs(p2.y - p1.y);
            float length = Math.Max(deltaX, deltaY);

            float dx = (p2.x - p1.x) / length;
            float dy = (p2.y - p1.y) / length;

            float x = p1.x + Integer(0.5f * Sign(dx));
            float y = p1.y + Integer(0.5f * Sign(dy));
            canvas.Plot(Integer(x), Integer(y), Brushes.Black);
            table.AddRow(new DDAResultRow(0, x, -1*(y-29), Integer(x), Integer(-1 * (y - 29))));

            for(int i = 0; i < length; i++)
            {
                x += dx;
                y += dy;
                canvas.Plot(Integer(x), Integer(y), Brushes.Black);
                table.AddRow(new DDAResultRow(i + 1, x, -1 * (y - 29), Integer(x), Integer(-1 * (y - 29))));
            }
        }

        private void DrawLineBresenham(Pixel p1, Pixel p2)
        {
            int x = p1.x;
            int y = p1.y;
            int deltaX = Math.Abs(p2.x - p1.x);
            int deltaY = Math.Abs(p2.y - p1.y);
            int e;
            canvas.Plot(x, y, Brushes.Black);

            int xInk = p2.x >= p1.x ? 1 : -1;
            int yInk = p2.y >= p1.y ? 1 : -1;

            if (deltaX > deltaY)
            {
                e = 2 * deltaY - deltaX;

                for (int i = 0; i < deltaX; i++)
                {
                    if (e >= 0)
                    {
                        y = y + yInk;
                        e = e - 2 * deltaX;
                    }

                    x = x + xInk;
                    e = e + 2 * deltaY;
                    canvas.Plot(x, y, Brushes.Black);
                }
            }
            else
            {
                e = 2 * deltaX - deltaY;

                for(int i = 0; i < deltaY; i++)
                {
                    if (e >= 0)
                    {
                        x = x + xInk;
                        e = e - 2 * deltaY;
                    }

                    y = y + yInk;
                    e = e + 2 * deltaX;
                    canvas.Plot(x, y, Brushes.Black);
                }
            }
        }
        private int Sign(float num)
        {
            if (num < 0)
                return -1;
            else if (num == 0)
                return 0;
            else
                return 1;
        }
        private int Integer(float num)
        {
            return Convert.ToInt32(Math.Truncate(num));
        }
    }
}
