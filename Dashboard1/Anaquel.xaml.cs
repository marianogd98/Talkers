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
    /// Lógica de interacción para Anaquel.xaml
    /// </summary>
    public partial class Anaquel : Page
    {
        private double pos_anterior = 0;
        private List<BitmapImage> lista = new List<BitmapImage>();
        bool mover = false;
        public Anaquel()
        {
            InitializeComponent();

        }

        private void scroll_viewer_panel_MouseMove(object sender, MouseEventArgs e)
        {

            if (mover)
            {
                if (this.pos_anterior > e.GetPosition(this).X)
                {
                    scroll_viewer_panel.ScrollToHorizontalOffset(scroll_viewer_panel.HorizontalOffset + 6);

                }

                if (this.pos_anterior < e.GetPosition(this).X)
                {
                    scroll_viewer_panel.ScrollToHorizontalOffset(scroll_viewer_panel.HorizontalOffset - 6);

                }

                this.pos_anterior = e.GetPosition(this).X;
            }
        }

        private void contenedor_productos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mover = true;
        }

        private void contenedor_productos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.mover = false;
        }

        private void btn_izquierda_Click(object sender, RoutedEventArgs e)
        {
            scroll_viewer_panel.ScrollToHorizontalOffset(scroll_viewer_panel.HorizontalOffset + 100);
        }

        private void btn_derecha_Click(object sender, RoutedEventArgs e)
        {
            scroll_viewer_panel.ScrollToHorizontalOffset(scroll_viewer_panel.HorizontalOffset - 100);
        }
    }


}
