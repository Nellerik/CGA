using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CGA.Table;
using CGA.Table.Rows;

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
        }

        private void DDA_Click(object sender, RoutedEventArgs e)
        {
            painter.alghoritm = Alghoritm.DigitalDifferentialAnalyzer;
        }

        private void Bresenham_Click(object sender, RoutedEventArgs e)
        {
            painter.alghoritm = Alghoritm.Bresenham;
        }

        private void Vu_Click(object sender, RoutedEventArgs e)
        {
            painter.alghoritm = Alghoritm.Vu;
        }
    }
}