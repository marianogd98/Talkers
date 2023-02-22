using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexApiRio.Model
{
    public class Habladores
    {
        public string Codigo { get; set; }
        public string Codigo_barra { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionL { get; set; }
        public string Precio { get; set; }
        public string Iva { get; set; }
        public string Total { get; set; }
        public string Precioo { get; set; }
        public int Oferta { get; set; }
        public string Departamento { get; set; }
        public int PorImprimir { get; set; }
        public string Fecha { get; set; }
        public string FechaStringFormat { get; set; }
        public string FechHorImpresion { get; set; }
        public string Moneda { get; set; }
        public string CodigoSap { get; set; }
        public double Tasa { get; set; }
        public string TipoPesado { get; set; }
        public string PesadoDescrip { get ; set;}

        public Habladores(string codigo, string codigo_barra, string descripcion, string descripcionL, string precio, string iva, string total, string precioo, int oferta, string departamento, int porImprimir, string fecha, string fechHorImpresion, string moneda, string codigoSap, double tasa, string tipoPesado, string pesadoDescrip)
        {
            Codigo = codigo;
            Codigo_barra = codigo_barra;
            Descripcion = descripcion;
            DescripcionL = descripcionL;
            Precio = precio;
            Iva = iva;
            Total = total;
            Precioo = precioo;
            Oferta = oferta;
            Departamento = departamento;
            PorImprimir = porImprimir;
            Fecha = Convert.ToDateTime(fecha).ToString(@"dd/MM/yyyy HH\:mm");
            FechHorImpresion = fechHorImpresion;
            Moneda = moneda;
            CodigoSap = codigoSap;
            Tasa = tasa;
            TipoPesado = tipoPesado;

            if (this.TipoPesado.Equals("1"))
            {
                this.PesadoDescrip = "Pesado";
            }
            else
            {
                this.PesadoDescrip = "Unidad";
            }
        }

        public string GetCodigo()
        {
            return Codigo;
        }
        public string GetCodigo_barra()
        {
            return Codigo_barra;
        }
        public string GetDescripcion()
        {
            return Descripcion;
        }

        public string GetDepartamento()
        {
            return Departamento;
        }

        public int GetPorImprimir()
        {
            return PorImprimir;
        }

        public string GetFecha()
        {
            return Fecha;
        }

        public string GetFechaImpresion()
        {
            return FechHorImpresion;
        }

        public string GetPrecio()
        {
            return Precio;
        }

        public string GetIva()
        {
            return Iva;
        }

        public string GetTotal()
        {
            return Total;
        }

        public string GetPrecioo()
        {
            return Precioo;
        }

        public int GetOferta()
        {
            return Oferta;
        }

        public void SetFechaImpresion(string fechaImpresion)
        {
            FechHorImpresion = fechaImpresion;
        }
    }

        public class HabladoresViewModel
        {
            public string Codigo { get; set; }
            public string Codigo_barra { get; set; }
            public string Descripcion { get; set; }
            public string DescripcionL { get; set; }
            public string Precio { get; set; }
            public string Iva { get; set; }
            public string Total { get; set; }
            public string Precioo { get; set; }
            public int Oferta { get; set; }
            public string Departamento { get; set; }
            public int PorImprimir { get; set; }
            public string DescripcionOferta { get; set; }
            public string Fecha { get; set; }
            public string FechHorImpresion { get; set; }
            public string Moneda { get; set; }
    }
    }
