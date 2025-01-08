using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DNDServer.Service.MapLayer
{
    public interface IMapLayerService
    {
        Brush GetCellColor(int x, int y);
        string GetServiceName();
    }
}
