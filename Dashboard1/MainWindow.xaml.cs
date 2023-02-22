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
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        JsonPlaceHolderApi jsonPlaceHolderApi;
        LoginUser userConect;
        PrintSvc printSvc;
        DataGrid dataGrid;
        private DispatcherTimer RefreshTimer = new DispatcherTimer();
        private DataConfig _dataConfig;
        public MainWindow(LoginUser loginUser, DataConfig dataConfig)
        {
            InitializeComponent();

            if (dataConfig != null)
            {
                _dataConfig = dataConfig;
                jsonPlaceHolderApi = new JsonPlaceHolderApi(dataConfig.Dpto);
                printSvc = new PrintSvc();               
                userConect = loginUser;
                Productos pagina = new Productos(userConect, _dataConfig);
                main.Navigate(pagina);

            }
            else
            {
                MessageBox.Show("Error Por favor Revise el archivo de Configuración, la aplicación se cerrará.", "Error Configuración", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }


        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void DgHabladores_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        {
            var row = (DataGridRow)sender;
            row.DetailsVisibility = row.DetailsVisibility == Visibility.Collapsed ?
            Visibility.Visible : Visibility.Collapsed;
        }


        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            //(sender as DataGrid).RowEditEnding -= dgHabladores_RowEditEnding;
            /* var row = (CheckBox)sender;
             for (int i = 0; i < dgHabladores.Items.Count; i++)
             {
                 // var rowView = (Habladores)dgHabladores.Items[i]; //Get RowView
                 //rowView.PorImprimir = 1;
                 //dgHabladores.Items.CurrentItem.// = (ItemCollection) rowView;
                 //rowView.BeginEdit();
                 //rowView[0] = row.IsChecked;
                 //rowView.EndEdit();
                 //dgHabladores.Items.Refresh(); // Refresh table
             }*/

            //(sender as DataGrid).RowEditEnding += DataGrid_RowEditEnding;


            //DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
            /* for (int i = 0; i < dgHabladores.Items.Count; i++)
             {
                 Habladores check = (Habladores)dgHabladores.Items[i];
                 dgHabladores.Columns[0].SetValue(,true);
                 if (arr[i] == true)
                 {
                     check.Value = check.TrueValue;
                 }
             }*/
        }

        void ImpresionMasiva(Habladores hablador)
        {
            bool r = printSvc.SendTextFileToPrinter(hablador);
            if (r)
            {
                if (hablador.PorImprimir == 0)
                {
                    this.dataGrid.ItemsSource = jsonPlaceHolderApi.SetActualizarHablador(hablador.Codigo, userConect.Data.Id, userConect.Data.Id);
                }
            }
        }

        private void ChkActTodos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea salir?", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //private void RefreshHabladores_Click(object sender, RoutedEventArgs e)
        //{
        //    RefreshTimer.Stop();
        //    RefreshHabladores();
        //    RefreshTimer.Start();
        //}

        //public void BuscarOfertas()
        //{ //, StringFormat=d <- esto es de data binding por imprimir
        //    txtBuscar.Clear();
        //    txtBuscar.Focus();
        //    var data = jsonPlaceHolderApi.GetHabladores().Where(h => h.Oferta == 1).ToList();
        //    if (data.Count() > 0)
        //    {
        //        List<HabladoresViewModel> lhdvm = new List<HabladoresViewModel>();
        //        data.ForEach(d =>
        //        {
        //            string porImprimir = (d.PorImprimir == 0) ? "No Impreso" : "Ya fue Impreso";
        //            porImprimir += " - " + ((d.Oferta == 1) ? "Producto con precio de Oferta: " + d.Precioo : "");
        //            string fdate = Convert.ToDateTime(d.Fecha).ToString(@"dd/MM/yyyy HH\:mm");
        //            //string fimp = Convert.ToDateTime(d.FechHorImpresion).ToString(@"dd/MM/yyyy HH:mm");
        //            var obj = new HabladoresViewModel
        //            {
        //                Codigo = d.Codigo,
        //                Codigo_barra = d.Codigo_barra,
        //                Departamento = d.Departamento,
        //                Descripcion = d.Descripcion,
        //                DescripcionL = d.DescripcionL,
        //                Iva = d.Iva,
        //                Fecha = fdate,
        //                FechHorImpresion = d.FechHorImpresion,
        //                Oferta = d.Oferta,
        //                DescripcionOferta = porImprimir,
        //                Precio = d.Precio,
        //                Precioo = d.Precioo,
        //                Total = d.Total
        //            };
        //            lhdvm.Add(obj);
        //        });
        //        dgHabladores.ItemsSource = lhdvm;// jsonPlaceHolderApi.GetHabladores().Where(h => h.Oferta == 1).ToList();
        //    }
        //    else
        //    {
        //        MessageBox.Show("No Hay ofertas en esta Tienda", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //}

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            main.Navigate(new ProductosOferta(userConect, _dataConfig));
            
        }

        public void BuscarProductosBimoneda()
        {
            //txtBuscar.Clear();
            //txtBuscar.Focus();
            try
            {
                var data = jsonPlaceHolderApi.GetHabladores("001");

                if (data != null)
                {
                    data = data.ToList();
                    List<HabladoresViewModel> lhdvm = new List<HabladoresViewModel>();
                    data.ForEach(d =>
                    {
                        string porImprimir = (d.Moneda.Contains("0000000001")) ? "Producto de Costo en Bolivares" : "Producto de Costo en Dolares";
                        porImprimir += " - " + ((d.Oferta == 1) ? "Producto con precio de Oferta: " + d.Precioo : "");
                        var obj = new HabladoresViewModel
                        {
                            Codigo = d.Codigo,
                            Codigo_barra = d.Codigo_barra,
                            Departamento = d.Departamento,
                            Descripcion = d.Descripcion,
                            Fecha = d.Fecha,
                            FechHorImpresion = Convert.ToDateTime(d.FechHorImpresion).ToString(@"dd/MM/yyyy HH\:mm"),
                            Oferta = d.Oferta,
                            PorImprimir = 0,
                            Precio = d.Precio,
                            Precioo = d.Precioo
                        };
                        lhdvm.Add(obj);
                    });
                    //dgHabladores.ItemsSource = lhdvm;// jsonPlaceHolderApi.GetHabladores().Where(h => h.Oferta == 1).ToList();
                }
                else
                {
                    MessageBox.Show("No Existen Productos con Precio en Bs.", "Información de Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    //dgHabladores.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en carga de productos", "Error en servidor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private void Button_Click_Bimoneda(object sender, RoutedEventArgs e)
        //{
        //    BuscarProductosBimoneda();
        //}

      
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button tb = e.Source as Button;
            tb.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#d35400");
        }

    
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Productos pagina = new Productos(userConect, _dataConfig);
            main.Navigate(pagina);
        }

        private void restoreWindow_btn_Click(object sender, RoutedEventArgs e)
        {
            if ( this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void container_geren_categ_MouseEnter(object sender, MouseEventArgs e)
        {
            hijos_geren_categ.Visibility = Visibility.Visible;
        }

        private void container_geren_categ_MouseLeave(object sender, MouseEventArgs e)
        {
            hijos_geren_categ.Visibility = Visibility.Collapsed;
        }

        private void geren_categ_config_Click(object sender, RoutedEventArgs e)
        {
            main.Navigate( new Config() );
        }

        private void geren_categ_anaquel_Click(object sender, RoutedEventArgs e)
        {
            main.Navigate( new Anaquel() );
        }

        private void indices_repo_Click(object sender, RoutedEventArgs e)
        {
            main.Navigate(new IndicesRepo());
        }

        private void btn_actualizados_Click(object sender, RoutedEventArgs e)
        {
            main.Navigate(new ProductosActualizados( _dataConfig));
        }
    }
}

