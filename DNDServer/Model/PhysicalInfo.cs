using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DNDServer.Enum;

namespace DNDServer.Model
{
    public class PhysicalInfo
    {
        public PhysicalCellTypeEnum[,] Types = new PhysicalCellTypeEnum[5,5];
    }
    public class RegionalInfo
    {
        public List<Region> Regions = new List<Region>();
    }
    public class Region
    {
        public string Name { get; set; }
        public Brush Color { get; set; }
        public List<Tuple<int, int>> Cells { get; set; }
    }
}