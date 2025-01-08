using System.Windows.Media;
using DNDServer.Model;

namespace DNDServer.Service.MapLayer
{
    public class RegionalMapLayerService : IMapLayerService
    {
        private Dictionary<Tuple<int, int>, Brush> cellColorMap;
        private readonly RegionalInfo _info;
        public RegionalMapLayerService(RegionalInfo info)
        {
            _info = info;
            InitializeCellColorMap(info);
        }
        private void InitializeCellColorMap(RegionalInfo info)
        {
            cellColorMap = new Dictionary<Tuple<int, int>, Brush>();

            foreach (var region in info.Regions)
            {
                foreach (var cell in region.Cells)
                {
                    if (!cellColorMap.ContainsKey(cell))
                    {
                        cellColorMap[cell] = region.Color;
                    }
                }
            }
        }
        public Brush GetCellColor(int x, int y)
        {
            var key = Tuple.Create(x, y);
            return cellColorMap != null && cellColorMap.TryGetValue(key, out var color)
                ? color
                : Brushes.Transparent;
        }

        public string GetServiceName()
        {
            return "Regional";
        }
    }
}
