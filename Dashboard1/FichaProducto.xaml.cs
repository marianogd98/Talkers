using ConexApiRio.Model;
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
using System.Windows.Shapes;

namespace Dashboard1
{
    /// <summary>
    /// Lógica de interacción para FichaProducto.xaml
    /// </summary>
    public partial class FichaProducto : Window
    {
        private System.Drawing.Image mi_imagen;
        PrintSvc printSvc;
        private Producto _producto;
        public FichaProducto(Producto productoPOS)
        {
  
            InitializeComponent();

            this.printSvc = new PrintSvc();
            this._producto = productoPOS;
            this.llenarFicha(productoPOS);

        }


        public void llenarFicha(Producto mi_producto)
        {
            cod_prod.Text = mi_producto.codigo;
            descripcion.Text = mi_producto.descripcion;
            departamento.Text = mi_producto.departamento;
            cod_balanza.Text = mi_producto.codigoBalanza.Equals("") == true ? "Sin Código Balanza" : mi_producto.codigoBalanza;
            cod_barra.Text = mi_producto.barra;
            image_bar.Source = generatebarcode(mi_producto.barra);
            precio_detal.Text = mi_producto.precioDetal.ToString();
            precio_detal_bs.Text = mi_producto.precioDetalBs.ToString();
            precio_oferta.Text = mi_producto.precioOferta.ToString();
            precio_oferta_bs.Text = mi_producto.precioOfertaBs.ToString();
            impuesto.Text = mi_producto.impuesto;
            Impuesto_valor.Text = mi_producto.valor.ToString();
            descuento.Text = mi_producto.descuento.ToString();


            if (mi_producto.estatus == 1)
            {
                estatus_si.IsChecked = true;
                estatus_no.IsChecked = false;
            }
            else
            {
                estatus_si.IsChecked = false;
                estatus_no.IsChecked = true;
            }

            if (mi_producto.pesado  == 1)
            {
                pesado_si.IsChecked = true;
                pesado_no.IsChecked = false;
            }
            else
            {
                pesado_no.IsChecked = true;
                pesado_si.IsChecked = false;
            }

            if (mi_producto.codigoMoneda == "002")
            {
                moneda.SelectedItem = moneda.Items[1];
            }
            else
            {
                moneda.SelectedItem = moneda.Items[0];
            }

            if (mi_producto.tipo == 1)
            {
                tipo.SelectedItem = tipo.Items[0];
            }
            else
            {
                tipo.SelectedItem = tipo.Items[1];
            }

            if (mi_producto.fechaOfertaIni.Year != 1980)
            {
                fechaOfertaInicial.SelectedDate = mi_producto.fechaOfertaIni;
                fechaOfertaFinal.SelectedDate = mi_producto.fechaOfertaFin;

            }
        }

        private ImageSource generatebarcode(string code)
        {
            BarcodeLib.Barcode barra = new BarcodeLib.Barcode();
            barra.IncludeLabel = true;

            var image_cod_barra = barra.Encode(BarcodeLib.TYPE.CODE128, code, 190, 80);
            using (var ms = new MemoryStream())
            {
                image_cod_barra.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }


        }

        private void mostrar_cod_barra_Click(object sender, RoutedEventArgs e)
        {
            stack_cod_bar.Visibility = Visibility;
            for (int i = 0; i < 105 ; i =  i + 5)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i;
                cantidad_imprimir.Items.Add(item);
            }
        }

        private void imprimir_Click(object sender, RoutedEventArgs e)
        {
            int indice_seleccinado = cantidad_imprimir.SelectedIndex;
            if (indice_seleccinado != -1)
            {
                var seleccionado = (ComboBoxItem)cantidad_imprimir.Items[cantidad_imprimir.SelectedIndex];
                int cantidadImprimir = Convert.ToInt32(seleccionado.Content);
                for (int i = 0; i < cantidadImprimir; i++)
                {
                    this.printSvc.SendCodeBarraToPrinterCardProduct(this._producto);
                    Thread.Sleep(2000);
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar una cantidad a imprimir");
            }


        }

       

        //public void Printing()
        //{
        //    var appSettings = ConfigurationManager.AppSettings;
        //    try
        //    {
        //        PrintDocument pd = new PrintDocument();
        //        pd.PrintPage += new PrintPageEventHandler(documentoaimprimir);
        //        // Especifica que impresora se utilizara!!
        //        pd.PrinterSettings.PrinterName = appSettings["Impresora"];
        //        PageSettings pa = new PageSettings();
        //        pa.Margins = new Margins(5,5,5,5);
        //        pd.DefaultPageSettings.Margins = pa.Margins;
        //        PaperSize ps = new PaperSize("Custom",225, 100);
        //        pd.DefaultPageSettings.PaperSize = ps;
        //        pd.Print();
        //    }
        //    catch (Exception exp)
        //    {
        //        MessageBox.Show("Ha ocurrido un error al imprimir " + exp.Message);
        //    }
        //}

        //private void documentoaimprimir(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    try
        //    {

        //        using (Graphics g = e.Graphics)
        //        {
        //            g.DrawImage(mi_imagen,0,0);
                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ha ocurrido un error con al imprimir : "  + ex.Message);
        //    }
        //}

   

        private void volver_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
