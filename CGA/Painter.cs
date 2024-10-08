using CGA.Table;
using CGA.Table.Rows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CGA
{
    public class Painter
    {
        private Canvas canvas;
        private List<Pixel> clickedPixels = new();
        private ResultsTable table;
        public Alghoritm alghoritm = Alghoritm.DigitalDifferentialAnalyzer;

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
            switch (alghoritm)
            {
                case Alghoritm.DigitalDifferentialAnalyzer:
                    DrawLineDDA(clickedPixels[0], clickedPixels[1]);
                    break;
                case Alghoritm.Bresenham:
                    DrawLineBresenham(clickedPixels[0], clickedPixels[1]);
                    break;
                case Alghoritm.Vu:
                    DrawLineAntiAliasing(clickedPixels[0], clickedPixels[1]);
                    break;
            }
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
            table.AddRow(new BresenhamResultRow(0, 0, x, -1 * (y - 29), 0));

            int xInk = p2.x >= p1.x ? 1 : -1;
            int yInk = p2.y >= p1.y ? 1 : -1;

            if (deltaX > deltaY)
            {
                e = 2 * deltaY - deltaX;
                int new_e = e;

                for (int i = 0; i < deltaX; i++)
                {
                    e = new_e;

                    if (e >= 0)
                    {
                        y = y + yInk;
                        new_e = e - 2 * deltaX;
                    }

                    x = x + xInk;
                    new_e = new_e + 2 * deltaY;
                    canvas.Plot(x, y, Brushes.Black);
                    table.AddRow(new BresenhamResultRow(i + 1, e, -1 * (x - 29), y, new_e));
                }
            }
            else
            {
                e = 2 * deltaX - deltaY;
                int new_e = e;

                for (int i = 0; i < deltaY; i++)
                {
                    e = new_e;

                    if (e >= 0)
                    {
                        x = x + xInk;
                        new_e = e - 2 * deltaY;
                    }

                    y = y + yInk;
                    new_e = new_e + 2 * deltaX;
                    canvas.Plot(x, y, Brushes.Black);
                    table.AddRow(new BresenhamResultRow(i + 1, e, x, -1 * (y - 29), new_e));
                }
            }
        }
        private void DrawLineAntiAliasing(Pixel p1,  Pixel p2)
        {
            if (p1.x == p2.x || p1.y == p2.y)
            {
                DrawLineBresenham(p1, p2);
                return;
            }

            int x0 = p1.x;
            int y0 = p1.y;
            int x1 = p2.x;
            int y1 = p2.y;

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (steep)
            {
                int temp = x0;
                x0 = y0;
                y0 = temp;

                temp = x1;
                x1 = y1;
                y1 = temp;
            }

            if (x0 > x1)
            {
                int temp = x0;
                x0 = x1;
                x1 = temp;

                temp = y0;
                y0 = y1;
                y1 = temp;
            }

            int dx = x1 - x0;
            int dy = y1 - y0;
            double gradient = dy / (double)dx;

            // First endpoint
            int xend = (int)Math.Round((double)x0);
            int yend = y0 + (int)(gradient * (xend - x0));
            double xgap = 1 - (x0 + 0.5 - Math.Floor(x0 + 0.5));
            int xpxl1 = xend;
            int ypxl1 = (int)Math.Floor((double)yend);
            if (steep)
            {
                canvas.Plot(ypxl1, xpxl1, 1 - xgap);
                canvas.Plot(ypxl1 + 1, xpxl1, xgap);
            }
            else
            {
                canvas.Plot(xpxl1, ypxl1, 1 - xgap);
                canvas.Plot(xpxl1, ypxl1 + 1, xgap);
            }

            double intery = yend + gradient; // first y-intersection for the main loop

            // Second endpoint
            xend = (int)Math.Round((double)x1);
            yend = y1 + (int)(gradient * (xend - x1));
            xgap = x0 + 0.5 - Math.Floor(x0 + 0.5);
            int xpxl2 = xend;
            int ypxl2 = (int)Math.Floor((double)yend);
            if (steep)
            {
                canvas.Plot(ypxl2, xpxl2, 1 - xgap);
                canvas.Plot(ypxl2 + 1, xpxl2, xgap);
            }
            else
            {
                canvas.Plot(xpxl2, ypxl2, 1 - xgap);
                canvas.Plot(xpxl2, ypxl2 + 1, xgap);
            }

            // Main loop
            if (steep)
            {
                for (int x = xpxl1 + 1, i = 0; x <= xpxl2 - 1; x++, i++)
                {
                    canvas.Plot((int)Math.Floor(intery), x, 1 - (intery - Math.Floor(intery)));
                    table.AddRow(new VuResultRow(i, Math.Floor(intery), x, (int)Math.Floor(intery), x, 1 - (intery - Math.Floor(intery))));
                    canvas.Plot((int)Math.Floor(intery) + 1, x, intery - Math.Floor(intery));
                    table.AddRow(new VuResultRow(i, Math.Floor(intery) + 1, x, (int)Math.Floor(intery) + 1, x, intery - Math.Floor(intery)));
                    intery += gradient;
                }
            }
            else
            {
                for (int x = xpxl1 + 1, i = 0; x <= xpxl2 - 1; x++, i++)
                {
                    canvas.Plot(x, (int)Math.Floor(intery), 1 - (intery - Math.Floor(intery)));
                    table.AddRow(new VuResultRow(i, x, Math.Floor(intery), x, (int)Math.Floor(intery), 1 - (intery - Math.Floor(intery))));
                    canvas.Plot(x, (int)Math.Floor(intery) + 1, intery - Math.Floor(intery));
                    table.AddRow(new VuResultRow(i, x, Math.Floor(intery) + 1, x, (int)Math.Floor(intery) + 1, intery - Math.Floor(intery)));
                    intery += gradient;
                }
            }
        }
        private float fpart(float num)
        {
            return num - MathF.Truncate(num);
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
