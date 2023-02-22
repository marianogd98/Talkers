using ConexApiRio.Model;
using ConexApiRio.Service;
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
using System.Windows.Threading;

namespace Dashboard1
{
    /// <summary>
    /// Lógica de interacción para ProductosOferta.xaml
    /// </summary>
    public partial class ProductosOferta : Page
    {
        public ProductosOferta()
        {
            InitializeComponent();
        }

        JsonPlaceHolderApi jsonPlaceHolderApi;
        private DataConfig _dataConfig;
        LoginUser _userConect;
        PrintSvc printSvc;
        private DispatcherTimer RefreshTimer = new DispatcherTimer();
        public ProductosOferta(LoginUser userConect, DataConfig dataConfig)
        {
            InitializeComponent();
            _dataConfig = dataConfig;
            if (dataConfig != null)
            {
                _dataConfig = dataConfig;
                jsonPlaceHolderApi = new JsonPlaceHolderApi(dataConfig.Dpto);
                printSvc = new PrintSvc();
                RefreshHabladores();
                _userConect = userConect;
               
            }
            else
            {
                MessageBox.Show("Error Por favor Revise el archivo de Configuración, la aplicación se cerrará.", "Error Configuración", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void RefreshHabladores()
        {
            txtBuscar.Clear();
            txtBuscar.Focus();
            dgHabladores.ItemsSource = jsonPlaceHolderApi.GetOfertas();
        }
        private void PreloadHabladores()
        {
            // Install a timer to show each image.
            RefreshTimer.Interval = TimeSpan.FromMinutes(30);//FromSeconds(5);
            RefreshTimer.Tick += Tick;
            RefreshTimer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            RefreshHabladores();
        }
        private void RefreshHabladores_Click(object sender, RoutedEventArgs e)
        {
            RefreshTimer.Stop();
            RefreshHabladores();
            RefreshTimer.Start();
        }
        bool esNumero(string value)
        {
            long outvar = 0;
            return Int64.TryParse(txtBuscar.Text, out outvar);
        }
        private void FiltrarDatosDatagrid(string txt_buscar)
        {
            ///Al texto recibido si contiene un asterisco (*) lo reemplazo de la cadena
            ///para que no provoque una excepción.
            //string cadena = txt_buscar.Text.Trim().Replace("*", "");

            List<ProductoOferta> lista_productos_oferta = jsonPlaceHolderApi.GetOfertas();

            if (lista_productos_oferta == null)
            {
                MessageBox.Show("No se Encontraron Productos en Oferta", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                List<ProductoOferta> lista = lista_productos_oferta.Where(producto => producto.codigo.Contains(txt_buscar) || producto.barra.Contains(txt_buscar) || producto.descri.Contains(txt_buscar)).ToList();
                dgHabladores.ItemsSource = lista;
            }

        }

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBuscar.Text.Equals(""))
            {
                //dgHabladores.ItemsSource = jsonPlaceHolderApi.GetHabladores();
            }
            else
            {
                //dgHabladores.ItemsSource = jsonPlaceHolderApi.GetHablador(txtBuscar.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!txtBuscar.Text.Equals(""))
                FiltrarDatosDatagrid(txtBuscar.Text);
            else
                dgHabladores.ItemsSource = jsonPlaceHolderApi.GetOfertas();

        }

        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!txtBuscar.Text.Equals(""))
                    FiltrarDatosDatagrid(txtBuscar.Text);
                else
                    dgHabladores.ItemsSource = jsonPlaceHolderApi.GetHabladores();
            }
        }


        void ImpresionMasiva(Habladores hablador)
        {
            bool r = printSvc.SendTextFileToPrinter(hablador);
            if (r)
            {
                if (hablador.PorImprimir == 0)
                {
                    dgHabladores.ItemsSource = jsonPlaceHolderApi.SetActualizarHablador(hablador.Codigo, _userConect.Data.Id, _userConect.Data.Id);
                }
            }
        }

        private void BtnImprimirTodo_Click(object sender, RoutedEventArgs e)
        {
            if (chkTodos.IsChecked == true)
            {
                for (int i = 0; i < dgHabladores.Items.Count; i++)
                {
                    var hablador = (Habladores)dgHabladores.Items[i]; //Get RowView
                    ImpresionMasiva(hablador);
                    Task.Delay(1000).Wait();
                }
                chkTodos.IsChecked = false;

                MessageBox.Show("Habladores Impresos y Actualizados", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                txtBuscar.Text = "";
                txtBuscar.Focus();
            }
        }

        private void BtnActualizaTodo_Click(object sender, RoutedEventArgs e)
        {
            if (chkActTodosBs.IsChecked == false && chkActTodosDolar.IsChecked == false)
            {
                MessageBox.Show("No se encontraron productos en oferta", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

                if (chkActTodosBs.IsChecked == true)
                {
                    columnaPrecioDolar.Visibility = Visibility.Hidden;
                    columnaPrecioOfertaDolar.Visibility = Visibility.Hidden;
                    columnaPrecioBs.Visibility = Visibility.Visible;
                    ColumnaPrecioOfertaBs.Visibility = Visibility.Visible;

                }
                else if (chkActTodosDolar.IsChecked == true)
                {
                    columnaPrecioDolar.Visibility = Visibility.Visible;
                    columnaPrecioOfertaDolar.Visibility = Visibility.Visible;
                    columnaPrecioBs.Visibility = Visibility.Hidden;
                    ColumnaPrecioOfertaBs.Visibility = Visibility.Hidden;

                }

                MessageBox.Show("Productos Actualizados", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);

            }


        }


        private void refrescar_btn_Click(object sender, RoutedEventArgs e)
        {
            RefreshHabladores();
            MessageBox.Show("Habladores Actualizados", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information
                );
        }







    }
}
