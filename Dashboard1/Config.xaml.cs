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
    /// Lógica de interacción para Config.xaml
    /// </summary>
    public partial class Config : Page
    {
        public Config()
        {
            InitializeComponent();
           
        }

        private void btn_agregar_pasillo_Click(object sender, RoutedEventArgs e)
        {

            //CREAR ITEM DE NÚMERO DE PASILLO

            TreeViewItem item_pasillo = new TreeViewItem();
            item_pasillo.SetValue(TreeViewItem.NameProperty, "pasillo_item");
            var template_pasillo = new DataTemplate();
            template_pasillo.VisualTree = new FrameworkElementFactory(typeof(Grid));
            var icono_pasillo = new FrameworkElementFactory(typeof(MaterialDesignThemes.Wpf.PackIcon));
            var textblock_pasillo = new FrameworkElementFactory(typeof( TextBlock ));
            textblock_pasillo.SetValue( TextBlock.TextProperty, "NÚMERO PASILLO" );
            textblock_pasillo.SetValue(TextBlock.MarginProperty, new Thickness(28, 0, 0, 0));
            icono_pasillo.SetValue(MaterialDesignThemes.Wpf.PackIcon.KindProperty, MaterialDesignThemes.Wpf.PackIconKind.ArrowLeftRightBold);
            template_pasillo.VisualTree.AppendChild(icono_pasillo);
            template_pasillo.VisualTree.AppendChild(textblock_pasillo);
            item_pasillo.HeaderTemplate = template_pasillo;

            tienda_item.Items.Add(item_pasillo);



            //CREAR ITEM DE ESTANTES

            TreeViewItem item_estante = new TreeViewItem();
            item_estante.SetValue( TreeViewItem.NameProperty, "estante_item" );
            var template_estante = new DataTemplate();
            template_estante.VisualTree = new FrameworkElementFactory(typeof(Grid));
            var icono_estante = new FrameworkElementFactory(typeof(MaterialDesignThemes.Wpf.PackIcon));
            var textblock_estante = new FrameworkElementFactory(typeof(TextBlock));
            textblock_estante.SetValue(TextBlock.TextProperty, "ESTANTES");
            textblock_estante.SetValue(TextBlock.MarginProperty, new Thickness(28, 0, 0, 0));
            icono_estante.SetValue(MaterialDesignThemes.Wpf.PackIcon.KindProperty, MaterialDesignThemes.Wpf.PackIconKind.TableColumn);
            template_estante.VisualTree.AppendChild(icono_estante);
            template_estante.VisualTree.AppendChild(textblock_estante);
            item_estante.HeaderTemplate = template_estante;

            item_pasillo.Items.Add(item_estante);

            //CREAR ITEM NIVELES PASILLO

            TreeViewItem item_niveles_pasillo = new TreeViewItem();
            item_niveles_pasillo.SetValue(TreeViewItem.NameProperty, "niveles_pasillo_item");
            var template_niveles_pasillo = new DataTemplate();
            template_niveles_pasillo.VisualTree = new FrameworkElementFactory(typeof(Grid));
            var icono_niveles_pasillo = new FrameworkElementFactory(typeof(MaterialDesignThemes.Wpf.PackIcon));
            var textblock_niveles_pasillo = new FrameworkElementFactory(typeof(TextBlock));
            textblock_niveles_pasillo.SetValue(TextBlock.TextProperty, "NIVELES PASILLO");
            textblock_niveles_pasillo.SetValue(TextBlock.MarginProperty, new Thickness(28, 0, 0, 0));
            icono_niveles_pasillo.SetValue(MaterialDesignThemes.Wpf.PackIcon.KindProperty, MaterialDesignThemes.Wpf.PackIconKind.FormatLineWeight);
            template_niveles_pasillo.VisualTree.AppendChild(icono_niveles_pasillo);
            template_niveles_pasillo.VisualTree.AppendChild(textblock_niveles_pasillo);
            item_niveles_pasillo.HeaderTemplate = template_niveles_pasillo;

            item_estante.Items.Add(item_niveles_pasillo);


            //CREAR ITEM FAMILIA PRODUCTOS

            TreeViewItem item_familia_productos = new TreeViewItem();
            item_familia_productos.SetValue(TreeViewItem.NameProperty, "familia_productos_item");
            var template_familia_productos = new DataTemplate();
            template_familia_productos.VisualTree = new FrameworkElementFactory(typeof(Grid));
            var icono_familia_productos = new FrameworkElementFactory(typeof(MaterialDesignThemes.Wpf.PackIcon));
            var textblock_familia_productos = new FrameworkElementFactory(typeof(TextBlock));
            textblock_familia_productos.SetValue(TextBlock.TextProperty, "FAMILIA DE PRODUCTOS");
            textblock_familia_productos.SetValue(TextBlock.MarginProperty, new Thickness(28, 0, 0, 0));
            icono_familia_productos.SetValue(MaterialDesignThemes.Wpf.PackIcon.KindProperty, MaterialDesignThemes.Wpf.PackIconKind.Cart);
            template_familia_productos.VisualTree.AppendChild(icono_familia_productos);
            template_familia_productos.VisualTree.AppendChild(textblock_familia_productos);
            item_familia_productos.HeaderTemplate = template_familia_productos;

            item_niveles_pasillo.Items.Add(item_familia_productos);

            //CREAR ITEM MARCA DE PRODUCTOS

            TreeViewItem item_marca_productos = new TreeViewItem();
            item_marca_productos.SetValue(TreeViewItem.NameProperty, "marca_productos_item");
            var template_marca_productos = new DataTemplate();
            template_marca_productos.VisualTree = new FrameworkElementFactory(typeof(Grid));
            var icono_marca_productos = new FrameworkElementFactory(typeof(MaterialDesignThemes.Wpf.PackIcon));
            var textblock_marca_productos = new FrameworkElementFactory(typeof(TextBlock));
            textblock_marca_productos.SetValue(TextBlock.TextProperty, "MARCA DE PRODUCTOS");
            textblock_marca_productos.SetValue(TextBlock.MarginProperty, new Thickness(28, 0, 0, 0));
            icono_marca_productos.SetValue(MaterialDesignThemes.Wpf.PackIcon.KindProperty, MaterialDesignThemes.Wpf.PackIconKind.TagTextOutline);
            template_marca_productos.VisualTree.AppendChild(icono_marca_productos);
            template_marca_productos.VisualTree.AppendChild(textblock_marca_productos);
            item_marca_productos.HeaderTemplate = template_marca_productos;

            item_familia_productos.Items.Add(item_marca_productos);

            //CREAR PRODUCTOS POR NIVEL

            TreeViewItem item_productos_nivel = new TreeViewItem();
            item_productos_nivel.SetValue(TreeViewItem.NameProperty, "productos_nivel_item");
            var template_productos_nivel = new DataTemplate();
            template_productos_nivel.VisualTree = new FrameworkElementFactory(typeof(Grid));
            var icono_productos_nivel = new FrameworkElementFactory(typeof(MaterialDesignThemes.Wpf.PackIcon));
            var textblock_productos_nivel = new FrameworkElementFactory(typeof(TextBlock));
            textblock_productos_nivel.SetValue(TextBlock.TextProperty, "PRODUCTOS POR NIVEL");
            textblock_productos_nivel.SetValue(TextBlock.MarginProperty, new Thickness(28, 0, 0, 0));
            icono_productos_nivel.SetValue(MaterialDesignThemes.Wpf.PackIcon.KindProperty, MaterialDesignThemes.Wpf.PackIconKind.Basket);
            template_productos_nivel.VisualTree.AppendChild(icono_productos_nivel);
            template_productos_nivel.VisualTree.AppendChild(textblock_productos_nivel);
            item_productos_nivel.HeaderTemplate = template_productos_nivel;

            item_marca_productos.Items.Add(item_productos_nivel);

        }
    }
}
