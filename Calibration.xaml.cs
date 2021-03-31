using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WordBlitzPlayer
{
    /// <summary>
    /// Interaction logic for Calibration.xaml
    /// </summary>
    public partial class Calibration : Window
    {
        private double ResizeFactor = 1.01;
        public bool CalibrationDone { get; set; } = true;

        private Point _TopLeftCoord { get; set; } = new Point(0, 0);
        public Point TopLeftCoord
        {
            get => _TopLeftCoord;
            set
            {
                _TopLeftCoord = value;
                CalibrationWindow.Top = value.Y * 0.80;
                CalibrationWindow.Left = value.X * 0.80;
            }
        }
 
        private double _BoxWidth { get; set; } = 39;

        public double BoxWidth
        {
            get => _BoxWidth;
            set
            {
                _BoxWidth = value;
                Letter1.Height = value;
                Letter1.Width = value;

                Letter2.Height = value;
                Letter2.Width = value;

                Letter3.Height = value;
                Letter3.Width = value;

                Letter4.Height = value;
                Letter4.Width = value;

                Letter5.Height = value;
                Letter5.Width = value;

                Letter6.Height = value;
                Letter6.Width = value;

                Letter7.Height = value;
                Letter7.Width = value;

                Letter8.Height = value;
                Letter8.Width = value;

                Letter9.Height = value;
                Letter9.Width = value;

                Letter10.Height = value;
                Letter10.Width = value;

                Letter11.Height = value;
                Letter11.Width = value;

                Letter12.Height = value;
                Letter12.Width = value;

                Letter13.Height = value;
                Letter13.Width = value;

                Letter14.Height = value;
                Letter14.Width = value;

                Letter15.Height = value;
                Letter15.Width = value;

                Letter16.Height = value;
                Letter16.Width = value;

            }
        }

        private double _ColumnWidth { get; set; } = 85;

        public double ColumnWidth
        {
            get => _ColumnWidth;
            set
            {
                _ColumnWidth = value;
                col1.Width = new System.Windows.GridLength(value, GridUnitType.Pixel);
                col2.Width = new System.Windows.GridLength(value, GridUnitType.Pixel);
                col3.Width = new System.Windows.GridLength(value, GridUnitType.Pixel);
                col4.Width = new System.Windows.GridLength(value, GridUnitType.Pixel);
            }
        }

        private double _RowHeight { get; set; } = 85;

        public double RowHeight
        {
            get => _RowHeight;
            set
            {
                _RowHeight = value;
                row1.Height = new System.Windows.GridLength(value, GridUnitType.Pixel);
                row2.Height = new System.Windows.GridLength(value, GridUnitType.Pixel);
                row3.Height = new System.Windows.GridLength(value, GridUnitType.Pixel);
                row4.Height = new System.Windows.GridLength(value, GridUnitType.Pixel);

            }
        }


        public List<Point> LetterBoxesCoord = new List<Point>();


        private readonly Point DefaultWindowPosition = new Point(736, 418);
        private readonly List<Point> DefaultCalibrationPoints = new List<Point>()
        {
            new Point(767,514), new Point(873,514), new Point(979,514), new Point(1086,514),
            new Point(767,621), new Point(873,621), new Point(979,621), new Point(1086,621),
            new Point(767,727), new Point(873,727), new Point(979,727), new Point(1086,727),
            new Point(767,833), new Point(873,833), new Point(979,833), new Point(1086,833)
        };

        public void LauchWIndow()
        {
            CalibrationWindow.Top = TopLeftCoord.Y * 0.80;
            CalibrationWindow.Left = TopLeftCoord.X * 0.80;
            this.Show();
        }

        public Calibration()
        {
            InitializeComponent();
            CalibrationWindow.DataContext = this;
            LetterBoxesCoord = DefaultCalibrationPoints;
            TopLeftCoord = DefaultWindowPosition;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Calibrate()
        {
            LetterBoxesCoord.Clear();
            BoxWidth = Letter1.ActualWidth;
            TopLeftCoord = CalibrationWindow.PointToScreen(new Point(0, 0));
            ColumnWidth = col1.ActualWidth;
            RowHeight = row1.ActualHeight;

            LetterBoxesCoord.Add(Letter1.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter2.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter3.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter4.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter5.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter6.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter7.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter8.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter9.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter10.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter11.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter12.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter13.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter14.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter15.PointToScreen(new Point(0, 0)));
            LetterBoxesCoord.Add(Letter16.PointToScreen(new Point(0, 0)));

            String message = string.Empty;

            message += $"tlc - x:{TopLeftCoord.X} y:{TopLeftCoord.Y} \n";

            int index = 1;

            foreach (Point point in LetterBoxesCoord)
            {
                message += $"(x:{point.X} y:{point.Y})";

                if (index % 4 == 0 && index != 0)
                    message += "\n";
                index++;
            }

            MessageBox.Show(message, "Calibration Done", MessageBoxButton.OK, MessageBoxImage.Information);
            CalibrationDone = true;
            this.Hide();
        }

        private void CalibrateButton_Click(object sender, RoutedEventArgs e)
        {
            Calibrate();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void ResizeUp_Click(object sender, RoutedEventArgs e)
        {
            if (4 * ColumnWidth * ResizeFactor > this.MaxWidth || 4 * RowHeight * ResizeFactor > this.MaxHeight)
            {
                this.Width = MaxWidth;
                this.Height = MaxHeight;
                return;
            }

            ColumnWidth = ColumnWidth * ResizeFactor;
            RowHeight = RowHeight * ResizeFactor;
            BoxWidth = BoxWidth * ResizeFactor;
        }

        private void ResizeDown_Click(object sender, RoutedEventArgs e)
        {

            ColumnWidth = ColumnWidth / ResizeFactor;
            RowHeight = RowHeight / ResizeFactor;
            BoxWidth = BoxWidth / ResizeFactor;
        }
    }
}
