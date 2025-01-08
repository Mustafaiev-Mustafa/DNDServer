using System.ComponentModel;
using System.Diagnostics;
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
using DNDServer.Enum;
using DNDServer.Model;
using DNDServer.Service.MapLayer;
using DNDServer.ViewModel;

namespace DNDServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var physicalInfo = new PhysicalInfo
            {
                Types = new PhysicalCellTypeEnum[10, 10]
            };
            var regionalInfo = new RegionalInfo
            {
                Regions = new List<Region>
                {
                    new Region()
                {
                    Name = "Tussent",
                    Cells = new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(1,1),
                        new Tuple<int, int>(2,2),
                    },
                    Color = Brushes.AliceBlue
                }
                }
            };
            var physicalLayerService = new PhysicalMapLayerService(physicalInfo);
            var regionalLayerService = new RegionalMapLayerService(regionalInfo);
            var serviceDictionary = new Dictionary<string, IMapLayerService>()
            {
                { physicalLayerService.GetServiceName(), physicalLayerService },
                { regionalLayerService.GetServiceName(), regionalLayerService }
            };

            DataContext = new MapViewModel(serviceDictionary);
        }
        private void Path_MouseEnter(object sender, MouseEventArgs e)
        {
            var path = sender as Path;
            if (path != null)
            {
                path.Fill = new SolidColorBrush(Colors.Gold);
            }
        }

        private void Path_MouseLeave(object sender, MouseEventArgs e)
        {
            var path = sender as Path;
            if (path?.DataContext is CellViewModel cell)
            {
                path.Fill = cell.Color;
            }
        }
    }
}