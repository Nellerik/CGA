using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CGA
{
    public class Canvas
    {
        private Grid canvas;
        private int width = 30;
        private int height = 30;
        private Action<Pixel> onPixelClick;

        public Canvas(Grid canvas, Action<Pixel> onPixelClick)
        {
            this.onPixelClick = onPixelClick;
            this.canvas = canvas;
            ResizeCanvas(width, height);
        }
        public void ResizeCanvas(int width, int height)
        {
            this.width = width;
            this.height = height;
            canvas.ColumnDefinitions.Clear();
            canvas.RowDefinitions.Clear();

            for (int i = 0; i < width; i++)
                canvas.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0;i < height; i++)
                canvas.RowDefinitions.Add(new RowDefinition());

            for(int i = 0; i < height; i++)
                for(int j = 0; j < width; j++)
                {
                    Button button = new Button();
                    button.Click += Pixel_Click;
                    button.Template = MainWindow.pixelButtonTemplate;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    canvas.Children.Add(button);
                }
        }
        private void Pixel_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int y = Grid.GetRow(button);
                int x = Grid.GetColumn(button);

                onPixelClick(new Pixel(x, y));
            }
        }
        public void Plot(int x, int y, SolidColorBrush brush)
        {
            Button button = FindPixelByCoordinates(x, y);
            button.Background = brush;
        }
        private Button FindPixelByCoordinates(int x, int y)
        {
            foreach (UIElement element in canvas.Children)
                if (element is Button && Grid.GetRow(element) == y && Grid.GetColumn(element) == x)
                    return (Button)element;

            return null;
        }
    }
}