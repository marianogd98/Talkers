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

namespace Dashboard1
{
    /// <summary>
    /// Lógica de interacción para CantImprimir.xaml
    /// </summary>
    public partial class CantImprimir : Window
    {

        static public int cantidad { get; set; }
        static CantImprimir window;
        public CantImprimir()
        {
            InitializeComponent();

            cantidad = 0;

            for (int i = 0; i < 105; i = i + 5)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i;
                cantidad_a_imprimir.Items.Add(item);
            }
        }

        static public int CantidadAImprimir()
        {
            window = new CantImprimir();
            window.ShowDialog();
            return cantidad;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cantidad_a_imprimir.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar una cantidad a imprimir");
                }
                else
                {
                    var cant = (ComboBoxItem)cantidad_a_imprimir.SelectedItem;
                    cantidad = (int)cant.Content;

                    if (cantidad > 0)
                        this.Close();

                    else
                    {
                        MessageBox.Show("Debe seleccionar un numero mayor a \"0\" ");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
