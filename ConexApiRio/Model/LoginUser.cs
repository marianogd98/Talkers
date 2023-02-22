using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexApiRio.Model
{

    public class User
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public string Nick { get; set; }
        public string Lxvl { get; set; }
        public string Token { get; set; }

    }

    public class LoginUser
    {
        public User Data { get; set; }
        public int Success { get; set; }
        public string Message { get; set; }

    }
}
