using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexApiRio.Model
{
    public class Response
    {
        public bool success { get; set; }
        public object data { get; set; }
        public string message { get; set; }
    }
}
