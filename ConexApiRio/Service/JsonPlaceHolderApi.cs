using ConexApiRio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConexApiRio.Service
{
    public class JsonPlaceHolderApi
    {
        private ConexHttp ConexHttp { get; set; }
        private string Dpto;
        public JsonPlaceHolderApi()
        {
            ConexHttp = new ConexHttp();
            Dpto = "00";
        }

        public JsonPlaceHolderApi(string Dpto)
        {
            this.Dpto = Dpto;
            ConexHttp = new ConexHttp();
        }

        //GET habladores
        public List<Habladores> GetHabladores(string Moneda="1")
        {           
            try
            {
                
                return JsonConvert.DeserializeObject<List<Habladores>>(ConexHttp.Get_Data(DefUrls.GetUrlApi((Dpto.Equals("00"))?"habladores/moneda/"+Moneda:"habladores/farmacia/moneda/" + Moneda)));
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<Habladores> GetHabladoresActualizados(string Moneda="1")
        {
            try
            {

                return JsonConvert.DeserializeObject<List<Habladores>>(ConexHttp.Get_Data(DefUrls.GetUrlApi("habladores/actualizados/moneda/" + Moneda)));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //@GET("hablador/{codigo}")
        public List<Habladores> GetHablador(string codigo, string Moneda = "1")
        {
            try
            {              
                return JsonConvert.DeserializeObject<List<Habladores>>(ConexHttp.Get_Data(DefUrls.GetUrlApi("hablador/"+((Dpto.Equals("05"))? "farmacia/" : "")+codigo + "/moneda/" + Moneda)));
            }
            catch
            {
                return null;
            }
        }

        //@GET("hablador/{Barra}")
        public List<Habladores> GetHabladorCodBarra(string codigoBarra, string Moneda = "1")
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Habladores>>(ConexHttp.Get_Data(DefUrls.GetUrlApi("hablador/" + ((Dpto.Equals("05")) ? "farmacia/" : "") + codigoBarra + "/moneda/" + Moneda)));
            }
            catch
            {
                return null;
            }
        }

        public List<ProductoOferta> GetOfertas()
        {
            try
            {
                Response respuesta = JsonConvert.DeserializeObject<Response>(ConexHttp.Get_Data(DefUrls.GetUrlApiOfertas("productos/ofertas/")));
                List<ProductoOferta> lista_productos = JsonConvert.DeserializeObject<List<ProductoOferta>>(respuesta.data.ToString());
                return lista_productos;
            }
            catch
            {
                return null;
            }
        }



        //@GET("product/{CodigoBarra}")
        public Producto GetProducto(string codigoBarra)
        {
            try
            {
                Response respuesta = JsonConvert.DeserializeObject<Response>(ConexHttp.Get_Data(DefUrls.GetUrlApiOfertas("productos/" + codigoBarra)));
                Producto producto = JsonConvert.DeserializeObject<Producto>(respuesta.data.ToString());
                return producto;

            }
            catch
            {
                return null;
            }  
        }

        //@GET("hablador/producto/{Descripcion}")
        public List<Habladores> GetHabladoresDescripcion(string Descripcion, string Moneda="1")
        {
            try
            {
                var mio = JsonConvert.DeserializeObject<List<Habladores>>(ConexHttp.Get_Data(DefUrls.GetUrlApi("hablador/producto/"+ ((Dpto.Equals("05") && Dpto!=null) ? "farmacia/" : "") + Descripcion+"/moneda/"+Moneda)));
                return mio;
            }
            catch
            {
                return null;
            }
        }

        //@GET("habladores/pendientes")
        public string GetHayHabladores()
        {
            return "";
        }

        //@GET("habladores/actualizar/producto/{Codigo}/user/{IdUser}/device/{IdDevice}")
        public List<Habladores> SetActualizarHablador(string Codigo, string IdUser, string IdDevice)
        {
            try
            {               
                return JsonConvert.DeserializeObject<List<Habladores>>(ConexHttp.Get_Data(DefUrls.GetUrlApi("habladores/actualizar/producto/" + Codigo + "/user/" + IdUser + "/device/" + IdDevice+"/dpto/"+Dpto)));
            }
            catch
            {
                return null;
            }
        }

        //@GET("habladores/actualizar/sin/producto/{Codigo}/user/{IdUser}/device/{IdDevice}")
        public string SetActualizarHabladorSinImpresion(string Codigo, string IdUser, string IdDevice)
        {
            try
            {
                return ConexHttp.Get_Data(DefUrls.GetUrlApi("habladores/actualizar/sin/producto/" + Codigo + "/user/" + IdUser + "/device/" + IdDevice));
            }
            catch
            {
                return null;
            }
        }

        //@GET("habladores/departamento/{Departamento}")
        public List<Habladores> GetHabladoresDepartamentos(string Departamento)
        {        
            return null;
        }

        //@GET("habladores/impresion/datetime")
        public string GetHabladoresDatetime()
        {
            return "";
        }

        //@GET("existencia/producto/{CodigoBarra}")
        public Producto GetExistenciaProducto(string CodigoBarra)
        {
            try
            {
                var mio = JsonConvert.DeserializeObject<Producto>(ConexHttp.Get_Data(DefUrls.GetUrlApi("existencia/producto/" + CodigoBarra)));
                return mio;
            }
            catch
            {
                return null;
            }
        }

        //@GET("departamento")
        public List<Departamento> getDepartamentos()
        {
            return null;
        }

        //@FormUrlEncoded
        //@POST("Login")
        public LoginUser Login(string Username,string Password,string Localidad)
        {
            LoginUser userData= new LoginUser();
            try
            {
                string result = ConexHttp.GetToken(Username, Password, Localidad);
                userData = JsonConvert.DeserializeObject<LoginUser>(result);
                //result.Result = ConexHttp.GetMacAddress();
                return userData;
            }
            catch //(JsonException e)
            {
                return userData;
            }           
        }

        //@GET("configuracion")
        public Impresion GetImpresion()
        {
            try
            {
                var mio = JsonConvert.DeserializeObject<Impresion>(ConexHttp.Get_Data(DefUrls.GetUrlApi("habladores/rtf")));
                return mio;
            }
            catch
            {
                return null;
            }
        }
    }
}
