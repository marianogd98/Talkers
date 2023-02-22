using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexApiRio.Model
{
    public class ProductoOferta
    {
        public string codigo { get; set; }
        public string barra { get; set; }
        public string descri { get; set; }
        public string precio { get; set; }
        public string precioBs { get; set; }
        public double pvpRef { get; set; }
        public double pvpBs { get; set; }
        public int iva { get; set; }
        public DateTime fecha { get; set; }
        public string departamento { get; set; }
        public string precioofertaRef { get; set; }
        public string precioofertaBs { get; set; }
        public DateTime fechaOfertaIni { get; set; }
        public DateTime fechaOfertaFin { get; set; }
        public string moneda { get; set; }
        public double tasa { get; set; }

        public ProductoOferta(string codigo, string barra, string descri, string precio, string precioBs, double pvpRef, double pvpBs, int iva, DateTime fecha, string departamento, string precioofertaRef, string precioofertaBs, DateTime fechaOfertaIni, DateTime fechaOfertaFin, string moneda, double tasa)
        {
            this.codigo = codigo;
            this.barra = barra;
            this.descri = descri;
            this.precio = precio;
            this.precioBs = precioBs;
            this.pvpRef = pvpRef;
            this.pvpBs = pvpBs;
            this.iva = iva;
            this.fecha = fecha;
            this.departamento = departamento;
            this.precioofertaRef = precioofertaRef;
            this.precioofertaBs = precioofertaBs;
            this.fechaOfertaIni = fechaOfertaIni;
            this.fechaOfertaFin = fechaOfertaFin;
            this.moneda = moneda;
            this.tasa = tasa;
        }
    }


}
