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

namespace CGA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ControlTemplate pixelButtonTemplate;
        private Painter painter;
        public static ObservableCollection<IterationResult> iterationResults;

        public MainWindow()
        {
            InitializeComponent();
            pixelButtonTemplate = (ControlTemplate)FindResource("PixelButton");
            painter = new Painter(Canvas);
            iterationResults = new ObservableCollection<IterationResult>();
            Table.ItemsSource = iterationResults;
        }
    }
}