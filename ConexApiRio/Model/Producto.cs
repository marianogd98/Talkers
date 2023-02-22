using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexApiRio.Model
{
    public class Producto
    {
        public int id { get; set; }
        public int tipo { get; set; }
        public string codigo { get; set; }
        public string barra { get; set; }
        public string descripcion { get; set; }
        public double precioDetal { get; set; }
        public double precioDetalBs { get; set; }
        public double precioOferta { get; set; }
        public double precioOfertaBs { get; set; }
        public double descuento { get; set; }
        public int pesado { get; set; }
        public string codigoMoneda { get; set; }
        public string impuesto { get; set; }
        public int valor { get; set; }
        public DateTime fechaOfertaIni { get; set; }
        public DateTime fechaOfertaFin { get; set; }
        public string departamento { get; set; }
        public string codigoBalanza { get; set; }
        public int estatus { get; set; }

        public Producto(int id, int tipo, string codigo, string barra, string descripcion, double precioDetal, double precioDetalBs, double precioOferta, double precioOfertaBs, double descuento, int pesado, string codigoMoneda, string impuesto, int valor, DateTime fechaOfertaIni, DateTime fechaOfertaFin, string departamento, string codigoBalanza, int estatus)
        {
            this.id = id;
            this.tipo = tipo;
            this.codigo = codigo;
            this.barra = barra;
            this.descripcion = descripcion;
            this.precioDetal = precioDetal;
            this.precioDetalBs = precioDetalBs;
            this.precioOferta = precioOferta;
            this.precioOfertaBs = precioOfertaBs;
            this.descuento = descuento;
            this.pesado = pesado;
            this.codigoMoneda = codigoMoneda;
            this.impuesto = impuesto;
            this.valor = valor;
            this.fechaOfertaIni = fechaOfertaIni;
            this.fechaOfertaFin = fechaOfertaFin;
            this.departamento = departamento;
            this.codigoBalanza = codigoBalanza;
            this.estatus = estatus;
        }
    }

}
