﻿using ConexApiRio.Model;
using ConexApiRio.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Page
    {
        JsonPlaceHolderApi jsonPlaceHolderApi;
        private DataConfig dataConfig;
        LoginUser _userConect;
        PrintSvc printSvc;
        private DispatcherTimer RefreshTimer = new DispatcherTimer();
        private DataConfig _dataConfig;
        private System.Drawing.Image mi_imagen;
        public Productos(LoginUser userConect, DataConfig _dataConfig)
        {
            InitializeComponent();
            dataConfig = _dataConfig;
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
            dgHabladores.ItemsSource = jsonPlaceHolderApi.GetHabladores();
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

            if (esNumero(txt_buscar))
            {
                List<Habladores> lista = jsonPlaceHolderApi.GetHablador(txt_buscar, chkActTodosDolar.IsChecked == true ? "1" : "0");

                if (lista != null)
                {
                    //BUSCAR POR CÓDIGO 
                    dgHabladores.ItemsSource = lista;
                }
                else
                {
                    //BUSCAR POR CÓDIGO DE BARRAS
                    dgHabladores.ItemsSource = jsonPlaceHolderApi.GetHabladorCodBarra(txt_buscar, chkActTodosDolar.IsChecked == true ? "1" : "0");
                }

            }
            else
            {
                dgHabladores.ItemsSource = jsonPlaceHolderApi.GetHabladoresDescripcion(txt_buscar, chkActTodosDolar.IsChecked == true ? "1" : "0");
            }

            //List<Habladores> lista_productos_oferta = jsonPlaceHolderApi.GetHabladores();
            //List<Habladores> lista = lista_productos_oferta.Where(producto => producto.Codigo.Contains(txt_buscar) || producto.Codigo_barra.Contains(txt_buscar) || producto.Descripcion.Contains(txt_buscar)).ToList();
            //dgHabladores.ItemsSource = lista;
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
                dgHabladores.ItemsSource = jsonPlaceHolderApi.GetHabladores();

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

        private void PrintHablador(object sender, RoutedEventArgs e)
        {
            Habladores mio = (Habladores)(sender as Button).DataContext;
            //model.DynamicText = (new Random().Next(0, 100).ToString());
            var cantidad = CantImprimir.CantidadAImprimir();

            for (int i = 0; i < cantidad; i++)
            {
                bool r = printSvc.SendTextFileToPrinter(mio);
                if (r)
                {
                    if (mio.PorImprimir == 0)
                    {
                        //dgHabladores.ItemsSource = jsonPlaceHolderApi.SetActualizarHablador(mio.Codigo, userConect.Data.Id, userConect.Data.Id);
                    }
                    if (!txtBuscar.Text.Equals(""))
                    {
                        FiltrarDatosDatagrid(txtBuscar.Text);
                    }
                }
                Thread.Sleep(2000);
            }

            
            //printSvc.ImprimirHablador();
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
            List<Habladores> listaHabladores = null;
            if (chkActTodosDolar.IsChecked == true)
            {
                //chkActTodos.IsChecked = false;            
                listaHabladores = jsonPlaceHolderApi.GetHabladores("1");
            }

            if (chkActTodosBs.IsChecked == true)
            {
                //chkActTodos.IsChecked = false;            
                listaHabladores = jsonPlaceHolderApi.GetHabladores("0");
            }

            if (listaHabladores != null)
            {
                dgHabladores.ItemsSource = listaHabladores;
                MessageBox.Show("Habladores Actualizados", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                txtBuscar.Text = "";
            }
            else
            {
                MessageBox.Show("No se encontraron Habladores", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {//ver ficha producto
            //messagebox.show("no disponible");
            var data = sender as Button;
            var hablador = data.DataContext as Habladores;
            Producto producto_encontrado = null;

            if (hablador.Codigo_barra.Equals("0"))
            {
                Producto producto = jsonPlaceHolderApi.GetProducto(hablador.CodigoSap);
                producto_encontrado = producto;
                
            }
            else
            {
                Producto producto = jsonPlaceHolderApi.GetProducto(hablador.Codigo_barra);
                producto_encontrado = producto;
            }

            if (producto_encontrado == null)
            {
                MessageBox.Show("No Existe el Producto con el Código de Barra Indicado", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                new FichaProducto(producto_encontrado).Show();
            }


        }

        private void refrescar_btn_Click(object sender, RoutedEventArgs e)
        {
            RefreshHabladores();
            MessageBox.Show("Habladores Actualizados","Mensaje", MessageBoxButton.OK,   MessageBoxImage.Information
                );
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Habladores hablador = (Habladores)(sender as Button).DataContext;
            var cantidad = CantImprimir.CantidadAImprimir();
            for (int i = 0; i < cantidad; i++)
            {
                bool r = printSvc.SendCodeBarraToPrinter(hablador);
                Thread.Sleep(2000);
            }
        }

    }
}
