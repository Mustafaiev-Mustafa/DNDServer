using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DNDServer.Enum;
using DNDServer.Model;

namespace DNDServer.Service.MapLayer
{
    public class PhysicalMapLayerService : IMapLayerService
    {
        private readonly PhysicalInfo _info;
        public PhysicalMapLayerService(PhysicalInfo info)
        {
            _info = info;
        }
        public Brush GetCellColor(int x, int y)
        {
            return _info.Types[x, y] switch
            {
                PhysicalCellTypeEnum.Water => Brushes.Blue,
                PhysicalCellTypeEnum.Earth => Brushes.Brown,
                PhysicalCellTypeEnum.Mountain => Brushes.Gray,
                _ => Brushes.Transparent
            };
        }

        public string GetServiceName()
        {
            return "Physical";
        }
    }
}
