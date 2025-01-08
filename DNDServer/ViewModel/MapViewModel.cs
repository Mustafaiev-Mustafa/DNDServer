using System.Collections.ObjectModel;
using System.ComponentModel;
using DNDServer.Service.MapLayer;

namespace DNDServer.ViewModel
{
    public class MapViewModel
    {
        public ObservableCollection<CellViewModel> Cells { get; } = [];
        public readonly Dictionary<string, IMapLayerService> Services;
        public ObservableCollection<string> MapLayerNames { get; } = [];
        private string _currentServiceName;
        public string CurrentServiceName
        {
            get => _currentServiceName;
            set
            {
                if (_currentServiceName != value)
                {
                    _currentServiceName = value;
                    _currentService = Services[value];
                    OnPropertyChanged(nameof(CurrentServiceName));
                    UpdateColors();
                }
            }
        }
        private IMapLayerService _currentService;
        public IMapLayerService CurrentService
        {
            get => _currentService;
            set
            {
                if (_currentService != value)
                {
                    _currentService = value;
                }
            }
        }
        public MapViewModel(Dictionary<string, IMapLayerService> services)
        {
            Services = services;
            CurrentService = Services.First().Value;
            LoadCells();
            MapLayerNames = new ObservableCollection<string>();
            foreach(var key in services.Keys)
            {
                MapLayerNames.Add(key);
            }
        }

        private void LoadCells()
        {
            double size = 30;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var cell = new CellViewModel(x, y, size)
                    {
                        Color = _currentService.GetCellColor(x, y)
                    };
                    Cells.Add(cell);
                }
            }
        }

        public void UpdateColors()
        {
            foreach (var cell in Cells)
            {
                cell.Color = _currentService.GetCellColor((int)Math.Round(cell.X), (int)Math.Round(cell.Y));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
