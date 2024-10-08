using System.Windows;
using System.Windows.Controls;
using CGA.Table;

namespace CGA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ControlTemplate pixelButtonTemplate;
        private Painter painter;

        public MainWindow()
        {
            InitializeComponent();
            pixelButtonTemplate = (ControlTemplate)FindResource("PixelButton");
            painter = new Painter(Canvas, new ResultsTable(Table));
            previousSender = DDAAlghoritmSelectButton;
            previousSender.IsEnabled = false;
        }

        Button previousSender;

        private void DDA_Click(object sender, RoutedEventArgs e)
        {
            painter.alghoritm = Alghoritm.DigitalDifferentialAnalyzer;
            if (sender is Button s)
            {
                previousSender.IsEnabled = true;
                previousSender = s;
                previousSender.IsEnabled = false;
            }
        }

        private void Bresenham_Click(object sender, RoutedEventArgs e)
        {
            painter.alghoritm = Alghoritm.Bresenham;
            if (sender is Button s)
            {
                previousSender.IsEnabled = true;
                previousSender = s;
                previousSender.IsEnabled = false;
            }
        }

        private void Vu_Click(object sender, RoutedEventArgs e)
        {
            painter.alghoritm = Alghoritm.Vu;
            if (sender is Button s)
            {
                previousSender.IsEnabled = true;
                previousSender = s;
                previousSender.IsEnabled = false;
            }
        }
    }
}