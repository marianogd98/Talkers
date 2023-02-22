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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard1
{
    /// <summary>
    /// Lógica de interacción para CartesianChart.xaml
    /// </summary>
    public partial class CartesianChart : UserControl
    {

        public string Title { get; set; }

        public CartesianChart()
        {
            InitializeComponent();


            this.DataContext = this;

            myCartesianCharts.Series = new LiveCharts.SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "Group A",
                    Values = new LiveCharts.ChartValues<double>{5},
                    Fill = (Brush)(new BrushConverter().ConvertFrom("#e67e22"))
                },
                 new LiveCharts.Wpf.ColumnSeries{
                     Title = "Group B",
                     Values = new LiveCharts.ChartValues<double>{10},
                     Fill = (Brush)(new BrushConverter().ConvertFrom("#6ab04c"))
                }
            };
        }
    }
}
