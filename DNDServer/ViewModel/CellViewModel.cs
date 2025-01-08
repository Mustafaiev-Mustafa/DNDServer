using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

    namespace DNDServer.ViewModel
    {
        public class CellViewModel : INotifyPropertyChanged
        {
            public double X { get; set; }
            public double Y { get; set; }
            public PathGeometry HexagonGeometry { get; }
            private Brush _color;
            public Brush Color
            {
                get => _color;
                set
                {
                    if (_color != value)
                    {
                    _color = value;
                        OnPropertyChanged(nameof(Color));
                    }
                }
            }
            public CellViewModel(int x, int y, double size)
            {
                X = x;
                Y = y;
                HexagonGeometry = CreateHexagonGeometry(x, y, size);
            }
            private PathGeometry CreateHexagonGeometry(int x, int y, double size)
            {
                double width = size * Math.Sqrt(3);
                double height = size * 2;
                double offsetX = x * width + (y % 2 == 1 ? width / 2 : 0);
                double offsetY = y * (height * 0.75);

                double centerX = offsetX + width / 2;
                double centerY = offsetY + height / 2;

                PointCollection points = new PointCollection
                {
                    new Point(centerX, centerY - size),
                    new Point(centerX + size * Math.Sqrt(3) / 2, centerY - size / 2),
                    new Point(centerX + size * Math.Sqrt(3) / 2, centerY + size / 2),
                    new Point(centerX, centerY + size),
                    new Point(centerX - size * Math.Sqrt(3) / 2, centerY + size / 2),
                    new Point(centerX - size * Math.Sqrt(3) / 2, centerY - size / 2)
                };

                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = points[0],
                    IsClosed = true
                };
                pathFigure.Segments.Add(new PolyLineSegment(points, true));

                return new PathGeometry(new[] { pathFigure });
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
